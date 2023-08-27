using System.Collections.Generic;
using SeedHearth.Plants;

namespace SeedHearth.MouseController
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