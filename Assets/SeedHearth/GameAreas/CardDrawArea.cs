using UnityEngine;

namespace SeedHearth.GameAreas
{
    public class CardDrawArea : CardArea
    {
        private void Start()
        {
            Debug.Log("DrawAreaCenter: " + GetCenter());
        }
    }
}