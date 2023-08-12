using System;
using System.Collections.Generic;
using SeedHearth.Managers;
using UnityEngine;

namespace SeedHearth.Plants
{
    [Serializable]
    public enum PlantableTileStates
    {
        Untilled,
        Tilled,
        Watered
    }

    public class PlantableTile : MonoBehaviour
    {
        [SerializeField] private TileManager tileManager;
        [SerializeField] private SpriteRenderer groundDisplay;

        [SerializeField] private List<PlantableTileStates> groundTileSpriteStates;
        [SerializeField] private List<Sprite> groundTileSprites;
        private Dictionary<PlantableTileStates, Sprite> mapOfStatesToSprites;

        private PlantableTileStates currentState = PlantableTileStates.Untilled;

        private void Awake()
        {
            mapOfStatesToSprites = new Dictionary<PlantableTileStates, Sprite>();
            for (int i = 0; i < groundTileSpriteStates.Count; i++)
            {
                mapOfStatesToSprites.Add(groundTileSpriteStates[i], groundTileSprites[i]);
            }
        }

        private void OnEnable()
        {
            tileManager.AddTile(this);
        }

        private void OnDisable()
        {
            tileManager.RemoveTile(this);
        }

        public bool TillTile()
        {
            if (currentState == PlantableTileStates.Untilled)
            {
                UpdateState(PlantableTileStates.Tilled);
                return true;
            }

            return false;
        }

        public bool WaterTile()
        {
            if (currentState == PlantableTileStates.Tilled)
            {
                UpdateState(PlantableTileStates.Watered);
                return true;
            }

            return false;
        }

        public bool UnWaterTile()
        {
            if (currentState == PlantableTileStates.Watered)
            {
                UpdateState(PlantableTileStates.Tilled);
                return true;
            }

            return false;
        }

        private void UpdateState(PlantableTileStates newState)
        {
            if (newState == currentState) return;

            currentState = newState;
            groundDisplay.sprite = mapOfStatesToSprites[currentState];
        }

        public PlantableTileStates GetState()
        {
            return currentState;
        }
    }
}