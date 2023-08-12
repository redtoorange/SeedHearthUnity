using System;
using SeedHearth.Cards.Abilities;
using UnityEngine;

namespace SeedHearth.Cards.Data.Abilities
{
    public abstract class TargetableCardAbility : CardAbility
    {
        [SerializeField] private LayerMask targetLayerMask;
        protected CardCastingContext context;
        protected CastCallback callback;
        private CardTargetingController cardTargetingController;
        
        private void Start()
        {
            cardTargetingController = GetComponent<CardTargetingController>();
            cardTargetingController.OnTargetSelected += OnTargetSelected;
        }

        public override void Cast(CardCastingContext context, CastCallback callback)
        {
            this.context = context;
            this.callback = callback;
            cardTargetingController.StartSelectingTarget();
        }

        protected void OnTargetSelected(HoveredTargets target)
        {
            if (ValidTarget(target))
            {
                ApplyAbility(target);
                FinishedCasting();                
            }
        }

        protected void FinishedCasting()
        {
            cardTargetingController.StopSelectingTarget();
            callback();
        }

        public override void CancelCasting()
        {
            base.CancelCasting();
            cardTargetingController.StopSelectingTarget();
        }


        protected abstract bool ValidTarget(HoveredTargets target);
        protected abstract void ApplyAbility(HoveredTargets target);
    }
}