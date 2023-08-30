using SeedHearth.GameMap.Plants;
using SeedHearth.Input.MouseController;

namespace SeedHearth.Cards.Abilities
{
    public class WatersCrop : TargetableCardAbility
    {
        protected override bool ValidTarget(HoverData targetData)
        {
            foreach (PlantableTile tile in targetData.tiles)
            {
                if (tile != null && tile.GetState() == PlantableTileStates.Tilled)
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
                tile.WaterTile();
            }
        }
    }
}