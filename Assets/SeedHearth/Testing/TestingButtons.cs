using SeedHearth.Cards;
using UnityEngine;
using UnityEngine.Serialization;

namespace SeedHearth.Testing
{
    public class TestingButtons : MonoBehaviour
    {
        [SerializeField] private int handSize = 4;
        [SerializeField] private int staminaAmount = 4;

        [FormerlySerializedAs("cardController")] [SerializeField] private CardManager cardManager;

        public void DrawNewHand()
        {
            cardManager.DiscardHand();

            for (int i = 0; i < handSize; i++)
            {
                cardManager.DrawCard();
            }
        }
    }
}