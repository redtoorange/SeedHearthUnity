using UnityEngine;

namespace SeedHearth.Cards
{
    [CreateAssetMenu(fileName = "CardData", menuName = "Card", order = 0)]
    public class CardData : ScriptableObject
    {
        [SerializeField] public string cardTitle;
        [SerializeField][Multiline(8)] public string cardDescription;
        [SerializeField] public Color cardBackgroundColor;
    }
}