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
        }

        public void ToggleCardSellWindow()
        {
            cardSellingUI.ToggleVisibility();
        }

        public void AddCard(Card soldCard)
        {
            soldCards.Add(soldCard);
            cardSellingUI.AddCard(soldCard);
        }

        public void RemoveCard(Card cardToRemove)
        {
            soldCards.Remove(cardToRemove);
        }
    }
}