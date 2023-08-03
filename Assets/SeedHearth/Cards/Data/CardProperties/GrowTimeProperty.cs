using UnityEngine;

namespace SeedHearth.Cards.Data.CardProperties
{
    [CreateAssetMenu(fileName = "GrowTime", menuName = "Properties/GrowTime")]
    public class GrowTimeProperty : CardProperty
    {
        public int days;
    }
}