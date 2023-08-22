using System.Collections.Generic;
using SeedHearth.Cards;
using SeedHearth.UI;
using UnityEngine;

namespace SeedHearth.Managers
{
    public class CardSellingManager : MonoBehaviour
    {
        [SerializeField] private CardSellingUI cardSellingUI;
        [SerializeField] private List<Card> soldCards = new List<Card>();

        private void Start()
        {
            cardSellingUI.SetVisible(false);
            Card.OnCardChangeState += HandleCardChangeState;
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
    }
}