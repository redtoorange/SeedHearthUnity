using UnityEngine;

namespace SeedHearth.Cards
{
    public class CardFlipAnimator : MonoBehaviour
    {
        [Header("Objects")]
        [SerializeField] private GameObject cardFront;
        [SerializeField] private GameObject cardBack;

        [Header("Animation")]
        [SerializeField] private float flipTime = 0.25f;

        [SerializeField] private bool showingFront = true;

        private void Start()
        {
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
                LeanTween.cancel(cardFront);
                LeanTween.cancel(cardBack);
            }

            if (showingFront)
            {
                cardBack.SetActive(true);
                LTSeq sequence = LeanTween.sequence();
                sequence.append(
                    LeanTween.scaleX(cardFront, 0.0f, flipTime / 2.0f)
                    .setOnComplete(_ => cardFront.SetActive(false))
                );
                sequence.append(
                    LeanTween.scaleX(cardBack, 1.0f, flipTime / 2.0f)
                );
            }
            else
            {
                cardFront.SetActive(true);
                LTSeq sequence = LeanTween.sequence();
                sequence.append(LeanTween.scaleX(cardBack, 0.0f, flipTime / 2.0f)
                    .setOnComplete(_ => cardBack.SetActive(false)));
                sequence.append(LeanTween.scaleX(cardFront, 1.0f, flipTime / 2.0f));
            }

            showingFront = !showingFront;
        }

        private bool IsTweening()
        {
            return LeanTween.isTweening(cardFront) || LeanTween.isTweening(cardBack);
        }
    }
}