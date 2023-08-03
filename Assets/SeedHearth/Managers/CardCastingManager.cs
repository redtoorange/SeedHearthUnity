using System.Collections.Generic;
using SeedHearth.Cards;
using SeedHearth.Cards.Data.Abilities;
using UnityEngine;

namespace SeedHearth.Managers
{
    public class CardCastingManager : MonoBehaviour
    {
        [SerializeField] private Card currentlyCastingCard;

        private CardManager cardManager;
        private ResourceManager playerResourceManager;
        private PlantManager plantManager;

        private Queue<CardAbility> callStack = new Queue<CardAbility>();

        private void Start()
        {
            cardManager = FindFirstObjectByType<CardManager>();
            cardManager.OnStartCasting += StartCasting;
            playerResourceManager = FindFirstObjectByType<ResourceManager>();
            plantManager = FindFirstObjectByType<PlantManager>();
        }

        public bool IsAbleToCast(Card card)
        {
            return playerResourceManager.HasEnoughStamina(card.GetCardData().staminaCost);
        }

        public void StartCasting(Card cardToCast)
        {
            Debug.Log("Casting: " + cardToCast.GetName());
            currentlyCastingCard = cardToCast;

            // Create a stack of properties to cast
            callStack = new Queue<CardAbility>(cardToCast.GetCardData().cardAbilities);
            ProcessNextCardAbility();
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
            playerResourceManager.ChangeStamina(-currentlyCastingCard.GetCardData().staminaCost);
            cardManager.DiscardCardFromPlay(currentlyCastingCard);
            Debug.Log("Done Casting: " + currentlyCastingCard.GetName());
            currentlyCastingCard = null;
        }
    }
}