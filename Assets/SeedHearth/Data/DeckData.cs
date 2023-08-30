using System;
using System.Collections.Generic;
using SeedHearth.Cards;
using UnityEngine;

namespace SeedHearth.Data
{
    [Serializable]
    public class DeckCardData
    {
        public int count = 1;
        public Card cardPrefab;
    }

    [CreateAssetMenu(fileName = "DeckData", menuName = "Deck Data", order = 0)]
    [Serializable]
    public class DeckData : ScriptableObject
    {
        [SerializeField] public string deckName;
        [SerializeField] public List<DeckCardData> deckCardData;
    }
}