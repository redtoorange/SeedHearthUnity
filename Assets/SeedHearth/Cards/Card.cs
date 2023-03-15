using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace SeedHearth.Cards
{
    public class Card : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
    {
        public static Action<Card> onCardStartHover;
        public static Action<Card> onCardStopHover;

        public static Action<Card> onCardStartDrag;
        public static Action<Card> onCardStopDrag;

        [Header("Card")]
        [SerializeField] private CardData cardData;
        public CardData GetCardData() => cardData;

        [Header("Zoom Settings")]
        [SerializeField] private float zoomInScale = 1.25f;
        [SerializeField] private float zoomInTime = 0.1f;
        [SerializeField] private float yMoveAmount = 100.0f;

        [Header("Display")]
        [SerializeField] private TMP_Text cardTitleLabel;
        [SerializeField] private TMP_Text cardDescriptionLabel;
        [SerializeField] private Image cardBackgroundImage;
        [SerializeField] private TMP_Text staminaCost;

        // TODO Implement card icons
        [SerializeField] private Image cardIconImage;
        private bool inHand = false;
        
        private void Start()
        {
            RefreshCardData();
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

        public void ToggleHover(bool hovered)
        {
            transform.SetAsLastSibling();
            LeanTween.cancel(gameObject);
            LeanTween.scale(
                gameObject,
                hovered ? new Vector2(zoomInScale, zoomInScale) : new Vector2(1, 1),
                zoomInTime
            );
            if (hovered && inHand)
            {
                LeanTween.moveY(
                    gameObject,
                    transform.position.y + yMoveAmount,
                    zoomInTime
                );
            }
        }


        public void OnPointerEnter(PointerEventData eventData)
        {
            onCardStartHover?.Invoke(this);
        }


        public void OnPointerExit(PointerEventData eventData)
        {
            onCardStopHover?.Invoke(this);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            onCardStartDrag?.Invoke(this);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            onCardStopDrag?.Invoke(this);
        }

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