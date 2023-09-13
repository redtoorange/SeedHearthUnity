using System;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;

namespace SeedHearth.GameAreas
{
    public class UIMover : MonoBehaviour
    {
        [Header("Slot Expand Data")]
        [SerializeField] private float expandTime = 0.2f;
        [SerializeField] private Vector2 expandedPosition = new Vector2(0, 0);
        [SerializeField] private Vector2 retractedPosition = new Vector2(0, -32.0f);

        [SerializeField] private RectTransform targetTransform;

        private TweenerCore<Vector2, Vector2, VectorOptions> movementTween;

        private void Start()
        {
            Retract();
        }

        public void Expand()
        {
            if (movementTween != null && movementTween.IsPlaying())
            {
                movementTween.Kill();
            }

            movementTween = targetTransform.DOAnchorPos(expandedPosition, expandTime);
        }

        public void Retract()
        {
            if (movementTween != null && movementTween.IsPlaying())
            {
                movementTween.Kill();
            }

            movementTween = targetTransform.DOAnchorPos(retractedPosition, expandTime);
        }
    }
}