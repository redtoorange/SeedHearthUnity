using System;
using System.Collections.Generic;
using SeedHearth.Cards;
using UnityEngine;
using Random = UnityEngine.Random;

namespace SeedHearth.Deck
{
    [Serializable]
    public class DeckManager : MonoBehaviour
    {
        [SerializeField] private Transform cardSpawnTarget;
        
        private DeckData sourceDeck;

        // Cards that are in the draw pile
        [SerializeField] private List<Card> libraryCardInstances;

        // Cards that are currently in play
        [SerializeField] private List<Card> activeCardInstances;

        // Card that are currently in the discard pile
        [SerializeField] private List<Card> graveyardCardInstances;


        public void LoadDeck(DeckData sourceDeck)
        {
            this.sourceDeck = sourceDeck;

            libraryCardInstances = new List<Card>();
            activeCardInstances = new List<Card>();
            graveyardCardInstances = new List<Card>();

            for (int i = 0; i < sourceDeck.deckCardData.Count; i++)
            {
                DeckCardData deckCardData = sourceDeck.deckCardData[i];
                for (int j = 0; j < deckCardData.count; j++)
                {
                    Card newCard = Instantiate(deckCardData.cardPrefab, cardSpawnTarget);
                    newCard.gameObject.SetActive(false);
                    libraryCardInstances.Add(newCard);
                }
            }

            Debug.Log($"Created deck instance with {libraryCardInstances.Count} cards");
        }

        public Card DrawCard()
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
            Card card = libraryCardInstances[index];
            libraryCardInstances.RemoveAt(index);
            activeCardInstances.Add(card);
            card.gameObject.SetActive(true);

            return card;
        }

        public void DiscardCard(Card cardToDiscard)
        {
            cardToDiscard.gameObject.SetActive(false);
            activeCardInstances.Remove(cardToDiscard);
            graveyardCardInstances.Add(cardToDiscard);
        }

        public void ShuffleDeck()
        {
            libraryCardInstances.AddRange(graveyardCardInstances);
            graveyardCardInstances.Clear();
        }
    }
}