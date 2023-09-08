using UnityEngine;
using UnityEngine.InputSystem;

namespace SeedHearth.GameAreas
{
    public class UIMoverArea : MonoBehaviour
    {
        [SerializeField] private UIMover uiArea;

        private Camera camera;
        private RectTransform rectTransform;
        private bool mouseInsideLastFrame = false;

        private void Start()
        {
            rectTransform = GetComponent<RectTransform>();
            camera = Camera.main;
        }

        private void Update()
        {
            bool mouseInsideThisFrame = MouseInsideBounds();
            if (mouseInsideLastFrame != mouseInsideThisFrame)
            {
                mouseInsideLastFrame = mouseInsideThisFrame;
                if (mouseInsideThisFrame)
                {
                    uiArea.Expand();
                }
                else
                {
                    uiArea.Retract();
                }
            }
        }

        private bool MouseInsideBounds()
        {
            return RectTransformUtility.RectangleContainsScreenPoint(
                rectTransform,
                Mouse.current.position.ReadValue(),
                camera
            );
        }
    }
}