using UnityEngine;

namespace SeedHearth.Managers.ScriptableObjects
{
    [CreateAssetMenu(fileName = "ManagerRegistry", menuName = "Managers/Manager Registry")]
    public class ManagerRegistry : ScriptableObject
    {
        public static ManagerRegistry S;

        [Header("Other Assets")]
        public PlantManager plantManager;
        public TileManager tileManager;

        [Header("Scene Managers")]
        public CardManager cardManager;
        public CardCastingManager castingManager;
        public CardSelectionManager cardSelectionManager;
        public CardTargettingManager cardTargettingManager;
        public DeckManager deckManager;
        public GrowthManager growthManager;
        public ResourceManager resourceManager;
        public TurnManager turnManager;

        private void Awake()
        {
            Debug.Log("ManagerRegistry.Awake()");
        }

        private void OnEnable()
        {
            S = this;
            Debug.Log("ManagerRegistry.OnEnable()");
        }

        private void OnDisable()
        {
            S = null;
            Debug.Log("ManagerRegistry.OnDisable()");
        }
    }
}