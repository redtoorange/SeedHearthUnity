using System.Collections.Generic;
using SeedHearth.Plants;
using UnityEngine;

namespace SeedHearth.MouseController
{
    public class SelectionSquareController : MonoBehaviour
    {
        [SerializeField] private Vector2 gridOffset = Vector2.zero;
        [SerializeField] private Color validColor = Color.green;
        [SerializeField] private Color invalidColor = Color.red;
        [SerializeField] private Color defaultColor = Color.white;

        RaycastHit2D[] results = new RaycastHit2D[10];
        private List<GameObject> subSquares;
        private HoverData cachedHoverData;

        private void Awake()
        {
            subSquares = new List<GameObject>();
            foreach (SpriteRenderer child in GetComponentsInChildren<SpriteRenderer>())
            {
                subSquares.Add(child.gameObject);
            }
        }

        public HoverData GetHoverData()
        {
            return cachedHoverData;
        }

        private void CheckHoveredObject(Vector2 worldPosition, List<PlantableTile> tiles,
            List<Plant> plants)
        {
            ContactFilter2D contactFilter2D = new ContactFilter2D();
            int count = Physics2D.Raycast(worldPosition, Vector2.zero, contactFilter2D, results);

            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    if (results[i].collider.TryGetComponent(out Plant plant))
                    {
                        plants.Add(plant);
                    }
                    else if (results[i].collider.TryGetComponent(out PlantableTile tile))
                    {
                        tiles.Add(tile);
                    }
                }
            }
        }

        public void UpdateSquare(Vector2 worldPosition)
        {
            Vector2 pos = worldPosition;
            pos.x = Mathf.FloorToInt(pos.x);
            pos.y = Mathf.FloorToInt(pos.y);
            transform.position = pos + gridOffset;

            List<PlantableTile> tiles = new List<PlantableTile>();
            List<Plant> plants = new List<Plant>();

            foreach (GameObject subSquare in subSquares)
            {
                CheckHoveredObject(subSquare.transform.position, tiles, plants);
            }

            cachedHoverData = new HoverData(tiles, plants);
        }
    }
}