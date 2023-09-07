using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using SeedHearth.Data;
using UnityEngine;

namespace SeedHearth.Cards.Controllers
{
    public class CardMovementController : CardController
    {
        [SerializeField] private float movementTime = 10.0f;
        private RectTransform rectTransform;
        private TweenerCore<Vector3, Vector3, VectorOptions> moveTween;

        public override void Initialize(Card pCard, CardData pCardData)
        {
            base.Initialize(pCard, pCardData);
            rectTransform = GetComponent<RectTransform>();
        }


        public void MoveTo(Vector2 newPosition)
        {
            if (moveTween != null)
            {
                moveTween.Kill();
            }

            moveTween = rectTransform.DOMove(newPosition, movementTime)
                // .SetSpeedBased(true)
                .SetEase(Ease.Linear);
        }

        public void CancelMovement()
        {
            if (moveTween != null)
            {
                moveTween.Kill();
            }
        }
    }
}