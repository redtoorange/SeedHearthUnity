using SeedHearth.Cards;
using SeedHearth.Input.MouseController;
using SeedHearth.Managers;
using UnityEngine;

namespace SeedHearth.Produce
{
    public class Produce : MonoBehaviour, IInteractable
    {
        [SerializeField] private Card produceCardPrefab;

        private CardManager cardManager;

        private void Start()
        {
            cardManager = FindFirstObjectByType<CardManager>();
        }

        public void Interact()
        {
            PickUpProduce();
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