using UnityEngine;
using UnityEngine.UI;

namespace SeedHearth.Cards.Controllers
{
    /**
     * This kind of works, but the alpha blending is shit and makes it look bad-ish
     */
    public class CardFadeController : CardController
    {
        [SerializeField] private float fadeTargetAmount = 0.80f;

        private void Start()
        {
            Image[] images = GetComponentsInChildren<Image>();
            foreach (Image image in images)
            {
                Color baseColor = image.color;
                baseColor.a = fadeTargetAmount;
                image.color = baseColor;
            }
        }
    }
}