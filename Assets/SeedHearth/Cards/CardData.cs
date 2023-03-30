using System;
using System.Collections.Generic;
using RotaryHeart.Lib.SerializableDictionary;
using UnityEngine;

namespace SeedHearth.Cards
{
    [Serializable]
    public enum CardPropertyKeys
    {
        Targetable,
        Targets,
        Applies,
        Plants,
        GrowTime,
        StaminaGain,
        Consumable,
        Sellable
    }
    
    [Serializable]
    public class CardMetaData
    {
        [SerializeField] public CardPropertyKeys key;
        [SerializeField] public string value;
    }

    [Serializable]
    public enum CardType
    {
        Seed,
        Produce,
        Tool,
        Consumable
    }

    [CreateAssetMenu(fileName = "CardData", menuName = "Card Data", order = 0)]
    [Serializable]
    public class CardData : ScriptableObject
    {
        public static string NONE_ID = "NONE";

        [SerializeField] public string id = NONE_ID;
        [SerializeField] public string cardTitle;
        [SerializeField][Multiline(8)] public string cardDescription;
        [SerializeField] public Color cardBackgroundColor;
        [SerializeField] public int staminaCost = 0;
        [SerializeField] public CardType cardType = CardType.Seed;
        [SerializeField] private SerializableDictionaryBase<CardPropertyKeys, string> cardProperties;
    }
}