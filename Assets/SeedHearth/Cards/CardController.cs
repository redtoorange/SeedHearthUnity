using System.Collections.Generic;
using SeedHearth.Deck;
using UnityEngine;
using UnityEngine.Serialization;

namespace SeedHearth.Cards
{
    public class CardController : MonoBehaviour
    {
        [Header("Player Areas")]
        [SerializeField] private CardDrawArea cardDrawArea;
        [SerializeField] private CardHandArea cardHandArea;
        [SerializeField] private CardDiscardArea cardDiscardArea;

        [Header("Deck")]
        [SerializeField] private DeckData currentlyLoadedDeck;
        [SerializeField] private DeckInstance deckInstance;

        [Header("Cards")]
        [SerializeField] private Card cardPrefab;
        [FormerlySerializedAs("instancedCard")] [SerializeField] private List<Card> instancedCards;

        private void Start()
        {
            // Wire signals
            cardDrawArea.onDrawAreaClicked += HandleDrawAreaClicked;

            // Instance the deck
            if (currentlyLoadedDeck != null)
            {
                deckInstance = new DeckInstance(currentlyLoadedDeck);
            }

            // Gather instanced Cards
            instancedCards = new List<Card>();
            instancedCards.AddRange(GetComponentsInChildren<Card>());

            // Migrate the instanced cards to the hand area
            foreach (Card card in instancedCards)
            {
                cardHandArea.AddCard(card);
            }
        }

        private void HandleDrawAreaClicked()
        {
            if (deckInstance == null) return;

            Debug.Log("Should Draw Card");
            CardData drawnCard = deckInstance.DrawCard();

            if (drawnCard != null)
            {
                Debug.Log("Drew card: " + drawnCard.cardTitle);
                Card newCard = Instantiate(cardPrefab, transform);
                newCard.Initialize(drawnCard);
                cardHandArea.AddCard(newCard);
            }
            else
            {
                Debug.Log("No cards remaining");
            }
        }
    }
}