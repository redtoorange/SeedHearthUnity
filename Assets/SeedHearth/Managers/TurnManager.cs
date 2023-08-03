using System;
using UnityEngine;

namespace SeedHearth.Managers
{
    public class TurnManager : MonoBehaviour
    {

        public static Action onEndTurn;
        public static Action onBeginTurn;

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
