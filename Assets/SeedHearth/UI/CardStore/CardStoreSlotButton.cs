using SeedHearth.Data;
using TMPro;
using UnityEngine.UI;

namespace SeedHearth.UI.CardStore
{
    public class CardStoreSlotButton : Button
    {
        private TMP_Text buttonLabel;
        private CardData cardData;

        public void Initialize(CardData getCardData)
        {
            buttonLabel = GetComponentInChildren<TMP_Text>();
            cardData = getCardData;
            buttonLabel.text = $"{cardData.basePurchaseValue}";
        }
    }
}