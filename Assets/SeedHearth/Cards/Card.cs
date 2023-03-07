using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace SeedHearth.Cards
{
    public class Card : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        public static Action<Card> onCardStartHover;
        public static Action<Card> onCardStopHover;

        [Header("Card")]
        [SerializeField] private CardData cardData;

        [Header("Zoom Settings")]
        [SerializeField] private float zoomInScale = 1.25f;
        [SerializeField] private float zoomInTime = 0.1f;

        [Header("Display")]
        [SerializeField] private TMP_Text cardTitleLabel;
        [SerializeField] private TMP_Text cardDescriptionLabel;
        [SerializeField] private Image cardBackgroundImage;
        
        // TODO Implement card icons
        [SerializeField] private Image cardIconImage;
        
        private void Start()
        {
            RefreshCardData();
        }

        [ContextMenu("Refresh")]
        private void RefreshCardData()
        {
            if (cardData != null)
            {
                cardTitleLabel.text = cardData.cardTitle;
                cardDescriptionLabel.text = cardData.cardDescription;
                cardBackgroundImage.color = cardData.cardBackgroundColor;
            }
        }

        public void ToggleHover(bool hovered)
        {
            LeanTween.cancel(gameObject);
            LeanTween.scale(
                gameObject,
                hovered ? new Vector2(zoomInScale, zoomInScale) : new Vector2(1, 1),
                zoomInTime
            );
        }


        public void OnPointerEnter(PointerEventData eventData)
        {
            onCardStartHover?.Invoke(this);
        }


        public void OnPointerExit(PointerEventData eventData)
        {
            onCardStopHover?.Invoke(this);
        }
    }
}