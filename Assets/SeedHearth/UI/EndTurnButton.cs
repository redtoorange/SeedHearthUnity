using SeedHearth.Managers;
using UnityEngine;

namespace SeedHearth.UI
{
    public class EndTurnButton : MonoBehaviour
    {
        private TurnManager turnManager;

        private void Start()
        {
            turnManager = FindFirstObjectByType<TurnManager>();
        }

        public void OnEndTurnButtonPressed()
        {
            turnManager.TriggerEndTurn();
        }
    }
}