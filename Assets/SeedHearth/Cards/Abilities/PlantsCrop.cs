using SeedHearth.Plants;
using UnityEngine;

namespace SeedHearth.Cards.Abilities
{
    public class PlantsCrop : TargetableCardAbility
    {
        [SerializeField] private Plant plantPrefab;

        protected override bool ValidTarget(HoveredTargets target)
        {
            if (target.tile != null && target.plant == null &&
                (target.tile.GetState() == PlantableTileStates.Tilled ||
                 target.tile.GetState() == PlantableTileStates.Watered))
            {
                return true;
            }

            return false;
        }

        protected override void ApplyAbility(HoveredTargets target)
        {
            PlantableTile tile = target.tile;
            tile.AddPlant(Instantiate(plantPrefab, tile.transform));
        }
    }
}