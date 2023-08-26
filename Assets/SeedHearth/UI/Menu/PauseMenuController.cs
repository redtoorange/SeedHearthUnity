using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace SeedHearth.Menu
{
    public class PauseMenuController : MonoBehaviour
    {
        [SerializeField] private GameObject menuOverlay;

        private bool showingMenu = false;

        private void Update()
        {
            if (Keyboard.current.escapeKey.wasPressedThisFrame)
            {
                ToggleMenu();
            }
        }

        public void ToggleMenu()
        {
            showingMenu = !showingMenu;
            menuOverlay.SetActive(showingMenu);
        }

        public void OnResumePressed()
        {
            ToggleMenu();
        }

        public void OnRestartPressed()
        {
            SceneManager.LoadScene(1);
        }

        public void OnMainMenuPressed()
        {
            SceneManager.LoadScene(0);
        }
        
        public void OnQuitPressed()
        {
            Application.Quit();
        }
    }
}