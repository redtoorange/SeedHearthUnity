using SeedHearth.Data;
using SeedHearth.Input;
using SeedHearth.Input.MouseController;
using UnityEngine;

namespace SeedHearth.Managers
{
    public class CardTargettingManager : Singleton<CardTargettingManager>
    {
        [SerializeField] private MouseToolTipDisplay mouseToolTipDisplay;
        [SerializeField] private SelectionGridDisplayController gridDisplayController;

        public void ShowSelectionSquare(SelectionSquareType selectionSquareType, CardData cardData)
        {
            gridDisplayController.ShowSelectionSquare(selectionSquareType);
            mouseToolTipDisplay.ShowToolTip(cardData.cardToolTipSprite);
        }

        public void HideSelectionSquare()
        {
            gridDisplayController.ShowSelectionSquare(SelectionSquareType.None);
            mouseToolTipDisplay.HideIcon();
        }

        public HoverData GetHoverData()
        {
            return gridDisplayController.GetHoverData();
        }
    }
}