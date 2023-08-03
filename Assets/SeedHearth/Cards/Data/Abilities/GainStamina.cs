using SeedHearth.Cards.Data.CardProperties;
using UnityEngine;

namespace SeedHearth.Cards.Data.Abilities
{
    [CreateAssetMenu(fileName = "GainStamina", menuName = "Abilities/GainStamina")]
    public class GainStamina : CardAbility
    {
        public override void Cast(CardCastingContext context, CastCallback callback)
        {
            Debug.Log("Casting GainStamina");
            foreach (CardProperty property in context.cardData.cardProperties)
            {
                if (property is GainStaminaProperty staminaProperty)
                {
                    context.playerResourceManager.AddStamina(staminaProperty.amount);
                    callback();
                    return;
                }
            }

            Debug.LogError("Missing GainStaminaProperty on " + context.cardData.cardTitle);
        }
    }
}