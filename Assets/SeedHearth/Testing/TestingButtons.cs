using SeedHearth.Cards;
using SeedHearth.ResourceDisplay;
using UnityEngine;

namespace SeedHearth.Testing
{
    public class TestingButtons : MonoBehaviour
    {
        [SerializeField] private int handSize = 4;
        [SerializeField] private int staminaAmount = 4;
        
        // [SerializeField] private ResourceClicker staminaClicker;
        [SerializeField] private CardController cardController;
        
        public void DrawNewHand()
        {
            // staminaClicker.SetAmount(staminaAmount);
            cardController.DiscardHand();

            for (int i = 0; i < handSize; i++)
            {
                cardController.DrawCard();
            }
        }
    }
}