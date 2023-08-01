using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace SeedHearth.Cards
{
    [RequireComponent(typeof(Card))]
    public class CardHoverController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
    {
        public static Action<Card> onCardStartHover;
        public static Action<Card> onCardStopHover;

        public static Action<Card> onCardStartDrag;
        public static Action<Card> onCardStopDrag;

        private Card parentCard;

        private void Start()
        {
            parentCard = GetComponent<Card>();
        }

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
    }
}