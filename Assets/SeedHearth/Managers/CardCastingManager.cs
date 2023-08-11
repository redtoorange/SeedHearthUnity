using System.Collections.Generic;
using SeedHearth.Cards;
using SeedHearth.Cards.Data.Abilities;
using SeedHearth.SelectionArrow;
using UnityEngine;
using UnityEngine.InputSystem;

namespace SeedHearth.Managers
{
    public class CardCastingManager : MonoBehaviour
    {
        [SerializeField] private Card currentlyCastingCard;
        [SerializeField] private ArrowCurve cardTargetingCurve;
        [SerializeField] private CardManager cardManager;
        [SerializeField] private ResourceManager playerResourceManager;

        private Queue<CardAbility> callStack = new Queue<CardAbility>();

        private void Start()
        {
            cardManager.OnStartCasting += StartCasting;
        }

        private void Update()
        {
            if (currentlyCastingCard != null && Mouse.current.rightButton.wasPressedThisFrame)
            {
                CancelCasting();
            }
        }

        public bool IsAbleToCast(Card card)
        {
            return playerResourceManager.HasEnoughStamina(card.GetCardData().staminaCost);
        }

        public void StartCasting(Card cardToCast)
        {
            Debug.Log("Casting: " + cardToCast.GetName());
            currentlyCastingCard = cardToCast;

            if (cardToCast.TryGetComponent(out CardTargetingController targetter))
            {
                cardTargetingCurve.gameObject.SetActive(true);
            }
            else
            {
                // Create a stack of properties to cast
                callStack = new Queue<CardAbility>(cardToCast.GetComponents<CardAbility>());
                ProcessNextCardAbility();
            }
        }

        public void ProcessNextCardAbility()
        {
            // Recursively empty the stack until there are no more properties or we call the card to cast
            if (callStack.TryDequeue(out CardAbility ability))
            {
                CardCastingContext context = new CardCastingContext()
                {
                    cardData = currentlyCastingCard.GetCardData(),
                    cardManager = cardManager,
                    playerResourceManager = playerResourceManager
                };
                ability.Cast(context, ProcessNextCardAbility);
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
            Debug.Log("Done Casting: " + currentlyCastingCard.GetName());
            currentlyCastingCard = null;
        }

        public void CancelCasting()
        {
            Debug.Log("Should Cancel Cast");
            cardTargetingCurve.gameObject.SetActive(false);
            cardManager.ResetCardToHand(currentlyCastingCard);
            currentlyCastingCard = null;
        }
    }
}