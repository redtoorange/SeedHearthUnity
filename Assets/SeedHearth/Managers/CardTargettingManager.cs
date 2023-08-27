using SeedHearth.MouseController;
using UnityEngine;

namespace SeedHearth.Managers
{
    public class CardTargettingManager : MonoBehaviour
    {
        [SerializeField] private MouseController.MouseController mouseController;

        public void ShowSelectionSquare(SelectionSquareType selectionSquareType)
        {
            mouseController.ShowSelectionSquare(selectionSquareType);
        }

        public void HideSelectionSquare()
        {
            mouseController.ShowSelectionSquare(SelectionSquareType.None);
        }

        public HoverData GetHoverData()
        {
            return mouseController.GetHoverData();
        }
    }
}