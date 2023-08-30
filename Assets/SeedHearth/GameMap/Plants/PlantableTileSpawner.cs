using UnityEngine;

namespace SeedHearth.GameMap.Plants
{
    public class PlantableTileSpawner : MonoBehaviour
    {
        [SerializeField] private PlantableTile plantableTilePrefab;

        [Range(1, 32)]
        [SerializeField] private int height;

        [Range(1, 32)]
        [SerializeField] private int width;

        [SerializeField] private bool debugDraw = false;

        private void Start()
        {
            Vector2 StartingPosition = transform.position - new Vector3(width / 2.0f, height / 2.0f, 0);
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    Instantiate(plantableTilePrefab, StartingPosition + new Vector2(x, y), Quaternion.identity,
                        transform);
                }
            }
        }

        private void OnDrawGizmos()
        {
            if (debugDraw)
            {
                Vector2 StartingPosition = transform.position -
                                           new Vector3(width / 2.0f, height / 2.0f, 0) - new Vector3(0.5f, 0.5f, 0);
                Gizmos.color = Color.green;

                for (int x = 0; x <= width; x++)
                {
                    Gizmos.DrawLine(StartingPosition + new Vector2(x, 0), StartingPosition + new Vector2(x, height));
                }

                for (int y = 0; y <= height; y++)
                {
                    Gizmos.DrawLine(StartingPosition + new Vector2(0, y), StartingPosition + new Vector2(width, y));
                }
            }
        }
    }
}