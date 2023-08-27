using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace SeedHearth.MouseController
{
    [Serializable]
    public enum SelectionSquareType
    {
        None,
        Size_1x1,
        Size_2x2,
        Size_3x3,
    }

    public class MouseController : MonoBehaviour
    {
        private Camera mainCamera;

        [SerializeField] private SelectionSquareController selectionSquare1x1;
        [SerializeField] private SelectionSquareController selectionSquare2x2;
        [SerializeField] private SelectionSquareController selectionSquare3x3;

        [SerializeField] private LayerMask plantLayerMask;

        private SelectionSquareController currentSquare = null;
        private SelectionSquareType currentState = SelectionSquareType.None;

        private void Start()
        {
            mainCamera = Camera.main;

            selectionSquare1x1.gameObject.SetActive(false);
            selectionSquare2x2.gameObject.SetActive(false);
            selectionSquare3x3.gameObject.SetActive(false);
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
            Update();
        }

        private void Update()
        {
            if (currentState != SelectionSquareType.None && currentSquare != null)
            {
                Vector2 mousePosition = Mouse.current.position.ReadValue();
                Vector2 worldPosition = mainCamera.ScreenToWorldPoint(mousePosition);
                currentSquare.UpdateSquare(worldPosition);
            }
        }

        public HoverData GetHoverData()
        {
            if (currentState == SelectionSquareType.None || currentSquare == null) return null;

            return currentSquare.GetHoverData();
        }
    }
}