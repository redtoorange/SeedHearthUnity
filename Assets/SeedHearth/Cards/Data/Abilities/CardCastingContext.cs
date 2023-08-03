using SeedHearth.Managers;

namespace SeedHearth.Cards.Data.Abilities
{
    public struct CardCastingContext
    {
       public CardManager cardManager;
       public ResourceManager playerResourceManager;
       public CardData cardData;
    }
}