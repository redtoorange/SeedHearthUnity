using UnityEngine;
using UnityEngine.EventSystems;

namespace SeedHearth.Cards
{
    public class CardDrawArea : MonoBehaviour, IPointerClickHandler
    {
        public void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Left)
            {
                Debug.Log("Should Draw Card");
            }
        }
    }
}