using SeedHearth.GameMap.Plants;
using SeedHearth.Input.MouseController;
using UnityEngine;

namespace SeedHearth.Cards.Abilities
{
    public class PlantsCrop : TargetableCardAbility
    {
        [SerializeField] private Plant plantPrefab;

        protected override bool ValidTarget(HoverData targetData)
        {
            foreach (PlantableTile tile in targetData.tiles)
            {
                if (tile != null && !tile.HasPlant() &&
                    (tile.GetState() == PlantableTileStates.Tilled ||
                     tile.GetState() == PlantableTileStates.Watered))
                {
                    return true;
                }
            }

            return false;
        }

        protected override void ApplyAbility(HoverData targetData)
        {
            foreach (PlantableTile tile in targetData.tiles)
            {
                tile.AddPlant(Instantiate(plantPrefab, tile.transform));
            }
        }
    }
}