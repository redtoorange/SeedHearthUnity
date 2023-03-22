using System;
using System.Collections.Generic;
using UnityEngine;

namespace SeedHearth.Cards
{
    [Serializable]
    public enum CardKeys
    {
        ProduceId,
        GrowTime,
        StaminaRecoveryAmount
    }
    
    [Serializable]
    public class CardMetaData
    {
        [SerializeField] public CardKeys key;
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
        [SerializeField] public int id = 0;
        [SerializeField] public string cardTitle;
        [SerializeField][Multiline(8)] public string cardDescription;
        [SerializeField] public Color cardBackgroundColor;
        [SerializeField] public int staminaCost = 0;
        [SerializeField] public CardType cardType = CardType.Seed;
        [SerializeField] public List<CardMetaData> cardMetaData;
    }
}