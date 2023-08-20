using UnityEngine;

namespace SeedHearth.Cards
{
    public class CardZoomController : CardController
    {
        [Header("Zoom Settings")]
        [SerializeField] private float zoomInScale = 1.25f;
        [SerializeField] private float zoomInTime = 0.1f;

        private bool isZoomed = false;

        public void ToggleZoomed(bool zoomed)
        {
            return;
            
            if (zoomed == isZoomed) return;

            isZoomed = zoomed;
            transform.SetAsLastSibling();

            if (isZoomed)
            {
                ZoomIn();
            }
            else
            {
                ZoomOut();
            }
        }

        public void ZoomIn()
        {
            LeanTween.cancel(gameObject);
            LeanTween.scale(
                gameObject,
                new Vector2(zoomInScale, zoomInScale),
                zoomInTime
            );
        }

        public void ZoomOut()
        {
            LeanTween.cancel(gameObject);
            LeanTween.scale(
                gameObject,
                new Vector2(1, 1),
                zoomInTime
            );
        }
    }
}