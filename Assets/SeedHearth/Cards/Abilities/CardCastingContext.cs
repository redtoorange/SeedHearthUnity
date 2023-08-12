using SeedHearth.Cards.Data;
using SeedHearth.Managers;

namespace SeedHearth.Cards.Abilities
{
    public struct CardCastingContext
    {
        public CardManager cardManager;
        public ResourceManager playerResourceManager;
        public CardData cardData;
    }
}