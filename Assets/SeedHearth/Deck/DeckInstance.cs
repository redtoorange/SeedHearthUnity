using System;
using System.Collections.Generic;
using SeedHearth.Cards;
using UnityEngine;
using Random = UnityEngine.Random;

namespace SeedHearth.Deck
{
    [Serializable]
    public class DeckInstance
    {
        private DeckData sourceDeck;

        [SerializeField] private List<CardData> undrawnCardInstances;
        [SerializeField] private List<CardData> drawnCardInstances;


        public DeckInstance(DeckData sourceDeck)
        {
            this.sourceDeck = sourceDeck;
            LoadDeck();
        }

        private void LoadDeck()
        {
            undrawnCardInstances = new List<CardData>();
            drawnCardInstances = new List<CardData>();
            for (int i = 0; i < sourceDeck.deckCardData.Count; i++)
            {
                DeckCardData deckCardData = sourceDeck.deckCardData[i];
                for (int j = 0; j < deckCardData.count; j++)
                {
                    undrawnCardInstances.Add(deckCardData.cardData);
                }
            }

            Debug.Log($"Created deck instance with {undrawnCardInstances.Count} cards");
        }

        public CardData DrawCard()
        {
            if (undrawnCardInstances.Count == 0) return null;

            int index = Random.Range(0, undrawnCardInstances.Count);
            CardData card = undrawnCardInstances[index];
            undrawnCardInstances.RemoveAt(index);
            drawnCardInstances.Add(card);

            return card;
        }

        public void RemoveCardFromDrawn(CardData cardData)
        {
            drawnCardInstances.Remove(cardData);
        }

        public void RemoveCardFromUndrawn(CardData cardData)
        {
            undrawnCardInstances.Remove(cardData);
        }

        public void ShuffleDeck()
        {
            undrawnCardInstances.AddRange(drawnCardInstances);
            drawnCardInstances.Clear();
        }
    }
}