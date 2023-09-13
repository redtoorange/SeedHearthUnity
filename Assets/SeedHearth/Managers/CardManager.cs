using System;
using System.Collections.Generic;
using SeedHearth.Cards;
using SeedHearth.Cards.Controllers;
using SeedHearth.GameAreas;
using UnityEngine;

namespace SeedHearth.Managers
{
    public class CardManager : Singleton<CardManager>
    {
        public Action<Card> OnStartCasting;

        [Header("Player Areas")]
        [SerializeField] private CardDrawArea cardDrawArea;
        [SerializeField] private CardHandArea cardHandArea;
        [SerializeField] private CardDiscardArea cardDiscardArea;

        [Header("Cards")]
        [SerializeField] private List<Card> instancedCards;
        [SerializeField] private int drawHandSize = 4;

        [Header("Casting")]
        [SerializeField] private RectTransform castingPoint;

        [Header("External Dependencies")]
        [SerializeField] private DeckManager deckManager;
        [SerializeField] private CardSellingManager cardSellingManager;

        private void Awake()
        {
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
                DiscardCardFromHand(heldCard);
            }
        }

        /**
         * Card is moved to the discard pile, will be shuffled back into deck on reshuffle
         */
        public void DiscardCardFromHand(Card card)
        {
            if (deckManager == null) return;

            if (card.IsEphemeral)
            {
                BurnCard(card);
            }
            else
            {
                cardHandArea.RemoveCard(card);
                deckManager.DiscardCard(card);
                cardDiscardArea.AddCard(card);
            }
        }

        public void DiscardCardFromPlay(Card card)
        {
            // There is currently no different
            DiscardCardFromHand(card);
        }

        /**
         * Card is removed entirely from play
         */
        public void BurnCard(Card card)
        {
            if (deckManager == null) return;

            cardHandArea.RemoveCard(card);
            deckManager.DestroyCard(card);
            Destroy(card.gameObject);
        }

        public void ResetCardToHand(Card card)
        {
            cardHandArea.AddCard(card);
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
            card.SetState(CardState.BeingCasting);
            if (card.TryGetComponent(out CardTargetingController cardTargetingController))
            {
                card.MoveTo(castingPoint.position);
            }

            OnStartCasting?.Invoke(card);
        }

        public void AddNewCard(Card card)
        {
            deckManager.AddActiveCard(card);
            cardHandArea.AddCard(card);
        }

        public Card SpawnNewCard(Card card)
        {
            return deckManager.SpawnNewCard(card);
        }

        public void SellCard(Card card)
        {
            cardSellingManager.AddCard(card);
        }
    }
}