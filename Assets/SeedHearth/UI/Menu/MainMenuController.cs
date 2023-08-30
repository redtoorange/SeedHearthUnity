using UnityEngine;
using UnityEngine.SceneManagement;

namespace SeedHearth.UI.Menu
{
    public class MainMenuController : MonoBehaviour
    {
        public void OnStartGamePressed()
        {
            SceneManager.LoadScene(1);
        }

        public void OnQuitGamePressed()
        {
            Application.Quit();
        }
    }
}