using System;
using SeedHearth.Cards.Data;
using UnityEngine;

namespace SeedHearth.Cards
{
    [RequireComponent(typeof(CardZoomController))]
    [RequireComponent(typeof(CardHoverController))]
    public class Card : MonoBehaviour
    {
        public static Action<Card, CardState, CardState> OnCardChangeState;

        [Header("Card")]
        [SerializeField] private CardData cardData;
        public CardData GetCardData() => cardData;
        
        [Tooltip("Should shit card be destroyed on discard?")]
        [SerializeField] private bool isEphemeral = false;

        private CardVisualController cardVisualController;
        private CardMovementController cardMovementController;
        private CardZoomController cardZoomController;
        private CardHoverController cardHoverController;
        private CardFlipAnimator cardFlipAnimator;

        public delegate void MoveToCallback(Card card);

        [SerializeField]
        private CardState currentState = CardState.None;
        public CardState GetState => currentState;

        public void SetState(CardState newState)
        {
            if (newState == currentState) return;

            OnCardChangeState?.Invoke(this, currentState, newState);
            currentState = newState;
        }

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

        public string GetName()
        {
            return cardData.cardTitle;
        }

        public void SetCardDimmed(bool shouldCardDim)
        {
            cardVisualController.SetDimmedCard(shouldCardDim);
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

        public void ResetZoom()
        {
            cardZoomController.SetZoomState(false);
        }

        public void SetAsEphemeral()
        {
            isEphemeral = true;
        }

        public bool IsEphemeral => isEphemeral;

        public bool IsZoomable()
        {
            return currentState == CardState.InHand || currentState == CardState.BeingSold;

        }

        public bool InCastableState()
        {
            if (currentState == CardState.InHand) return true;

            return false;
        }
    }
}