using DG.Tweening;
using SeedHearth.Data;
using UnityEngine;

namespace SeedHearth.Cards.Controllers
{
    public class CardMovementController : CardController
    {
        [SerializeField] private float movementTime = 10.0f;
        private RectTransform rectTransform;

        public override void Initialize(Card pCard, CardData pCardData)
        {
            base.Initialize(pCard, pCardData);
            rectTransform = GetComponent<RectTransform>();
        }


        public void MoveTo(Vector2 newPosition)
        {
            rectTransform.DOKill();
            rectTransform.DOMove(newPosition, movementTime)
                // .SetSpeedBased(true)
                .SetEase(Ease.Linear);
        }

        public void CancelMovement()
        {
            rectTransform.DOKill();
        }
    }
}