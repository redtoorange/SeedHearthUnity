using SeedHearth.Cards.Abilities;
using UnityEngine;

namespace SeedHearth.Cards.Data.Abilities
{
    public class GainStamina : CardAbility
    {
        [SerializeField] private int amount = 1;

        public override void Cast(CardCastingContext context, CastCallback callback)
        {
            context.playerResourceManager.AddStamina(amount);
            callback();
        }
    }
}