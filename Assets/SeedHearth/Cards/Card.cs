using System;
using SeedHearth.Cards.Controllers;
using SeedHearth.Data;
using UnityEngine;

namespace SeedHearth.Cards
{
    public class Card : MonoBehaviour
    {
        public static Action<Card, CardState, CardState> OnCardChangeState;

        [Header("Card")]
        [SerializeField] private CardData cardData;
        public CardData GetCardData() => cardData;

        [Tooltip("Should this card be destroyed on discard?")]
        [SerializeField] private bool isEphemeral = false;

        [SerializeField] private CardMovementController cardMovementController;
        [SerializeField] private CardZoomController cardZoomController;
        [SerializeField] private CardHoverController cardHoverController;
        [SerializeField] private CardFlipAnimator cardFlipAnimator;
        [SerializeField] private CardOverlayController cardOverlayController;

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

        public void Initialize()
        {
            // Initialize all Controllers
            foreach (CardController cardController in GetComponentsInChildren<CardController>())
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
            cardOverlayController.SetCardDimmed(shouldCardDim);
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

        public int GetPurchasePrice()
        {
            return cardData.basePurchaseValue;
        }
    }
}