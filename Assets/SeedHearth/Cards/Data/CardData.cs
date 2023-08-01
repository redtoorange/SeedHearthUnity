using System;
using System.Collections.Generic;
using UnityEngine;

namespace SeedHearth.Cards
{
    [CreateAssetMenu(fileName = "CardData", menuName = "Card Data", order = 0)]
    [Serializable]
    public class CardData : ScriptableObject
    {
        public static string NONE_ID = "NONE";

        [SerializeField] public string id = NONE_ID;
        [SerializeField] public string cardTitle;
        [SerializeField] [Multiline(8)] public string cardDescription;
        [SerializeField] public Color cardBackgroundColor;
        [SerializeField] public int staminaCost = 0;
        [SerializeField] public CardType cardType;
        [SerializeField] private List<CardProperty> cardProperties;
    }
}