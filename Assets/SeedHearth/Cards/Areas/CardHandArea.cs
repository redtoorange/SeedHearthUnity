using System.Collections.Generic;
using SeedHearth.Managers;
using UnityEngine;

namespace SeedHearth.Cards.Areas
{
    public class CardHandArea : CardArea
    {
        [SerializeField] private CardCastingManager cardCastingManager;

        private List<Card> heldCards = new List<Card>();

        private void Update()
        {
            //TODO This needs to be optimized
            UpdateCardCanCast();
        }

        public override void AddCard(Card card)
        {
            base.AddCard(card);
            
            heldCards.Add(card);
            card.FlipToFont();
            card.SetInHand(true);
            heldCards.Sort(new CardComparer());
            OrganizeCards();
        }

        public List<Card> GetHeldCards()
        {
            return new List<Card>(heldCards);
        }

        public void RemoveCard(Card card)
        {
            heldCards.Remove(card);
            card.SetInHand(false);
            OrganizeCards();
        }

        public void OrganizeCards()
        {
            Rect handAreaRect = GetWorldBounds();

            Vector3 startPos = handAreaRect.center;
            startPos.x -= handAreaRect.width / 2.0f;
            // startPos.y -= handAreaRect.height / 2.0f;

            float totalWidth = handAreaRect.width;
            float totalCards = heldCards.Count;
            float perCardWidth = totalWidth;
            if (totalCards > 0)
            {
                perCardWidth = totalWidth / totalCards;
            }

            for (int i = 0; i < heldCards.Count; i++)
            {
                Card heldCard = heldCards[i];
                Vector3 cardPosition = startPos + new Vector3(
                    (i * perCardWidth) + (perCardWidth / 2.0f),
                    0,
                    0
                );
                heldCard.transform.SetSiblingIndex(i);
                heldCard.MoveTo(cardPosition);
            }
        }

        public void UpdateCardCanCast()
        {
            foreach (Card card in heldCards)
            {
                card.SetCanCast(cardCastingManager.IsAbleToCast(card));
            }
        }
    }


    public class CardComparer : IComparer<Card>
    {
        public int Compare(Card a, Card b)
        {
            if (a == b || a == null || b == null)
            {
                return 0;
            }

            float aXPos = a.transform.position.x;
            float bXPos = b.transform.position.x;

            return Mathf.RoundToInt(aXPos - bXPos);
        }
    }
}