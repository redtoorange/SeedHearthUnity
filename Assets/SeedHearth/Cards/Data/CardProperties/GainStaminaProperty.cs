using UnityEngine;

namespace SeedHearth.Cards.Data.CardProperties
{
    [CreateAssetMenu(fileName = "GainStaminaProperty", menuName = "Properties/GainStamina")]
    public class GainStaminaProperty : CardProperty
    {
        public int amount;
    }
}