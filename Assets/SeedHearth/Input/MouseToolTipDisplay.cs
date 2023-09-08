using UnityEngine;
using UnityEngine.UI;

namespace SeedHearth.Input
{
    public class MouseToolTipDisplay : MonoBehaviour
    {
        [SerializeField] private Image toolTipIconDisplay;

        private MouseFollow mouseFollow;

        private void Start()
        {
            mouseFollow = GetComponent<MouseFollow>();
            HideIcon();
        }

        public void ShowToolTip(Sprite toolTipIcon)
        {
            toolTipIconDisplay.sprite = toolTipIcon;
            mouseFollow.SetShouldFollow(true);
        }

        public void HideIcon()
        {
            mouseFollow.SetShouldFollow(false);
            transform.position = new Vector3(-1000, -1000, 0);
        }
    }
}