using System;
using SeedHearth.Managers;
using SeedHearth.Plants;
using UnityEngine;
using UnityEngine.InputSystem;

namespace SeedHearth.Cards
{
    public class HoveredTargets
    {
        public PlantableTile tile;
        public Plant plant;
        public Produce.Produce produce;

        public HoveredTargets(PlantableTile tile, Plant plant, Produce.Produce produce)
        {
            this.tile = tile;
            this.plant = plant;
            this.produce = produce;
        }
    }


    public class CardTargetingController : MonoBehaviour
    {
        public Action<HoveredTargets> OnTargetSelected;

        private CardTargettingManager cardTargetingManager;
        private bool selectingTarget = false;

        private void Start()
        {
            cardTargetingManager = FindFirstObjectByType<CardTargettingManager>();
        }

        private void Update()
        {
            if (selectingTarget && Mouse.current.leftButton.wasPressedThisFrame)
            {
                OnTargetSelected?.Invoke(new HoveredTargets(
                    cardTargetingManager.GetHoveredTile(),
                    cardTargetingManager.GetHoveredPlant(),
                    cardTargetingManager.GetHoveredProduce()
                ));
            }
        }

        public void StartSelectingTarget()
        {
            selectingTarget = true;
            cardTargetingManager.EnableArrow();
        }

        public void StopSelectingTarget()
        {
            selectingTarget = false;
            cardTargetingManager.DisableArrow();
        }
    }
}