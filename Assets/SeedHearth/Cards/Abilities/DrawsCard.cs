using SeedHearth.Cards.Abilities;
using UnityEngine;

namespace SeedHearth.Cards.Data.Abilities
{
    public class DrawsCard : CardAbility
    {
        [SerializeField] private int amount = 1;

        public override void Cast(CardCastingContext context, CastCallback callback)
        {
            for (int i = 0; i < amount; i++)
            {
                context.cardManager.DrawCard();
            }

            callback();
        }
    }
}