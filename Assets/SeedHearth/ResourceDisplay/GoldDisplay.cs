using SeedHearth.Player;
using TMPro;
using UnityEngine;

namespace SeedHearth.ResourceDisplay
{
    public class GoldDisplay : MonoBehaviour
    {
        private TMP_Text resourceText;

        [SerializeField] private string valueLabel = "";


        private void Awake()
        {
            resourceText = GetComponentInChildren<TMP_Text>();
        }

        private void OnEnable()
        {
            ResourceManager.OnGoldChanged += HandleGoldChanged;
        }

        private void OnDisable()
        {
            ResourceManager.OnGoldChanged -= HandleGoldChanged;
        }

        private void HandleGoldChanged(int oldvalue, int newvalue)
        {
            SetAmount(newvalue);
        }


        private void SetAmount(int newAmount)
        {
            resourceText.text = $"{valueLabel}\n{newAmount}";
        }
    }
}