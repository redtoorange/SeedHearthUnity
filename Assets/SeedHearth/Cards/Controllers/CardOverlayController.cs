using UnityEngine;

namespace SeedHearth.Cards.Controllers
{
    public class CardOverlayController : CardController
    {
        [SerializeField] private GameObject noStaminaOverlay;
        [SerializeField] private GameObject withStaminaOverlay;

        public void SetCardDimmed(bool shouldCardDim)
        {
            if (shouldCardDim)
            {
                if (cardData.staminaCost == 0)
                    noStaminaOverlay.SetActive(true);
                else
                    withStaminaOverlay.SetActive(true);
            }
            else
            {
                noStaminaOverlay.SetActive(false);
                withStaminaOverlay.SetActive(false);
            }
        }
    }
}