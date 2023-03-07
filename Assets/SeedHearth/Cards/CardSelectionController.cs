using UnityEngine;

namespace SeedHearth.Cards
{
    public class CardSelectionController : MonoBehaviour
    {
        private Card currentlyHoveredCard;
        private void OnEnable()
        {
            Card.onCardStartHover += HandleCardStartHover;
            Card.onCardStopHover += HandleCardStopHover;
        }

        private void OnDisable()
        {
            Card.onCardStartHover -= HandleCardStartHover;
            Card.onCardStopHover -= HandleCardStopHover;
        }

        private void HandleCardStartHover(Card card)
        {
            if (currentlyHoveredCard != null)
            {
                HandleCardStopHover(currentlyHoveredCard);
            }
            currentlyHoveredCard = card;
            Debug.Log("Start Hovering: " + card.name);
            card.ToggleHover(true);
        }

        private void HandleCardStopHover(Card card)
        {
            if (card == currentlyHoveredCard)
            {
                currentlyHoveredCard = null;
            }
            Debug.Log("Stop Hovering: " + card.name);
            card.ToggleHover(false);
        }
    }
}