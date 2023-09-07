using System.Collections;
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

        [Header("Slot Expand Data")]
        [SerializeField] private float expandTime = 0.5f;
        [SerializeField] private float expandedHeight = 64;
        [SerializeField] private float retractedHeight = 0;
        private float elapsedTime = 0.0f;
        private Coroutine runningCoroutine;
        
        
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

        public void Expand()
        {
            if (runningCoroutine != null)
            {
                StopCoroutine(runningCoroutine);
            }

            runningCoroutine = StartCoroutine(ExpandUp());
        }

        public void Retract()
        {
            if (runningCoroutine != null)
            {
                StopCoroutine(runningCoroutine);
            }

            runningCoroutine = StartCoroutine(RetractDown());
        }

       

        private IEnumerator ExpandUp()
        {
            float start = rectTransform.rect.height;
            float totalDelta = expandedHeight - retractedHeight;    // 64 - 0 = 64      |   64-0=64
            float currentDelta = expandedHeight - start;            // 64 - 0 = 64      |   64-32=32
            float percent = currentDelta / totalDelta;              // 64/64 = 1        |   32/64=0.5
            
            elapsedTime = expandTime * (1 - percent);               // 0.5 * (1 - 1) = 0|   0.5* (1-0.5) = 0.5*0.5=0.25
            
            while (true)
            {
                elapsedTime += Time.deltaTime;
                float amount = Mathf.Lerp(retractedHeight, expandedHeight, elapsedTime / expandTime);
                rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, amount);
                if (elapsedTime >= expandTime)
                {
                    break;
                }

                yield return null;
            }

            rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, expandedHeight);
        }

        private IEnumerator RetractDown()
        {
            float start = rectTransform.rect.height;
            float totalDelta = retractedHeight - expandedHeight;
            float currentDelta = retractedHeight - start;
            float percent = currentDelta / totalDelta;
            
            elapsedTime = expandTime * (1 - percent);

            while (true)
            {
                elapsedTime += Time.deltaTime;
                float amount = Mathf.Lerp(expandedHeight, retractedHeight, elapsedTime / expandTime);
                rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, amount);
                if (elapsedTime >= expandTime)
                {
                    break;
                }

                yield return null;
            }

            rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, retractedHeight);
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