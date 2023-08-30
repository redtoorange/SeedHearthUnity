using SeedHearth.Input.PlayerInput;
using UnityEngine;

namespace SeedHearth.Input.MouseController
{
    public class ProduceHarvestController : MonoBehaviour
    {
        [SerializeField] private PlayerInputManager playerInputManager;
        [SerializeField] private LayerMask plantLayerMask;
        RaycastHit2D[] results = new RaycastHit2D[10];


        public void UpdateDisplay(Vector2 worldPosition)
        {
            if (playerInputManager.playerInputActions.Player.Harvest.WasPerformedThisFrame())
            {
                if (CheckHoveredObject(worldPosition, out IInteractable interactable))
                {
                    interactable.Interact();
                }
            }
        }

        private bool CheckHoveredObject(Vector2 worldPosition, out IInteractable interactable)
        {
            interactable = null;
            ContactFilter2D contactFilter2D = new ContactFilter2D();
            contactFilter2D.layerMask = plantLayerMask;
            int count = Physics2D.Raycast(worldPosition, Vector2.zero, contactFilter2D, results);

            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    if (results[i].collider.TryGetComponent(out IInteractable maybePlant))
                    {
                        interactable = maybePlant;
                        return true;
                    }
                }
            }

            return false;
        }
    }
}