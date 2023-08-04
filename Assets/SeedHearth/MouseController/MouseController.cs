using SeedHearth.Plants;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

namespace SeedHearth.MouseController
{
    public class MouseController : MonoBehaviour
    {
        [SerializeField] private Tilemap grassMap;
        [SerializeField] private Tilemap dirtMap;
        [SerializeField] private Tilemap wetDirtMap;

        private Camera mainCamera;

        [SerializeField] private GameObject selectionSquare;
        [SerializeField] private LayerMask plantLayerMask;

        [SerializeField] private TileBase hoveredTile;

        [SerializeField] private Plant hoveredPlant;
        [SerializeField] private Produce.Produce hoveredProduce;

        RaycastHit2D[] results = new RaycastHit2D[10];


        private void Start()
        {
            mainCamera = Camera.main;
        }

        private void Update()
        {
            DetectHoveredTile();
        }

        private void DetectHoveredTile()
        {
            Vector2 mousePosition = Mouse.current.position.ReadValue();
            Vector2 worldPosition = mainCamera.ScreenToWorldPoint(mousePosition);
            Vector3Int intPosition = new Vector3Int(
                Mathf.FloorToInt(worldPosition.x),
                Mathf.FloorToInt(worldPosition.y)
            );

            UpdateSelectionSquare(intPosition);
            CheckHoveredObject(worldPosition);
            CheckHoveredTile(intPosition);

            if (Mouse.current.leftButton.wasPressedThisFrame)
            {
                if (hoveredPlant != null)
                {
                    hoveredPlant.ConvertToProduce();
                }
                else if (hoveredProduce != null)
                {
                    hoveredProduce.PickUpProduce();
                }
            }
        }

        private void UpdateSelectionSquare(Vector3Int intPosition)
        {
            selectionSquare.transform.position = new Vector3(intPosition.x, intPosition.y);
        }

        private void CheckHoveredTile(Vector3Int intPosition)
        {
            hoveredTile = wetDirtMap.GetTile(intPosition);
            if (hoveredTile != null)
            {
                return;
            }

            hoveredTile = dirtMap.GetTile(intPosition);
            if (hoveredTile != null)
            {
                return;
            }

            hoveredTile = grassMap.GetTile(intPosition);
            if (hoveredTile != null)
            {
                return;
            }
        }

        private void CheckHoveredObject(Vector2 worldPosition)
        {
            int count = Physics2D.RaycastNonAlloc(worldPosition, Vector2.zero, results, 0.0f, plantLayerMask);

            hoveredPlant = null;
            hoveredProduce = null;

            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    if (results[i].collider.TryGetComponent(out Plant plant))
                    {
                        hoveredPlant = plant;
                    }
                    else if (results[i].collider.TryGetComponent(out Produce.Produce produce))
                    {
                        hoveredProduce = produce;
                    }
                }
            }
        }
    }
}