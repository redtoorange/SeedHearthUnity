using SeedHearth.MouseController;
using UnityEngine;

namespace SeedHearth.Input.MouseController
{
    public class SelectionGridDisplayController : MonoBehaviour
    {
        [SerializeField] private SelectionSquareController selectionSquare1x1;
        [SerializeField] private SelectionSquareController selectionSquare2x2;
        [SerializeField] private SelectionSquareController selectionSquare3x3;

        private SelectionSquareController currentSquare = null;
        private SelectionSquareType currentState = SelectionSquareType.None;
        private Vector2 cachedPosition;

        private void Start()
        {
            selectionSquare1x1.gameObject.SetActive(false);
            selectionSquare2x2.gameObject.SetActive(false);
            selectionSquare3x3.gameObject.SetActive(false);
        }

        public void UpdateDisplay(Vector2 worldPosition)
        {
            cachedPosition = worldPosition;
            if (currentState != SelectionSquareType.None && currentSquare != null)
            {
                currentSquare.UpdateSquare(cachedPosition);
            }
        }

        public void ShowSelectionSquare(SelectionSquareType squareType)
        {
            if (squareType == currentState) return;
            currentState = squareType;

            if (currentSquare != null)
            {
                currentSquare.gameObject.SetActive(false);
            }

            switch (currentState)
            {
                case SelectionSquareType.None:
                    currentSquare = null;
                    return;

                case SelectionSquareType.Size_1x1:
                    currentSquare = selectionSquare1x1;
                    break;
                case SelectionSquareType.Size_2x2:
                    currentSquare = selectionSquare2x2;
                    break;
                case SelectionSquareType.Size_3x3:
                    currentSquare = selectionSquare3x3;
                    break;
            }

            currentSquare.gameObject.SetActive(true);
            UpdateDisplay(cachedPosition);
        }

        public SelectionSquareType GetCurrentState() => currentState;

        public HoverData GetHoverData()
        {
            if (currentState == SelectionSquareType.None || currentSquare == null) return null;

            return currentSquare.GetHoverData();
        }
    }
}