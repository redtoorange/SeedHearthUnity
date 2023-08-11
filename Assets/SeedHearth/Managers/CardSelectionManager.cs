using SeedHearth.Cards;
using SeedHearth.Cards.Areas;
using SeedHearth.MouseController;
using UnityEngine;
using UnityEngine.InputSystem;

namespace SeedHearth.Managers
{
    public class CardSelectionManager : MonoBehaviour
    {
        [SerializeField] private MouseEnterDetector mouseEnterDetector;
        [SerializeField] private CardManager cardManager;
        [SerializeField] private CardCastingManager cardCastingManager;
        [SerializeField] private Canvas playCanvas;
        
        private Card currentlyHoveredCard;
        private Card currentlyDraggingCard;
        private RectTransform canvasTransform;
        private bool isDragging = false;
        private RectTransform cardTransform;


        private void Start()
        {
            canvasTransform = playCanvas.GetComponent<RectTransform>();
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
            Vector2 mPos = Mouse.current.position.ReadValue();
            currentlyDraggingCard.MoveTo(mPos);
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