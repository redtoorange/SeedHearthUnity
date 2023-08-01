using System.Collections.Generic;
using UnityEngine;

namespace SeedHearth.Cards
{
    public class CardHandArea : CardArea
    {
        private List<Card> heldCards = new List<Card>();
        private RectTransform rectTransform;
        private Rect handAreaRect;

        private void Start()
        {
            rectTransform = GetComponent<RectTransform>();
            handAreaRect = rectTransform.rect;
        }

        public void AddCard(Card card)
        {
            heldCards.Add(card);
            card.AddToHand();
            OrganizeCards();
        }
        
        public void AddCardAtPosition(Card card)
        {
            heldCards.Add(card);
            heldCards.Sort(new CardComparer());
            card.AddToHand();
            OrganizeCards();
        }

        public List<Card> GetHeldCards()
        {
            return new List<Card>(heldCards);
        }

        public void RemoveCard(Card card)
        {
            heldCards.Remove(card);
            card.RemoveFromHand();
            OrganizeCards();
        }

        public void OrganizeCards()
        {
            // startPos will be the left edge, middle height
            //      |
            //      |
            //      x---------
            //      |
            //      |
            Vector3 startPos = rectTransform.position;
            startPos.x -= handAreaRect.width / 2.0f;
            startPos.y += handAreaRect.height / 2.0f;

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
                LeanTween.move(heldCard.gameObject, cardPosition, 0.1f);

                heldCard.transform.SetSiblingIndex(i);
            }
        }
    }
    
    public class CardComparer: IComparer<Card>
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