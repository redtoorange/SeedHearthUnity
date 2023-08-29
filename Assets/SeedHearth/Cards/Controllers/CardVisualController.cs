using SeedHearth.Cards.Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SeedHearth.Cards
{
    public class CardVisualController : CardController
    {
        [Header("Display")]
        [SerializeField] private TMP_Text cardTitleLabel;
        [SerializeField] private TMP_Text cardDescriptionLabel;
        [SerializeField] private Image cardBackgroundImage;
        [SerializeField] private Image cardIconImage;

        [Header("Stamina")]
        [SerializeField] private GameObject staminaHolder;
        [SerializeField] private TMP_Text staminaCost;


        public override void Initialize(Card pCard, CardData pCardData)
        {
            base.Initialize(pCard, pCardData);

            cardTitleLabel.text = cardData.cardTitle;
            cardDescriptionLabel.text = cardData.cardDescription;
            cardBackgroundImage.color = cardData.cardBackgroundColor;
            staminaCost.text = cardData.staminaCost.ToString();
            cardIconImage.sprite = cardData.cardSprite;

            if (cardData.staminaCost == 0)
            {
                staminaHolder.SetActive(false);
            }
        }
    }
}