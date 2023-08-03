using System.Collections.Generic;
using SeedHearth.Cards.Areas;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace SeedHearth.MouseController
{
    public class MouseEnterDetector : MonoBehaviour
    {
        private GraphicRaycaster raycaster;
        private EventSystem eventSystem;
        private List<RaycastResult> results;
        private PointerEventData pointerEventData;

        private void Start()
        {
            eventSystem = EventSystem.current;
            raycaster = GetComponentInParent<GraphicRaycaster>();

            results = new List<RaycastResult>();
            pointerEventData = new PointerEventData(eventSystem);
        }

        /**
         * Detect the first area under the mouse
         */
        public CardArea DetectCardArea(out Vector2 releasePosition)
        {
            results.Clear();
            pointerEventData.position = Mouse.current.position.ReadValue();
            eventSystem.RaycastAll(pointerEventData, results);

            // raycaster.Raycast();
            for (int i = 0; i < results.Count; i++)
            {
                RaycastResult result = results[i];
                GameObject go = result.gameObject;
                if (go.TryGetComponent(out CardArea area))
                {
                    releasePosition = pointerEventData.position;
                    return area;
                }
            }

            releasePosition = Vector2.zero;
            return null;
        }
    }
}