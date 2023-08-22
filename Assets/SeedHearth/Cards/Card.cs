using SeedHearth.Cards.Data;
using UnityEngine;

namespace SeedHearth.Cards
{
    [RequireComponent(typeof(CardZoomController))]
    [RequireComponent(typeof(CardHoverController))]
    public class Card : MonoBehaviour
    {
        [Header("Card")]
        [SerializeField] private CardData cardData;
        public CardData GetCardData() => cardData;


        private bool inHand = false;

        private CardVisualController cardVisualController;
        private CardMovementController cardMovementController;
        private CardZoomController cardZoomController;
        private CardHoverController cardHoverController;
        private CardFlipAnimator cardFlipAnimator;

        public delegate void MoveToCallback(Card card);


        private void Awake()
        {
            // Gather all Controllers
            cardVisualController = GetComponent<CardVisualController>();
            cardZoomController = GetComponent<CardZoomController>();
            cardHoverController = GetComponent<CardHoverController>();
            cardMovementController = GetComponent<CardMovementController>();
            cardFlipAnimator = GetComponent<CardFlipAnimator>();

            // Initialize all Controllers
            foreach (CardController cardController in GetComponents<CardController>())
            {
                cardController.Initialize(this, cardData);
            }
        }


        public CardZoomController GetZoomControl() => cardZoomController;
        public CardHoverController GetHoverControl() => cardHoverController;

        public void SetInHand(bool isInHand)
        {
            inHand = isInHand;
        }

        public string GetName()
        {
            return cardData.cardTitle;
        }

        public void SetCanCast(bool canCast)
        {
            cardVisualController.SetDimmedCard(!canCast);
        }

        public bool InHand()
        {
            return inHand;
        }

        public void MoveTo(Vector2 position)
        {
            cardMovementController.MoveTo(position);
        }

        public void FlipToFont()
        {
            cardFlipAnimator.FlipToFront();
        }

        public void FlipToBack()
        {
            cardFlipAnimator.FlipToBack();
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }

        public void CancelMovement()
        {
            cardMovementController.CancelMovement();
        }
    }
}