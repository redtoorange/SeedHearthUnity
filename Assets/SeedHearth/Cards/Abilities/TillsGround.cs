using SeedHearth.Cards.Data.Abilities;
using SeedHearth.Plants;

namespace SeedHearth.Cards.Abilities
{
    public class TillsGround : TargetableCardAbility
    {
        protected override bool ValidTarget(HoveredTargets target)
        {
            if (target.tile != null && target.tile.GetState() == PlantableTileStates.Untilled)
            {
                return true;
            }

            return false;
        }

        protected override void ApplyAbility(HoveredTargets target)
        {
            target.tile.TillTile();
        }
    }
}