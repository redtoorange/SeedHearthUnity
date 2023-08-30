using UnityEngine;

namespace SeedHearth.Input.PlayerInput
{
    [CreateAssetMenu(menuName = "Managers/Input Manager")]
    public class PlayerInputManager : ScriptableObject
    {
        public PlayerInputActions playerInputActions;

        private void OnEnable()
        {
            playerInputActions = new PlayerInputActions();
            playerInputActions.Enable();
        }

        private void OnDisable()
        {
            playerInputActions.Disable();
            playerInputActions = null;
        }
    }
}