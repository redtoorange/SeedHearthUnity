using SeedHearth.Cards;
using SeedHearth.Cards.Areas;
using SeedHearth.MouseController;
using UnityEngine;
using UnityEngine.InputSystem;

namespace SeedHearth.Managers
{
    public class CardSelectionManager : MonoBehaviour
    {
        private MouseEnterDetector mouseEnterDetector;
        private CardManager cardManager;
        private CardCastingManager cardCastingManager;

        private Card currentlyHoveredCard;
        private Card currentlyDraggingCard;

        [SerializeField]
        private Canvas playCanvas;
        private RectTransform canvasTransform;


        private bool isDragging = false;
        private RectTransform cardTransform;
        private Vector2 cardDragOffset;

        private void Start()
        {
            canvasTransform = playCanvas.GetComponent<RectTransform>();
            mouseEnterDetector = GetComponent<MouseEnterDetector>();
            cardManager = GetComponent<CardManager>();
            cardCastingManager = GetComponent<CardCastingManager>();
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
            if (!card.InHand()) return;

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
            cardManager.ResetCardHand();
        }

        private void HandleCardStartDrag(Card card)
        {
            if (!cardCastingManager.IsAbleToCast(card)) return;

            currentlyDraggingCard = card;
            isDragging = true;
            cardTransform = currentlyDraggingCard.GetComponent<RectTransform>();
            cardDragOffset = cardTransform.anchoredPosition - GetMousePosition();

            cardManager.PlayingCard(card);
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
                    cardManager.DiscardCardFromHand(card);
                }
                else if (area is CardHandArea || area is CardDrawArea)
                {
                    cardManager.ResetCardToHand(card);
                }
                else
                {
                    cardManager.StartCasting(card);
                }
            }
        }
    }
}