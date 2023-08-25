using UnityEngine;

namespace SeedHearth.Cards
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

        private void Start()
        {
            trans = transform;
            targetZoomIn = new Vector3(zoomInScale, zoomInScale, zoomInScale);
            targetZoomOut = new Vector3(1, 1, 1);
        }

        public void SetZoomState(bool shouldBeZoomed)
        {
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
                    trans.SetAsLastSibling();
                    elapsed = 0.0f;
                    currentCardState = CardZoomState.ZoomingIn;
                }
            }
            else
            {
                if (currentCardState != CardZoomState.ZoomedOut)
                {
                    elapsed = 0.0f;
                    currentCardState = CardZoomState.ZoomingOut;
                }
            }
        }

        private void Update()
        {
            if (currentCardState == CardZoomState.ZoomingIn)
            {
                ProcessZoomIn();
            }
            else if (currentCardState == CardZoomState.ZoomingOut)
            {
                ProcessZoomOut();
            }
        }

        private void ProcessZoomIn()
        {
            elapsed += Time.deltaTime;
            trans.localScale = Vector3.Lerp(
                trans.localScale,
                targetZoomIn,
                elapsed / zoomInTime
            );

            if (Vector3.Distance(trans.localScale, targetZoomIn) < 0.01f)
            {
                currentCardState = CardZoomState.ZoomedIn;
                trans.localScale = targetZoomIn;
            }
        }

        private void ProcessZoomOut()
        {
            elapsed += Time.deltaTime;
            trans.localScale = Vector3.Lerp(
                trans.localScale,
                targetZoomOut,
                elapsed / zoomInTime
            );
            
            if (Vector3.Distance(trans.localScale, targetZoomOut) < 0.01f)
            {
                currentCardState = CardZoomState.ZoomedOut;
                trans.localScale = targetZoomOut;
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