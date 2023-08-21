using System.Collections.Generic;
using SeedHearth.Plants;
using UnityEngine;

namespace SeedHearth.Managers.ScriptableObjects
{
    [CreateAssetMenu(fileName = "Tile Manager", menuName = "Managers/TileManager")]
    public class TileManager : ScriptableObject
    {
        private List<PlantableTile> managedTiles = new List<PlantableTile>();

        public void AddTile(PlantableTile newTile)
        {
            if (!managedTiles.Contains(newTile))
            {
                managedTiles.Add(newTile);
            }
        }

        public void RemoveTile(PlantableTile tileToRemove)
        {
            if (managedTiles.Contains(tileToRemove))
            {
                managedTiles.Remove(tileToRemove);
            }
        }

        public List<PlantableTile> GetAllTiles() => managedTiles;
    }
}