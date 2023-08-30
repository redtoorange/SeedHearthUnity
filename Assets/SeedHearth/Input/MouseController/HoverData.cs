using System.Collections.Generic;
using SeedHearth.GameMap.Plants;

namespace SeedHearth.Input.MouseController
{
    public class HoverData
    {
        public List<PlantableTile> tiles;
        public List<Plant> plants;

        public HoverData(List<PlantableTile> tiles, List<Plant> plants)
        {
            this.tiles = tiles;
            this.plants = plants;
        }
    }
}