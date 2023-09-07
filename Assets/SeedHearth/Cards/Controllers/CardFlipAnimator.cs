using DG.Tweening;
using SeedHearth.Data;
using UnityEngine;

namespace SeedHearth.Cards.Controllers
{
    public class CardFlipAnimator : CardController
    {
        [Header("Objects")]
        [SerializeField] private GameObject cardFront;
        [SerializeField] private GameObject cardBack;

        [Header("Animation")]
        [SerializeField] private float flipTime = 0.25f;

        [SerializeField] private bool showingFront = true;

        private Sequence currentSequence;

        public override void Initialize(Card pCard, CardData pCardData)
        {
            base.Initialize(pCard, pCardData);

            cardFront.transform.localScale = new Vector3(0, 1, 1);
            cardBack.transform.localScale = new Vector3(1, 1, 1);

            cardFront.SetActive(false);
            cardBack.SetActive(true);

            showingFront = false;
        }

        public void FlipToFront()
        {
            if (showingFront) return;
            FlipCard();
        }

        public void FlipToBack()
        {
            if (!showingFront) return;
            FlipCard();
        }

        public void FlipCard()
        {
            if (IsTweening())
            {
                currentSequence.Kill();
            }

            if (showingFront)
            {
                cardBack.SetActive(true);
                currentSequence = DOTween.Sequence();
                currentSequence.Append(cardFront.transform.DOScaleX(0.0f, flipTime / 2.0f))
                    .Append(cardBack.transform.DOScaleX(1.0f, flipTime / 2.0f));
            }
            else
            {
                cardFront.SetActive(true);
                currentSequence = DOTween.Sequence();
                currentSequence.Append(cardBack.transform.DOScaleX(0.0f, flipTime / 2.0f))
                    .Append(cardFront.transform.DOScaleX(1.0f, flipTime / 2.0f));
            }

            showingFront = !showingFront;
        }

        private bool IsTweening()
        {
            return currentSequence != null && currentSequence.IsPlaying();
        }
    }
}