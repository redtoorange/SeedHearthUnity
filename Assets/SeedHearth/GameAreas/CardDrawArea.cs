using SeedHearth.Cards;
using UnityEngine;

namespace SeedHearth.GameAreas
{
    public class CardDrawArea : CardArea
    {
        private void Start()
        {
            Debug.Log("DrawAreaCenter: " + GetCenter());
        }

        public override void AddCard(Card cardToAdd)
        {
            cardToAdd.FlipToBack();
            base.AddCard(cardToAdd);
        }
    }
}