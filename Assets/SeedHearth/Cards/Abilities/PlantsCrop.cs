using SeedHearth.GameMap.Plants;
using SeedHearth.Input.MouseController;
using UnityEngine;

namespace SeedHearth.Cards.Abilities
{
    public class PlantsCrop : TargetableCardAbility
    {
        [SerializeField] private Plant plantPrefab;

        public override bool ValidTarget(HoverData targetData)
        {
            foreach (PlantableTile tile in targetData.tiles)
            {
                if (tile != null && tile.CanHavePlant())
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
                if (tile.CanHavePlant())
                {
                    tile.AddPlant(Instantiate(plantPrefab, tile.transform));
                }
            }
        }
    }
}