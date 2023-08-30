using System.Collections.Generic;
using SeedHearth.Cards;
using SeedHearth.Cards.Controllers;
using SeedHearth.UI;
using SeedHearth.UI.ShippingBox;
using UnityEngine;

namespace SeedHearth.Managers
{
    public class CardSellingManager : MonoBehaviour
    {
        [Header("External Dependencies")]
        [SerializeField] private ResourceManager resourceManager;
        [SerializeField] private CardManager cardManager;
        [SerializeField] private CardSellingUI cardSellingUI;

        [Header("Managed Data")]
        [SerializeField] private List<Card> soldCards = new List<Card>();

        private void OnEnable()
        {
            TurnManager.onEndTurn += SellCards;
            Card.OnCardChangeState += HandleCardChangeState;
        }

        private void OnDisable()
        {
            TurnManager.onEndTurn -= SellCards;
            Card.OnCardChangeState -= HandleCardChangeState;
        }


        private void HandleCardChangeState(Card card, CardState oldState, CardState newState)
        {
            if (oldState == CardState.BeingSold && newState != CardState.BeingSold)
            {
                RemoveCard(card);
            }
        }

        public void ToggleCardSellWindow()
        {
            cardSellingUI.ToggleVisibility();
        }

        public void AddCard(Card soldCard)
        {
            if (!soldCards.Contains(soldCard))
            {
                soldCard.SetState(CardState.BeingSold);
                soldCard.CancelMovement();
                soldCard.ResetZoom();

                soldCards.Add(soldCard);
                cardSellingUI.AddCard(soldCard);
            }
        }

        public void RemoveCard(Card cardToRemove)
        {
            if (soldCards.Contains(cardToRemove))
            {
                soldCards.Remove(cardToRemove);
            }
        }

        private void SellCards()
        {
            foreach (Card soldCard in soldCards)
            {
                if (soldCard.TryGetComponent(out CardSellingController sellingController))
                {
                    if (sellingController.IsSellable)
                    {
                        resourceManager.AddGold(sellingController.SellPrice);
                    }

                    cardManager.BurnCard(soldCard);
                }
            }

            soldCards.Clear();
        }
    }
}