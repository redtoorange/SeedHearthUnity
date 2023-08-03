using System.Collections.Generic;
using SeedHearth.Plants;
using UnityEngine;

namespace SeedHearth.Managers
{
    public class PlantManager : MonoBehaviour
    {
        [SerializeField] private List<Plant> managedPlants = new List<Plant>();

        public void AddPlant(Plant plantToAdd)
        {
            if (!managedPlants.Contains(plantToAdd))
            {
                managedPlants.Add(plantToAdd);
            }
        }

        public void RemovePlant(Plant plantToRemove)
        {
            if (managedPlants.Contains(plantToRemove))
            {
                managedPlants.Remove(plantToRemove);
            }
        }

        public List<Plant> GetManagedPlants() => managedPlants;
    }
}