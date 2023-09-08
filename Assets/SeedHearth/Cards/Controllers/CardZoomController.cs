using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;

namespace SeedHearth.Cards.Controllers
{
    public class CardZoomController : CardController
    {
        [Header("Zoom Settings")]
        [SerializeField] private float zoomInScale = 1.25f;
        [SerializeField] private float zoomInTime = 0.1f;

        private Transform trans;
        private CardZoomState currentCardState = CardZoomState.ZoomedOut;
        private float elapsed = 0.0f;
        private Vector3 targetZoomIn;
        private Vector3 targetZoomOut;

        private TweenerCore<Vector3, Vector3, VectorOptions> zoomTween;

        private void Awake()
        {
            trans = transform;
            targetZoomIn = new Vector3(zoomInScale, zoomInScale, zoomInScale);
            targetZoomOut = new Vector3(1, 1, 1);
        }

        public void SetZoomState(bool shouldBeZoomed)
        {
            if (zoomTween != null)
            {
                zoomTween.Kill();
            }

            if (shouldBeZoomed)
            {
                currentCardState = CardZoomState.ZoomedIn;
                trans.localScale = targetZoomIn;
            }
            else
            {
                currentCardState = CardZoomState.ZoomedOut;
                trans.localScale = targetZoomOut;
            }
        }

        public void ToggleZoomed(bool shouldBeZoomed)
        {
            if (shouldBeZoomed)
            {
                if (currentCardState != CardZoomState.ZoomedIn)
                {
                    if (parentCard.GetState == CardState.InHand)
                    {
                        trans.SetAsLastSibling();
                    }

                    currentCardState = CardZoomState.ZoomingIn;
                    if (zoomTween != null)
                    {
                        zoomTween.Kill();
                    }

                    zoomTween = trans.DOScale(targetZoomIn, zoomInTime)
                        .OnComplete(() => currentCardState = CardZoomState.ZoomedIn);
                }
            }
            else
            {
                if (currentCardState != CardZoomState.ZoomedOut)
                {
                    currentCardState = CardZoomState.ZoomingOut;
                    if (zoomTween != null)
                    {
                        zoomTween.Kill();
                    }

                    zoomTween = trans.DOScale(targetZoomOut, zoomInTime)
                        .OnComplete(() => currentCardState = CardZoomState.ZoomedOut);
                }
            }
        }

        private enum CardZoomState
        {
            ZoomedOut,
            ZoomingIn,
            ZoomedIn,
            ZoomingOut
        }
    }
}