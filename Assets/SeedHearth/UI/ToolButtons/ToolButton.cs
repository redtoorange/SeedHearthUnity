using SeedHearth.Cards;
using SeedHearth.Managers;
using UnityEngine;

namespace SeedHearth.UI.ToolButtons
{
    public class ToolButton : MonoBehaviour
    {
        [SerializeField] private Card toolCardPrefab;
        private CardManager cardManager;

        private void Start()
        {
            cardManager = FindFirstObjectByType<CardManager>();
        }

        public void OnToolButtonPressed()
        {
            Card newCard = cardManager.SpawnNewCard(toolCardPrefab);
            cardManager.AddNewCard(
                newCard
            );
        }
    }
}