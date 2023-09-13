using UnityEngine;

namespace SeedHearth.Managers
{
    public class ResourceManager : Singleton<ResourceManager>
    {
        public delegate void HandleResourceChanged(int oldValue, int newValue);

        public static event HandleResourceChanged OnStaminaChanged;
        public static event HandleResourceChanged OnGoldChanged;

        [Header("Stamina")]
        [SerializeField] private int stamina;
        [SerializeField] private int startingStamina = 4;
        [SerializeField] private int minStamina = 0;
        [SerializeField] private int maxStamina = 10;

        [Header("Gold")]
        [SerializeField] private int gold;
        [SerializeField] private int startingGold = 0;
        [SerializeField] private int minGold = 0;
        [SerializeField] private int maxGold = 1000;

        private void Start()
        {
            AddStamina(startingStamina);
            AddGold(startingGold);
        }

        private void OnEnable()
        {
            TurnManager.onBeginTurn += ResetStamina;
        }

        private void OnDisable()
        {
            TurnManager.onBeginTurn -= ResetStamina;
        }

        private void ResetStamina()
        {
            SetStamina(startingStamina);
        }

        public int GetGold() => gold;
        public int GetStamina() => stamina;

        public bool HasEnoughStamina(int required)
        {
            return stamina >= required;
        }

        public bool HasEnoughGold(int required)
        {
            return gold >= required;
        }

        public void AddStamina(int amount)
        {
            int oldValue = stamina;
            int newValue = Mathf.Clamp(stamina + amount, minStamina, maxStamina);
            OnStaminaChanged?.Invoke(oldValue, newValue);
            stamina = newValue;
        }

        public void AddGold(int amount)
        {
            int oldValue = gold;
            int newValue = Mathf.Clamp(gold + amount, minGold, maxGold);
            OnGoldChanged?.Invoke(oldValue, newValue);
            gold = newValue;
        }

        public void SetStamina(int staminaAmount)
        {
            int oldValue = stamina;
            int newValue = Mathf.Clamp(staminaAmount, minStamina, maxStamina);
            OnStaminaChanged?.Invoke(oldValue, newValue);
            stamina = newValue;
        }

        public void TakeGold(int purchasePrice)
        {
            int oldValue = gold;
            int newValue = Mathf.Clamp(gold - purchasePrice, minGold, maxGold);
            OnGoldChanged?.Invoke(oldValue, newValue);
            gold = newValue;
        }
    }
}