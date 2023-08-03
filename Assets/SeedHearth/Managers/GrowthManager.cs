using System.Collections.Generic;
using SeedHearth.Plants;
using UnityEngine;

namespace SeedHearth.Managers
{
    public class GrowthManager : MonoBehaviour
    {
        private PlantManager plantManager;

        private void OnEnable()
        {
            TurnManager.onBeginTurn += GrowPlants;
        }

        private void OnDisable()
        {
            TurnManager.onBeginTurn -= GrowPlants;
        }

        private void Start()
        {
            plantManager = FindFirstObjectByType<PlantManager>();
        }

        public void GrowPlants()
        {
            List<Plant> plants = plantManager.GetManagedPlants();
            foreach (Plant plant in plants)
            {
                plant.Grow(1);
            }
        }
    }
}