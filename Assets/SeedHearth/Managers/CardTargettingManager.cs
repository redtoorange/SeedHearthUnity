using SeedHearth.Input.MouseController;
using UnityEngine;

namespace SeedHearth.Managers
{
    public class CardTargettingManager : MonoBehaviour
    {
        [SerializeField] private SelectionGridDisplayController gridDisplayController;

        public void ShowSelectionSquare(SelectionSquareType selectionSquareType)
        {
            gridDisplayController.ShowSelectionSquare(selectionSquareType);
        }

        public void HideSelectionSquare()
        {
            gridDisplayController.ShowSelectionSquare(SelectionSquareType.None);
        }

        public HoverData GetHoverData()
        {
            return gridDisplayController.GetHoverData();
        }
    }
}