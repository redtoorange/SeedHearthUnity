using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace SeedHearth.Cards
{
    public class CardHoverController : CardController, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler,
        IPointerUpHandler, IDragHandler, IDropHandler
    {
        public static Action<Card> onCardStartHover;
        public static Action<Card> onCardStopHover;

        public static Action<Card> onCardStartDrag;
        public static Action<Card> onCardStopDrag;

        public void OnPointerEnter(PointerEventData eventData)
        {
            onCardStartHover?.Invoke(parentCard);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            onCardStopHover?.Invoke(parentCard);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            onCardStartDrag?.Invoke(parentCard);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            onCardStopDrag?.Invoke(parentCard);
        }

        public void OnDrag(PointerEventData eventData)
        {
            Debug.Log("OnDrag");
        }

        public void OnDrop(PointerEventData eventData)
        {
            Debug.Log("OnDrop");
        }
    }
}