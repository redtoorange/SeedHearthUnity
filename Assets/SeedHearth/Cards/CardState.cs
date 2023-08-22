using System;

namespace SeedHearth.Cards
{
    [Serializable]
    public enum CardState
    {
        None,
        InDrawPile,
        InHand,
        InDiscardPile,
        BeingDragged,
        BeingCasting,
        BeingSold,
    }
}