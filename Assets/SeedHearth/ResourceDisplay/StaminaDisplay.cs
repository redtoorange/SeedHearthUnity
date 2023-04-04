using SeedHearth.Player;
using TMPro;
using UnityEngine;

namespace SeedHearth.ResourceDisplay
{
    public class StaminaDisplay : MonoBehaviour
    {
        private TMP_Text resourceText;

        [SerializeField] private string valueLabel = "";


        private void Awake()
        {
            resourceText = GetComponentInChildren<TMP_Text>();
        }

        private void OnEnable()
        {
            ResourceManager.OnStaminaChanged += HandleStaminaChanged;
        }

        private void OnDisable()
        {
            ResourceManager.OnStaminaChanged -= HandleStaminaChanged;
        }

        private void HandleStaminaChanged(int oldvalue, int newvalue)
        {
            SetAmount(newvalue);
        }


        private void SetAmount(int newAmount)
        {
            resourceText.text = $"{valueLabel}\n{newAmount}";
        }
    }
}