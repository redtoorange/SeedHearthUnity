using System;
using UnityEngine;

namespace SeedHearth.Cards.Abilities
{
    public delegate void CastCallback();

    public abstract class CardAbility : MonoBehaviour
    {
        [SerializeField] private int order = -1;
        public int GetOrder => order;

        protected Card parentCard;
        
        private void Start()
        {
            parentCard = GetComponentInParent<Card>();
        }

        public abstract void Cast(CardCastingContext context, CastCallback callback);

        public virtual void CancelCasting()
        {
        }
    }
}