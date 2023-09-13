using UnityEngine;

namespace SeedHearth.Managers
{
    public abstract class Singleton<T> : MonoBehaviour where T : Singleton<T>
    {
        [SerializeField] private bool persistent = true;
        public static T S;


        private void Awake()
        {
            if (S == null)
            {
                S = GetComponent<T>();
                if (persistent)
                {
                    DontDestroyOnLoad(gameObject);
                }
            }
            else
            {
                Debug.LogError("Multiple instance of the same singleton");
                Destroy(this);
                enabled = false;
            }
        }
    }
}