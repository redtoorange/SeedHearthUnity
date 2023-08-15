using System;
using System.Collections.Generic;
using SeedHearth.Cards.Data.CardProperties;
using UnityEngine;

namespace SeedHearth.Cards.Data
{
    [CreateAssetMenu(fileName = "CardData", menuName = "Card Data", order = 0)]
    [Serializable]
    public class CardData : ScriptableObject
    {
        public static string NONE_ID = "NONE";

        public string id = NONE_ID;
        public string cardTitle;
        public Sprite cardSprite;
        [TextArea(5, 8)] public string cardDescription;
        public Color cardBackgroundColor;
        public int staminaCost = 0;
        public CardType cardType;
        public List<CardProperty> cardProperties;

        public bool isSellable = true;
        public int baseSellValue = 0;
        public int basePurchaseValue = 0;
    }
}