using SeedHearth.Cards.Abilities;
using UnityEngine;

namespace SeedHearth.Cards.Data.Abilities
{
    public delegate void CastCallback();

    public abstract class CardAbility : MonoBehaviour
    {
        public abstract void Cast(CardCastingContext context, CastCallback callback);

        public virtual void CancelCasting()
        {
        }
    }
}