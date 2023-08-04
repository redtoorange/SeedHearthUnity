using SeedHearth.Plants;
using UnityEngine;

namespace SeedHearth.Cards.Data.Abilities
{
    public class PlantsCrop : CardAbility
    {
        [SerializeField] private Plant plantPrefab;

        public override void Cast(CardCastingContext context, CastCallback callback)
        {
            callback();
        }
    }
}