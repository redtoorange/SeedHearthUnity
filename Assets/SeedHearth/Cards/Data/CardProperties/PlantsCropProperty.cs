using UnityEngine;

namespace SeedHearth.Cards.Data.CardProperties
{
    [CreateAssetMenu(fileName = "PlantsCropProperty", menuName = "Properties/PlantsCrop")]
    public class PlantsCropProperty : CardProperty
    {
        public GameObject cropPrefab;
    }
}