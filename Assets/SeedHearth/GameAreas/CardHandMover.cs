using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;

namespace SeedHearth.GameAreas
{
    public class CardHandMover : MonoBehaviour
    {
        [Header("Slot Expand Data")]
        [SerializeField] private float expandTime = 0.5f;
        [SerializeField] private float expandedHeight = 0;
        [SerializeField] private float retractedHeight = -32;
        [SerializeField] private RectTransform targetTransform;

        private TweenerCore<Vector2, Vector2, VectorOptions> movementTween;

        public void Expand()
        {
            if (movementTween != null && movementTween.IsPlaying())
            {
                movementTween.Kill();
            }

            movementTween = targetTransform.DOAnchorPos(new Vector2(0.0f, expandedHeight), expandTime);
        }

        public void Retract()
        {
            if (movementTween != null && movementTween.IsPlaying())
            {
                movementTween.Kill();
            }

            movementTween = targetTransform.DOAnchorPos(new Vector2(0.0f, retractedHeight), expandTime);
        }
    }
}