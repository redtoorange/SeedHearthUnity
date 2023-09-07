using UnityEngine;
using UnityEngine.InputSystem;

namespace SeedHearth.GameAreas
{
    public class CardHandExpandBox : MonoBehaviour
    {
        [SerializeField] private CardHandMover cardHandArea;

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
                    cardHandArea.Expand();
                }
                else
                {
                    cardHandArea.Retract();
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