using System;
using SeedHearth.Cards.Data;
using SeedHearth.Input.MouseController;
using SeedHearth.Managers;
using SeedHearth.MouseController;
using UnityEngine.InputSystem;

namespace SeedHearth.Cards
{
    public class CardTargetingController : CardController
    {
        public Action<HoverData> OnTargetSelected;
        private CardTargettingManager cardTargetingManager;
        private bool selectingTarget = false;

        public override void Initialize(Card pCard, CardData pCardData)
        {
            base.Initialize(pCard, pCardData);
            cardTargetingManager = FindFirstObjectByType<CardTargettingManager>();
        }

        private void Update()
        {
            if (selectingTarget && Mouse.current.leftButton.wasPressedThisFrame)
            {
                OnTargetSelected?.Invoke(cardTargetingManager.GetHoverData());
            }
        }

        public void StartSelectingTarget(SelectionSquareType selectionSquareType)
        {
            selectingTarget = true;
            cardTargetingManager.ShowSelectionSquare(selectionSquareType);
        }

        public void StopSelectingTarget()
        {
            selectingTarget = false;
            cardTargetingManager.HideSelectionSquare();
        }
    }
}