using UnityEngine;

namespace SeedHearth.Cards.Areas
{
    public abstract class CardArea : MonoBehaviour
    {
        protected RectTransform rectTransform;

        private void Awake()
        {
            rectTransform = GetComponent<RectTransform>();
        }

        public Vector2 GetCenter()
        {
            Vector2 center = rectTransform.position;
            Vector2 rect = rectTransform.rect.center;
            return center + rect;
        }
        
        public virtual void AddCard(Card cardToAdd)
        {
            cardToAdd.FlipToBack();
            cardToAdd.MoveTo(GetCenter());
        }
    }
}