using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace SeedHearth.ResourceDisplay
{
    public class ResourceClicker : MonoBehaviour, IPointerClickHandler
    {
        private TMP_Text resourceText;

        [SerializeField] private string valueLabel = "";
        [SerializeField] private int initialValue = 0;
        [SerializeField] private int minValue = 0;
        [SerializeField] private int maxValue = 100;

        private int value;

        private void Start()
        {
            resourceText = GetComponentInChildren<TMP_Text>();

            value = initialValue;
            resourceText.text = $"{valueLabel}\n{value}";
        }

        public void ChangeResource(int deltaAmount)
        {
            value = Mathf.Clamp(value + deltaAmount, minValue, maxValue);
            resourceText.text = $"{valueLabel}\n{value}";
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Left)
            {
                ChangeResource(1);
            }
            else if (eventData.button == PointerEventData.InputButton.Right)
            {
                ChangeResource(-1);
            }
        }
    }
}