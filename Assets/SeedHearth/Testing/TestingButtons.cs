using SeedHearth.Cards;
using UnityEngine;

namespace SeedHearth.Testing
{
    public class TestingButtons : MonoBehaviour
    {
        [SerializeField] private int handSize = 4;
        [SerializeField] private int staminaAmount = 4;

        [SerializeField] private CardController cardController;

        public void DrawNewHand()
        {
            cardController.DiscardHand();

            for (int i = 0; i < handSize; i++)
            {
                cardController.DrawCard();
            }
        }
    }
}