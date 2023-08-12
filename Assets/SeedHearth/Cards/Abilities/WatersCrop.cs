using SeedHearth.Cards.Data.Abilities;
using SeedHearth.Plants;

namespace SeedHearth.Cards.Abilities
{
    public class WatersCrop : TargetableCardAbility
    {
        protected override void ApplyAbility(HoveredTargets target)
        {
            target.tile.WaterTile();
        }

        protected override bool ValidTarget(HoveredTargets target)
        {
            if (target.tile != null && target.tile.GetState() == PlantableTileStates.Tilled)
            {
                return true;
            }

            return false;
        }
    }
}