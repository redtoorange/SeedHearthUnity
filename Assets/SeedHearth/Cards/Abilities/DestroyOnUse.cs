namespace SeedHearth.Cards.Abilities
{
    public class DestroyOnUse : CardAbility
    {
        public override void Cast(CardCastingContext context, CastCallback callback)
        {
            parentCard.SetAsEphemeral();
            callback();
        }
    }
}