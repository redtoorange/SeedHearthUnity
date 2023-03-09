using System;
using UnityEngine;

namespace SeedHearth.Cards
{
    [CreateAssetMenu(fileName = "CardData", menuName = "Card Data", order = 0)]
    [Serializable]
    public class CardData : ScriptableObject
    {
        [SerializeField] public string cardTitle;
        [SerializeField][Multiline(8)] public string cardDescription;
        [SerializeField] public Color cardBackgroundColor;
        [SerializeField] public int staminaCost = 0;
    }
}