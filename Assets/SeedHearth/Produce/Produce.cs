using SeedHearth.Cards;
using SeedHearth.Managers;
using UnityEngine;

namespace SeedHearth.Produce
{
    public class Produce : MonoBehaviour
    {
        [SerializeField] private Card produceCardPrefab;

        private CardManager cardManager;

        private void Start()
        {
            cardManager = FindFirstObjectByType<CardManager>();
        }

        public void PickUpProduce()
        {
            Card newCard = cardManager.SpawnNewCard(produceCardPrefab);
            cardManager.AddNewCard(
                newCard
            );
            Destroy(gameObject);
        }
    }
}