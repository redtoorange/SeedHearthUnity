using UnityEngine;

namespace SeedHearth.Cards
{
    [CreateAssetMenu(fileName = "CardProperty", menuName = "Card Property", order = 1)]
    public class CardProperty : ScriptableObject
    {
        public string name;
        public string value;
    }
}