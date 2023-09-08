using SeedHearth.Cards.Controllers;
using SeedHearth.Input.MouseController;
using UnityEngine;

namespace SeedHearth.Cards.Abilities
{
    public abstract class TargetableCardAbility : CardAbility
    {
        [SerializeField] private SelectionSquareType selectionSquareType = SelectionSquareType.Size_1x1;
        [SerializeField] private LayerMask targetLayerMask;
        protected CardCastingContext context;
        protected CastCallback callback;
        private CardTargetingController cardTargetingController;

        private void Start()
        {
            cardTargetingController = GetComponentInParent<CardTargetingController>();
            cardTargetingController.OnTargetSelected += OnTargetSelected;
        }

        public override void Cast(CardCastingContext context, CastCallback callback)
        {
            this.context = context;
            this.callback = callback;

            cardTargetingController.StartSelectingTarget(selectionSquareType);
        }

        protected void OnTargetSelected(HoverData targetData)
        {
            if (ValidTarget(targetData))
            {
                ApplyAbility(targetData);
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


        protected abstract bool ValidTarget(HoverData targetData);
        protected abstract void ApplyAbility(HoverData targetData);
    }
}