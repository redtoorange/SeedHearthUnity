using System;
using System.Collections.Generic;
using SeedHearth.Cards;
using SeedHearth.Cards.Data;
using UnityEngine;
using Random = UnityEngine.Random;

namespace SeedHearth.Deck
{
    [Serializable]
    public class DeckInstance
    {
        private DeckData sourceDeck;

        // Cards that are in the draw pile
        [SerializeField] private List<CardData> libraryCardInstances;

        // Cards that are currently in play
        [SerializeField] private List<CardData> activeCardInstances;

        // Card that are currently in the discard pile
        [SerializeField] private List<CardData> graveyardCardInstances;


        public DeckInstance(DeckData sourceDeck)
        {
            this.sourceDeck = sourceDeck;
            LoadDeck();
        }

        private void LoadDeck()
        {
            libraryCardInstances = new List<CardData>();
            activeCardInstances = new List<CardData>();
            graveyardCardInstances = new List<CardData>();

            for (int i = 0; i < sourceDeck.deckCardData.Count; i++)
            {
                DeckCardData deckCardData = sourceDeck.deckCardData[i];
                for (int j = 0; j < deckCardData.count; j++)
                {
                    libraryCardInstances.Add(deckCardData.cardData);
                }
            }

            Debug.Log($"Created deck instance with {libraryCardInstances.Count} cards");
        }

        public CardData DrawCard()
        {
            // Out of cards, attempt to shuffle
            if (libraryCardInstances.Count <= 0 && graveyardCardInstances.Count > 0)
            {
                ShuffleDeck();
            }

            // After shuffle, still no cards, return null
            if (libraryCardInstances.Count <= 0)
            {
                return null;
            }

            int index = Random.Range(0, libraryCardInstances.Count);
            CardData card = libraryCardInstances[index];
            libraryCardInstances.RemoveAt(index);
            activeCardInstances.Add(card);

            return card;
        }

        public void DiscardCard(CardData cardData)
        {
            activeCardInstances.Remove(cardData);
            graveyardCardInstances.Add(cardData);
        }

        public void ShuffleDeck()
        {
            libraryCardInstances.AddRange(graveyardCardInstances);
            graveyardCardInstances.Clear();
        }
    }
}