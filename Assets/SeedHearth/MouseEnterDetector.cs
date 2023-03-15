using System.Collections.Generic;
using SeedHearth.Cards;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace SeedHearth
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

        // TODO I don't think we need this every tick
        // private void Update()
        // {
        //     results.Clear();
        //     pointerEventData.position = Mouse.current.position.ReadValue();
        //
        //     raycaster.Raycast(pointerEventData, results);
        //     for (int i = 0; i < results.Count; i++)
        //     {
        //         RaycastResult result = results[i];
        //         GameObject go = result.gameObject;
        //         if (go.TryGetComponent(out CardHandArea handArea))
        //         {
        //             Debug.Log("Over hand area");
        //         }
        //         else if (go.TryGetComponent(out CardDiscardArea discardArea))
        //         {
        //             Debug.Log("Over discard area");
        //         }
        //     }
        // }

        /**
         * Detect the first area under the mouse
         */
        public CardArea DetectCardArea()
        {
            results.Clear();
            pointerEventData.position = Mouse.current.position.ReadValue();

            raycaster.Raycast(pointerEventData, results);
            for (int i = 0; i < results.Count; i++)
            {
                RaycastResult result = results[i];
                GameObject go = result.gameObject;
                if (go.TryGetComponent(out CardArea area))
                {
                    return area;
                }
            }

            return null;
        }
    }
}