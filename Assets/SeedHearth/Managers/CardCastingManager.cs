using System.Collections.Generic;
using SeedHearth.Cards;
using SeedHearth.Cards.Abilities;
using SeedHearth.Cards.Data.Abilities;
using UnityEngine;
using UnityEngine.InputSystem;

namespace SeedHearth.Managers
{
    public class CardCastingManager : MonoBehaviour
    {
        [SerializeField] private Card currentlyCastingCard;
        [SerializeField] private CardManager cardManager;
        [SerializeField] private ResourceManager playerResourceManager;

        private Queue<CardAbility> callStack;
        private CardAbility currentlyActiveAbility;

        private void Start()
        {
            cardManager.OnStartCasting += StartCasting;
        }

        private void Update()
        {
            if (currentlyCastingCard != null && currentlyActiveAbility != null)
            {
                if (Mouse.current.rightButton.wasPressedThisFrame)
                {
                    CancelCasting();
                }
            }
        }

        public bool IsAbleToCast(Card card)
        {
            return playerResourceManager.HasEnoughStamina(card.GetCardData().staminaCost);
        }

        public void StartCasting(Card cardToCast)
        {
            currentlyCastingCard = cardToCast;
            callStack = new Queue<CardAbility>(cardToCast.GetComponents<CardAbility>());
            ProcessNextCardAbility();
        }

        public void ProcessNextCardAbility()
        {
            currentlyActiveAbility = null;

            // Recursively empty the stack until there are no more properties or we call the card to cast
            if (callStack.TryDequeue(out CardAbility ability))
            {
                currentlyActiveAbility = ability;
                ability.Cast(GetCastingContext(), ProcessNextCardAbility);
            }
            else
            {
                FinishedCasting();
            }
        }

        public void FinishedCasting()
        {
            playerResourceManager.AddStamina(-currentlyCastingCard.GetCardData().staminaCost);
            cardManager.DiscardCardFromPlay(currentlyCastingCard);
            currentlyCastingCard = null;
        }

        private CardCastingContext GetCastingContext()
        {
            return new CardCastingContext()
            {
                cardData = currentlyCastingCard.GetCardData(),
                cardManager = cardManager,
                playerResourceManager = playerResourceManager
            };
        }

        public void CancelCasting()
        {
            currentlyActiveAbility.CancelCasting();
            cardManager.ResetCardToHand(currentlyCastingCard);
            currentlyCastingCard = null;
            currentlyActiveAbility = null;

            // TODO need to rollback the queue to undo abilities already cast
        }
    }
}