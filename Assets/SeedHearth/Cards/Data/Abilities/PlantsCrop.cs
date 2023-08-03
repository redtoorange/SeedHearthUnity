using SeedHearth.Cards.Data.CardProperties;
using UnityEngine;

namespace SeedHearth.Cards.Data.Abilities
{
    [CreateAssetMenu(fileName = "PlantsCrop", menuName = "Abilities/PlantsCrop")]
    public class PlantsCrop : CardAbility
    {
        public override void Cast(CardCastingContext context, CastCallback callback)
        {
            Debug.Log("Casting PlantsCrop");
            foreach (CardProperty property in context.cardData.cardProperties)
            {
                if (property is PlantsCropProperty cropProperty)
                {
                    callback();
                    return;
                }
            }

            Debug.LogError("Missing PlantsCrop on " + context.cardData.cardTitle);
        }
    }
}