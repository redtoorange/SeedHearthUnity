using SeedHearth.Deck;
using UnityEngine;

namespace SeedHearth.UI.CardStore
{
    public class CardStoreController : MonoBehaviour
    {
        [SerializeField] private GameObject storeUIWindow;
        [SerializeField] private RectTransform cardStoreHolder;
        [SerializeField] private DeckData storeDeck;
        [SerializeField] private CardStoreSlot storeSlotPrefab;

        private void Start()
        {
            storeUIWindow.SetActive(false);
            InitializeStore();
        }

        public void ToggleStoreWindow()
        {
            storeUIWindow.SetActive(!storeUIWindow.activeSelf);
        }

        private void InitializeStore()
        {
            foreach (DeckCardData cardData in storeDeck.deckCardData)
            {
                CardStoreSlot newSlot = Instantiate(storeSlotPrefab, cardStoreHolder);
                newSlot.Initialize(Instantiate(cardData.cardPrefab));
                
            }
        }
    }
}