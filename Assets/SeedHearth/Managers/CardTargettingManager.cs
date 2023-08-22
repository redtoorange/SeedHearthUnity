using SeedHearth.Plants;
using UnityEngine;

namespace SeedHearth.Managers
{
    public class CardTargettingManager : MonoBehaviour
    {
        // [SerializeField] private CardTargettingArrow cardTargeting;
        [SerializeField] private MouseController.MouseController mouseController;

        public void EnableArrow()
        {
            // cardTargeting.gameObject.SetActive(true);
        }

        public void DisableArrow()
        {
            // cardTargeting.gameObject.SetActive(false);
        }

        public PlantableTile GetHoveredTile() => mouseController.GetHoveredTile();
        public Plant GetHoveredPlant() => mouseController.GetHoveredPlant();
        public Produce.Produce GetHoveredProduce() => mouseController.GetHoveredProduce();
    }
}