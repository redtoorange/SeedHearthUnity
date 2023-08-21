using System.Collections.Generic;
using SeedHearth.Managers.ScriptableObjects;
using SeedHearth.Plants;
using UnityEngine;

namespace SeedHearth.Managers
{
    public class GrowthManager : MonoBehaviour
    {
        [SerializeField] private PlantManager plantManager;

        private void OnEnable()
        {
            TurnManager.onEndTurn += GrowPlants;
        }

        private void OnDisable()
        {
            TurnManager.onEndTurn -= GrowPlants;
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