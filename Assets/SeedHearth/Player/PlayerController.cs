using UnityEngine;

namespace SeedHearth.Player
{
    public class PlayerController : MonoBehaviour
    {
        public static PlayerController S;

        private void Awake()
        {
            if (S == null)
            {
                S = this;
                DontDestroyOnLoad(S);
            }
            else
            {
                Debug.LogError("Multiple PLayerControllers");
                Destroy(this);
                enabled = false;
            }
        }
    }
}