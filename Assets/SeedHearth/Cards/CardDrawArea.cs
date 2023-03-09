using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace SeedHearth.Cards
{
    public class CardDrawArea : MonoBehaviour, IPointerClickHandler
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