using System;
using System.Collections.Generic;
using SeedHearth.Input.MouseController;
using SeedHearth.Managers.ScriptableObjects;
using UnityEngine;
using Random = UnityEngine.Random;

namespace SeedHearth.Plants
{
    public class Plant : MonoBehaviour, IInteractable
    {
        [Header("Visuals")]
        [SerializeField] private List<Sprite> plantSpriteTops;
        [SerializeField] private List<Sprite> plantSpriteBottoms;
        [SerializeField] private SpriteRenderer plantVisualTop;
        [SerializeField] private SpriteRenderer plantVisualBottom;

        [Header("Grow")]
        [SerializeField] private int daysRequiredToGrow = 4;
        [SerializeField] private int daysToGrownSoFar = 0;

        [Header("Harvest")]
        [SerializeField] private PlantManager plantManager;
        [SerializeField] private bool harvestable = false;
        [SerializeField] private Produce.Produce producePrefab;

        private PlantableTile owningTile;

        private void Start()
        {
            if (plantSpriteTops.Count != plantSpriteBottoms.Count)
            {
                Debug.LogError("Plant Sprite Lists must contain the same number of items");
            }

            if (daysRequiredToGrow < plantSpriteBottoms.Count - 1)
            {
                Debug.LogError("Plant Sprites maybe be inaccesible due to growth time being shorter than image count");
            }

            UpdatePlantVisuals();
        }

        private void OnEnable()
        {
            plantManager.AddPlant(this);
        }

        private void OnDisable()
        {
            Debug.Log("Disabling plant");
            plantManager.RemovePlant(this);
        }

        public void Interact()
        {
            TryConvertToProduce();
        }

        public void TryConvertToProduce()
        {
            if (!harvestable) return;

            Produce.Produce spawnedProduce = Instantiate(
                producePrefab,
                transform.position,
                Quaternion.identity,
                transform.parent
            );
            owningTile.RemovePlant();
            Destroy(gameObject);
        }

        private void UpdatePlantVisuals()
        {
            if (daysToGrownSoFar == 0)
            {
                // Day 0 is always seeds
                plantVisualTop.sprite = plantSpriteTops[0];
                plantVisualBottom.sprite = plantSpriteBottoms[0];
            }
            else if (daysToGrownSoFar == daysRequiredToGrow)
            {
                // Day [daysRequiredToGrow] is always the last image
                int last = plantSpriteTops.Count - 1;
                plantVisualTop.sprite = plantSpriteTops[last];
                plantVisualBottom.sprite = plantSpriteBottoms[last];
            }
            else
            {
                // All days in-between
                float percentGrown = (float)daysToGrownSoFar / daysRequiredToGrow;
                int index = Mathf.RoundToInt(percentGrown * (plantSpriteBottoms.Count - 2));
                index = Math.Clamp(index, 1, plantSpriteBottoms.Count - 2);
                plantVisualTop.sprite = plantSpriteTops[index];
                plantVisualBottom.sprite = plantSpriteBottoms[index];
            }
        }

        public void Grow(int days, float randomGrowthChance)
        {
            if (harvestable) return;

            if (!owningTile.IsWatered() && Random.Range(0.0f, 1.0f) > randomGrowthChance) return;

            // Only a watered plant can grow
            owningTile.UnWaterTile();
            daysToGrownSoFar = Math.Min(daysToGrownSoFar + days, daysRequiredToGrow);
            if (daysToGrownSoFar == daysRequiredToGrow)
            {
                harvestable = true;
            }

            UpdatePlantVisuals();
        }

        public void SetOwner(PlantableTile plantableTile)
        {
            owningTile = plantableTile;
        }
    }
}