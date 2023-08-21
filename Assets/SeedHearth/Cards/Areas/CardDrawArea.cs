using System;
using UnityEngine;

namespace SeedHearth.Cards.Areas
{
    public class CardDrawArea : CardArea
    {
        private void Start()
        {
            Debug.Log("DrawAreaCenter: " + GetCenter());
        }
    }
}