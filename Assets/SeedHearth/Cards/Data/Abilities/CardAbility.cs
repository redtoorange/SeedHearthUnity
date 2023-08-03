using UnityEngine;

namespace SeedHearth.Cards.Data.Abilities
{
    public delegate void CastCallback();

    public abstract class CardAbility : ScriptableObject
    {
        public abstract void Cast(CardCastingContext context, CastCallback callback);
    }
}