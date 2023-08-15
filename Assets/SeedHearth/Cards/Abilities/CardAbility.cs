using UnityEngine;

namespace SeedHearth.Cards.Abilities
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