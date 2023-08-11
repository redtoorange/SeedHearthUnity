﻿using System;
using System.Collections.Generic;
using SeedHearth.Cards;
using UnityEngine;
using Random = UnityEngine.Random;

namespace SeedHearth.Deck
{
    [Serializable]
    public class DeckManager : MonoBehaviour
    {
        [Tooltip("The transform which will be the parent of all new cards we spawn.")]
        [SerializeField] private Transform cardSpawnTarget;

        [Header("Deck")]
        [SerializeField] private DeckData currentlyLoadedDeck;
        [SerializeField] private List<Card> libraryCardInstances;
        [SerializeField] private List<Card> activeCardInstances;
        [SerializeField] private List<Card> graveyardCardInstances;


        private void Awake()
        {
            // Instance the deck
            if (currentlyLoadedDeck != null)
            {
                LoadDeck();
            }
        }

        public void LoadDeck()
        {
            libraryCardInstances = new List<Card>();
            activeCardInstances = new List<Card>();
            graveyardCardInstances = new List<Card>();

            for (int i = 0; i < currentlyLoadedDeck.deckCardData.Count; i++)
            {
                DeckCardData deckCardData = currentlyLoadedDeck.deckCardData[i];
                for (int j = 0; j < deckCardData.count; j++)
                {
                    Card newCard = SpawnNewCard(deckCardData.cardPrefab);
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

        public void AddActiveCard(Card card)
        {
            activeCardInstances.Add(card);
        }

        public Card SpawnNewCard(Card card)
        {
            return Instantiate(card, cardSpawnTarget);
        }
    }
}