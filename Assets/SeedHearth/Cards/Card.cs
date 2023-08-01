using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SeedHearth.Cards
{
    [RequireComponent(typeof(CardZoomController))]
    [RequireComponent(typeof(CardHoverController))]
    public class Card : MonoBehaviour
    {
        [Header("Card")]
        [SerializeField] private CardData cardData;
        public CardData GetCardData() => cardData;

        [Header("Display")]
        [SerializeField] private TMP_Text cardTitleLabel;
        [SerializeField] private TMP_Text cardDescriptionLabel;
        [SerializeField] private Image cardBackgroundImage;
        [SerializeField] private TMP_Text staminaCost;

        // TODO Implement card icons
        [SerializeField] private Image cardIconImage;
        private bool inHand = false;

        private CardZoomController cardZoomController;
        private CardHoverController cardHoverController;

        private void Start()
        {
            RefreshCardData();

            cardZoomController = GetComponent<CardZoomController>();
            cardHoverController = GetComponent<CardHoverController>();
        }

        public void Initialize(CardData cardData)
        {
            this.cardData = cardData;
        }

        [ContextMenu("Refresh")]
        private void RefreshCardData()
        {
            if (cardData != null)
            {
                cardTitleLabel.text = cardData.cardTitle;
                cardDescriptionLabel.text = cardData.cardDescription;
                cardBackgroundImage.color = cardData.cardBackgroundColor;
                staminaCost.text = cardData.staminaCost.ToString();
            }
        }

        public CardZoomController GetZoomControl() => cardZoomController;
        public CardHoverController GetHoverControl() => cardHoverController;

        public void AddToHand()
        {
            inHand = true;
        }

        public void RemoveFromHand()
        {
            inHand = false;
        }
    }
}