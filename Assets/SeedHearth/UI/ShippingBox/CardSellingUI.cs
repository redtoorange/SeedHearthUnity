using System.Collections.Generic;
using System.Linq;
using SeedHearth.Cards;
using UnityEngine;

namespace SeedHearth.UI.ShippingBox
{
    public class CardSellingUI : MonoBehaviour
    {
        [SerializeField] private RectTransform cardHolder;
        [SerializeField] private RectTransform cardSellingWindow;

        private void Start()
        {
            SetVisible(false);
        }

        public void SetVisible(bool shouldBeVisible)
        {
            cardSellingWindow.gameObject.SetActive(shouldBeVisible);
        }

        public void ToggleVisibility()
        {
            cardSellingWindow.gameObject.SetActive(!cardSellingWindow.gameObject.activeSelf);
        }

        public void AddCard(Card soldCard)
        {
            soldCard.transform.parent = cardHolder;
            OrganizeCards();
        }

        private void OrganizeCards()
        {
            List<Card> cards = GetComponentsInChildren<Card>().ToList();
            cards.Sort(new CardNameComparer());

            for (int i = 0; i < cards.Count; i++)
            {
                cards[i].transform.SetSiblingIndex(i);
            }
        }

        public class CardNameComparer : IComparer<Card>
        {
            public int Compare(Card a, Card b)
            {
                if (a == b || a == null || b == null)
                {
                    return 0;
                }

                return a.GetCardData().name.CompareTo(b.GetCardData().name);
            }
        }
    }
}