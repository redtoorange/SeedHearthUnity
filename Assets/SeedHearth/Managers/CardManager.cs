using System;
using System.Collections.Generic;
using SeedHearth.Cards;
using SeedHearth.Cards.Areas;
using SeedHearth.Cards.Data;
using SeedHearth.Deck;
using UnityEngine;

namespace SeedHearth.Managers
{
    public class CardManager : MonoBehaviour
    {
        public Action<Card> OnStartCasting;

        [Header("Player Areas")]
        [SerializeField] private CardDrawArea cardDrawArea;
        [SerializeField] private CardHandArea cardHandArea;
        [SerializeField] private CardDiscardArea cardDiscardArea;
        [SerializeField] private Transform cardSpawnTarget; 

        [Header("Deck")]
        [SerializeField] private DeckData currentlyLoadedDeck;
        [SerializeField] private DeckInstance deckInstance;

        [Header("Cards")]
        [SerializeField] private Card cardPrefab;
        [SerializeField] private List<Card> instancedCards;
        [SerializeField] private int drawHandSize = 4;

        private void Awake()
        {
            // Wire signals
            cardDrawArea.onDrawAreaClicked += HandleDrawAreaClicked;

            // Instance the deck
            if (currentlyLoadedDeck != null)
            {
                deckInstance = new DeckInstance(currentlyLoadedDeck);
            }

            // Gather instanced Cards
            instancedCards = new List<Card>();
            instancedCards.AddRange(GetComponentsInChildren<Card>());

            // Migrate the instanced cards to the hand area
            foreach (Card card in instancedCards)
            {
                cardHandArea.AddCard(card);
            }
        }

        private void OnEnable()
        {
            TurnManager.onEndTurn += DiscardHand;
            TurnManager.onBeginTurn += DrawNewHand;
        }

        private void OnDisable()
        {
            TurnManager.onEndTurn -= DiscardHand;
            TurnManager.onBeginTurn -= DrawNewHand;
        }

        private void HandleDrawAreaClicked()
        {
            DrawCard();
        }

        public void DrawCard()
        {
            if (deckInstance == null) return;

            CardData drawnCard = deckInstance.DrawCard();

            if (drawnCard != null)
            {
                Card newCard = Instantiate(cardPrefab, cardSpawnTarget);
                newCard.Initialize(drawnCard);
                cardHandArea.AddCard(newCard);
            }
            else
            {
                Debug.Log("No cards remaining");
            }
        }

        public void DrawNewHand()
        {
            for (int i = 0; i < drawHandSize; i++)
            {
                DrawCard();
            }
        }

        public void DiscardHand()
        {
            foreach (Card heldCard in cardHandArea.GetHeldCards())
            {
                cardHandArea.RemoveCard(heldCard);
                deckInstance.DiscardCard(heldCard.GetCardData());
                Destroy(heldCard.gameObject);
            }
        }

        /**
         * Card is moved to the discard pile, will be shuffled back into deck on reshuffle
         */
        public void DiscardCardFromHand(Card card)
        {
            if (deckInstance == null) return;

            cardHandArea.RemoveCard(card);
            deckInstance.DiscardCard(card.GetCardData());
            Destroy(card.gameObject);
        }
        
        public void DiscardCardFromPlay(Card card)
        {
            if (deckInstance == null) return;

            deckInstance.DiscardCard(card.GetCardData());
            Destroy(card.gameObject);
        }

        /**
         * Card is removed entirely from play
         */
        public void BurnCard(Card card)
        {
            if (deckInstance == null) return;
        }

        public void ResetCardToHand(Card card)
        {
            cardHandArea.AddCardAtPosition(card);
        }


        public void ResetCardHand()
        {
            cardHandArea.OrganizeCards();
        }

        /**
         * Player is moving this card, it should not be managed by the hand
         */
        public void PlayingCard(Card card)
        {
            cardHandArea.RemoveCard(card);
        }

        public void StartCasting(Card card)
        {
            Debug.Log("Attempting to Cast: " + card.GetName());
            OnStartCasting?.Invoke(card);
        }

        
    }
}