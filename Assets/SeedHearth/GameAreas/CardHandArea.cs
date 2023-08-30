using System.Collections.Generic;
using SeedHearth.Cards;
using SeedHearth.Managers;
using UnityEngine;

namespace SeedHearth.GameAreas
{
    public class CardHandArea : CardArea
    {
        [SerializeField] private CardCastingManager cardCastingManager;
        [SerializeField] private float maxWidthPerCard = 50.0f;
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
            card.SetState(CardState.InHand);
            heldCards.Sort(new CardPositionComparer());
            OrganizeCards();
        }

        public List<Card> GetHeldCards()
        {
            return new List<Card>(heldCards);
        }

        public void RemoveCard(Card card)
        {
            heldCards.Remove(card);
            OrganizeCards();
        }

        public void OrganizeCards()
        {
            Rect handAreaRect = GetWorldBounds();


            float totalWidth = handAreaRect.width;
            float totalCards = heldCards.Count;
            float perCardWidth = totalWidth;
            if (totalCards > 0)
            {
                perCardWidth = totalWidth / totalCards;
            }

            perCardWidth = Mathf.Min(perCardWidth, maxWidthPerCard);

            Vector3 startPos = handAreaRect.center;
            startPos.x -= (perCardWidth * totalCards) / 2.0f;
            // startPos.y -= handAreaRect.height / 2.0f;

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
                card.SetCardDimmed(!cardCastingManager.IsAbleToCast(card));
            }
        }
    }


    public class CardPositionComparer : IComparer<Card>
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