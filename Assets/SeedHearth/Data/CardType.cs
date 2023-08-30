using UnityEngine;

namespace SeedHearth.Data
{
    [CreateAssetMenu(fileName = "CardType", menuName = "Card Type", order = 0)]
    public class CardType : ScriptableObject
    {
        public string name;
        public string value;
    }
}