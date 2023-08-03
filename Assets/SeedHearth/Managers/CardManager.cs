using System;
using System.Collections.Generic;
using SeedHearth.Cards;
using SeedHearth.Cards.Areas;
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

        [Header("Deck")]
        [SerializeField] private DeckData currentlyLoadedDeck;
        [SerializeField] private DeckManager deckManager;

        [Header("Cards")]
        [SerializeField] private List<Card> instancedCards;
        [SerializeField] private int drawHandSize = 4;

        private void Awake()
        {
            // Wire signals
            cardDrawArea.onDrawAreaClicked += HandleDrawAreaClicked;

            // Instance the deck
            if (currentlyLoadedDeck != null)
            {
                deckManager.LoadDeck(currentlyLoadedDeck);
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
            if (deckManager == null) return;

            Card drawnCard = deckManager.DrawCard();

            if (drawnCard != null)
            {
                cardHandArea.AddCard(drawnCard);
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
                deckManager.DiscardCard(heldCard);
            }
        }

        /**
         * Card is moved to the discard pile, will be shuffled back into deck on reshuffle
         */
        public void DiscardCardFromHand(Card card)
        {
            if (deckManager == null) return;

            cardHandArea.RemoveCard(card);
            deckManager.DiscardCard(card);
        }

        public void DiscardCardFromPlay(Card card)
        {
            if (deckManager == null) return;

            deckManager.DiscardCard(card);
        }

        /**
         * Card is removed entirely from play
         */
        public void BurnCard(Card card)
        {
            if (deckManager == null) return;
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