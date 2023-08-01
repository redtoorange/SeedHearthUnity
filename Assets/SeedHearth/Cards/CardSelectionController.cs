using UnityEngine;
using UnityEngine.InputSystem;

namespace SeedHearth.Cards
{
    public class CardSelectionController : MonoBehaviour
    {
        private MouseEnterDetector mouseEnterDetector;
        private CardController cardController;

        private Card currentlyHoveredCard;
        private Card currentlyDraggingCard;

        private RectTransform canvasTransform;


        private bool isDragging = false;
        private RectTransform cardTransform;
        private Vector2 cardDragOffset;

        private void Start()
        {
            canvasTransform = GetComponentInParent<Canvas>().GetComponent<RectTransform>();
            mouseEnterDetector = GetComponent<MouseEnterDetector>();
            cardController = GetComponent<CardController>();
        }

        private void OnEnable()
        {
            CardHoverController.onCardStartHover += HandleCardStartHover;
            CardHoverController.onCardStopHover += HandleCardStopHover;
            CardHoverController.onCardStartDrag += HandleCardStartDrag;
            CardHoverController.onCardStopDrag += HandleCardStopDrag;
        }

        private void OnDisable()
        {
            CardHoverController.onCardStartHover -= HandleCardStartHover;
            CardHoverController.onCardStopHover -= HandleCardStopHover;
            CardHoverController.onCardStartDrag -= HandleCardStartDrag;
            CardHoverController.onCardStopDrag -= HandleCardStopDrag;
        }

        private void Update()
        {
            if (isDragging)
            {
                UpdateDragging();
            }
        }

        private void UpdateDragging()
        {
            cardTransform.anchoredPosition = GetMousePosition() + cardDragOffset;
        }

        private Vector2 GetMousePosition()
        {
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                canvasTransform,
                Mouse.current.position.ReadValue(),
                null,
                out Vector2 localPoint
            );
            return localPoint;
        }

        private void HandleCardStartHover(Card card)
        {
            if (isDragging) return;

            if (currentlyHoveredCard != null)
            {
                HandleCardStopHover(currentlyHoveredCard);
            }

            currentlyHoveredCard = card;
            card.GetZoomControl().ToggleZoomed(true);
        }

        private void HandleCardStopHover(Card card)
        {
            if (isDragging) return;

            if (card == currentlyHoveredCard)
            {
                currentlyHoveredCard = null;
            }

            card.GetZoomControl().ToggleZoomed(false);
            cardController.ResetCardHand();
        }

        private void HandleCardStartDrag(Card card)
        {
            currentlyDraggingCard = card;
            isDragging = true;
            cardTransform = currentlyDraggingCard.GetComponent<RectTransform>();
            cardDragOffset = cardTransform.anchoredPosition - GetMousePosition();

            cardController.PlayingCard(card);
        }

        private void HandleCardStopDrag(Card card)
        {
            if (card == currentlyDraggingCard)
            {
                isDragging = false;
                currentlyDraggingCard = null;

                // TODO move this to the CardController
                CardArea area = mouseEnterDetector.DetectCardArea(out Vector2 releasePosition);
                if (area is CardDiscardArea)
                {
                    Debug.Log("Discarding Card");
                    cardController.DiscardCard(card);
                }
                else if (area is CardHandArea || area is CardDrawArea)
                {
                    cardController.ResetCardToHand(card);
                }
                else
                {
                    cardController.StartCasting(card);
                }
            }
        }
    }
}