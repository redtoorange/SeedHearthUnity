using System;
using UnityEngine.EventSystems;

namespace SeedHearth.Cards.Areas
{
    public class CardDrawArea : CardArea, IPointerClickHandler
    {
        public Action onDrawAreaClicked;

        public void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Left)
            {
                onDrawAreaClicked?.Invoke();
            }
        }
    }
}