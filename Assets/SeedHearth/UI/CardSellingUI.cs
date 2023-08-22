using SeedHearth.Cards;
using UnityEngine;
using UnityEngine.UI;

namespace SeedHearth.UI
{
    public class CardSellingUI : MonoBehaviour
    {
        [SerializeField] private RectTransform cardHolder;
        [SerializeField] private RectTransform cardSellingWindow;

        private void Start()
        {
        }

        public void SetVisible(bool shouldBeVisible)
        {
            cardSellingWindow.gameObject.SetActive(shouldBeVisible);
        }

        public void ToggleVisibility()
        {
            cardSellingWindow.gameObject.SetActive(!cardSellingWindow.gameObject.activeSelf);
        }

        public void AddCard(Card soldCard)
        {
            soldCard.transform.parent = cardHolder;
            // GridLayoutGroup glg = cardHolder.GetComponent<GridLayoutGroup>();
            // glg.SetLayoutHorizontal();
            // glg.SetLayoutVertical();
        }
    }
}