using SeedHearth.Cards.Abilities;
using SeedHearth.Input.MouseController;
using UnityEngine;

namespace SeedHearth.Managers
{
    public class CardTileValidatorService : Singleton<CardTileValidatorService>
    {
        private TargetableCardAbility tagetValidator;
        
        public void SetValidator(TargetableCardAbility targetableCardAbility)
        {
            tagetValidator = targetableCardAbility;
        }

        public void Reset()
        {
            tagetValidator = null;
        }

        public bool IsValid(HoverData hoverData)
        {
            if (tagetValidator != null)
            {
                return tagetValidator.ValidTarget(hoverData);
            }

            return false;
        }
    }
}