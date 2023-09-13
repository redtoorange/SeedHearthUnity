using System;
using UnityEngine;

namespace SeedHearth.Managers
{
    public class TurnManager : Singleton<TurnManager>
    {
        public static Action onEndTurn;
        public static Action onBeginTurn;

        private void Start()
        {
            TriggerBeginTurn();
        }

        public void TriggerEndTurn()
        {
            onEndTurn?.Invoke();

            TriggerBeginTurn();
        }

        public void TriggerBeginTurn()
        {
            onBeginTurn?.Invoke();
        }
    }
}