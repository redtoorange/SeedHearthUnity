using UnityEngine;

namespace SeedHearth.Cards
{
    public class CardSelectionController : MonoBehaviour
    {
        [SerializeField] private CardHandArea cardHandArea;
        private Card currentlyHoveredCard;
        private Card currentlyDraggingCard;
        private Camera camera;
        private RectTransform canvasTransform;


        private bool isDragging = false;
        private RectTransform cardTransform;
        private Vector2 cardDragOffset;

        private void Start()
        {
            camera = Camera.main;
            canvasTransform = GetComponentInParent<Canvas>().GetComponent<RectTransform>();
        }

        private void OnEnable()
        {
            Card.onCardStartHover += HandleCardStartHover;
            Card.onCardStopHover += HandleCardStopHover;
            Card.onCardStartDrag += HandleCardStartDrag;
            Card.onCardStopDrag += HandleCardStopDrag;
        }

        private void OnDisable()
        {
            Card.onCardStartHover -= HandleCardStartHover;
            Card.onCardStopHover -= HandleCardStopHover;
            Card.onCardStartDrag -= HandleCardStartDrag;
            Card.onCardStopDrag -= HandleCardStopDrag;
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
                Input.mousePosition,
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
            card.ToggleHover(true);
        }

        private void HandleCardStopHover(Card card)
        {
            if (isDragging) return;

            if (card == currentlyHoveredCard)
            {
                currentlyHoveredCard = null;
            }

            card.ToggleHover(false);
            cardHandArea.OrganizeCards();
        }

        private void HandleCardStartDrag(Card card)
        {
            currentlyDraggingCard = card;
            isDragging = true;
            cardTransform = currentlyDraggingCard.GetComponent<RectTransform>();
            cardDragOffset = cardTransform.anchoredPosition - GetMousePosition();
        }

        private void HandleCardStopDrag(Card card)
        {
            if (card == currentlyDraggingCard)
            {
                isDragging = false;
                currentlyDraggingCard = null;
                cardHandArea.OrganizeCards();
            }
        }
    }
}