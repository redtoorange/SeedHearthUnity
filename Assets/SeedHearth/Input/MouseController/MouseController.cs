using UnityEngine;
using UnityEngine.InputSystem;

namespace SeedHearth.Input.MouseController
{
    public class MouseController : MonoBehaviour
    {
        private Camera mainCamera;

        private SelectionGridDisplayController selectionGridDisplayController;
        private ProduceHarvestController produceHarvestController;

        private void Start()
        {
            mainCamera = Camera.main;
            selectionGridDisplayController = GetComponent<SelectionGridDisplayController>();
            produceHarvestController = GetComponent<ProduceHarvestController>();
        }

        private void Update()
        {
            Vector2 mousePosition = Mouse.current.position.ReadValue();
            Vector2 worldPosition = mainCamera.ScreenToWorldPoint(mousePosition);

            selectionGridDisplayController.UpdateDisplay(worldPosition);
            if (selectionGridDisplayController.GetCurrentState() == SelectionSquareType.None)
            {
                produceHarvestController.UpdateDisplay(worldPosition);
            }
        }
    }
}