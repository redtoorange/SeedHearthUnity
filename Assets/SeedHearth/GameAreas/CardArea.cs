using SeedHearth.Cards;
using UnityEngine;

namespace SeedHearth.GameAreas
{
    public abstract class CardArea : MonoBehaviour
    {
        protected RectTransform rectTransform;

        private Rect bounds;
        private bool boundsDirty = true;

        private void Awake()
        {
            rectTransform = GetComponent<RectTransform>();
        }

        public Vector2 GetCenter()
        {
            if (boundsDirty)
            {
                CalculateBounds();
            }

            return bounds.center;
        }

        public Rect GetWorldBounds()
        {
            CalculateBounds();
            return bounds;
        }

        private void CalculateBounds()
        {
            boundsDirty = false;
            //  1 ----- 2   
            //  |       |
            //  |       |
            //  0 ----- 3
            Vector3[] worldCorners = new Vector3[4];
            rectTransform.GetWorldCorners(worldCorners);

            float height = worldCorners[1].y - worldCorners[0].y;
            float width = worldCorners[2].x - worldCorners[1].x;
            bounds = new Rect(worldCorners[0], new Vector2(width, height));
        }


        public virtual void AddCard(Card cardToAdd)
        {
            cardToAdd.MoveTo(GetCenter());
            cardToAdd.ResetZoom();
            cardToAdd.transform.SetParent(transform, true);
        }

        public virtual bool IsValidDropSpot(Card currentCard)
        {
            return true;
        }
    }
}