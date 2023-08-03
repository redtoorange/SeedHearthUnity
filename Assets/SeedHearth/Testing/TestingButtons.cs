using SeedHearth.Cards;
using SeedHearth.Managers;
using SeedHearth.Player;
using UnityEngine;
using UnityEngine.Serialization;

namespace SeedHearth.Testing
{
    public class TestingButtons : MonoBehaviour
    {
        [SerializeField] private int handSize = 4;
        [SerializeField] private int staminaAmount = 4;

        [SerializeField] private CardManager cardManager;
        [SerializeField] private ResourceManager resourceManager;
        [SerializeField] private GrowthManager growthManager;

        public void DrawNewHand()
        {
            cardManager.DiscardHand();
            for (int i = 0; i < handSize; i++)
            {
                cardManager.DrawCard();
            }
            
            resourceManager.SetStamina(staminaAmount);
            growthManager.GrowPlants();
        }
    }
}