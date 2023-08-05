using System;
using System.Collections.Generic;
using SeedHearth.Managers;
using UnityEngine;

namespace SeedHearth.Cards.Areas
{
    public class CardHandArea : CardArea
    {
        private List<Card> heldCards = new List<Card>();
        private RectTransform rectTransform;
        private Rect handAreaRect;
        private CardCastingManager cardCastingManager;

        private void Start()
        {
            rectTransform = GetComponent<RectTransform>();
            handAreaRect = rectTransform.rect;

            cardCastingManager = FindFirstObjectByType<CardCastingManager>();
        }

        private void Update()
        {
            //TODO This needs to be optimized
            UpdateCardCanCast();
        }

        public void AddCard(Card card)
        {
            heldCards.Add(card);
            OrganizeCards();
        }

        public void AddCardAtPosition(Card card)
        {
            heldCards.Add(card);
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
                heldCard.transform.SetSiblingIndex(i);
                LeanTween.move(heldCard.gameObject, cardPosition, 0.25f)
                    .setOnCompleteParam(heldCard)
                    .setOnComplete(HandleCardComplete);
            }
        }

        private void HandleCardComplete(object card)
        {
            Card castCast = (Card)card;
            castCast.AddToHand();
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