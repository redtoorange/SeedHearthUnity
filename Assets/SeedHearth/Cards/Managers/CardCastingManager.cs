using SeedHearth.Cards;
using SeedHearth.Player;
using UnityEngine;

namespace SeedHearth.Casting
{
    public class CardCastingManager : MonoBehaviour
    {
        [SerializeField] private Card currentlyCastingCard;

        private CardManager cardManager;
        private ResourceManager playerResourceManager;

        private void Start()
        {
            cardManager = FindFirstObjectByType<CardManager>();
            cardManager.OnStartCasting += StartCasting;

            playerResourceManager = FindFirstObjectByType<ResourceManager>();
        }

        public bool IsAbleToCast(Card card)
        {
            return playerResourceManager.HasEnoughStamina(card.GetCardData().staminaCost) ;
        }

        public void StartCasting(Card cardToCast)
        {
            Debug.Log("Casting: " + cardToCast.GetName());
            currentlyCastingCard = cardToCast;
            playerResourceManager.ChangeStamina(-currentlyCastingCard.GetCardData().staminaCost);
            cardManager.DiscardCardFromPlay(cardToCast);
            Debug.Log("Done Casting: " + cardToCast.GetName());
        }
    }
}