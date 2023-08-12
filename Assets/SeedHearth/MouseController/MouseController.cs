using SeedHearth.Plants;
using UnityEngine;
using UnityEngine.InputSystem;

namespace SeedHearth.MouseController
{
    public class MouseController : MonoBehaviour
    {
        private Camera mainCamera;

        [SerializeField] private GameObject selectionSquare;
        [SerializeField] private LayerMask plantLayerMask;

        [SerializeField] private PlantableTile hoveredTile;
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

        public PlantableTile GetHoveredTile() => hoveredTile;
        public Plant GetHoveredPlant() => hoveredPlant;
        public Produce.Produce GetHoveredProduce() => hoveredProduce;

        private void DetectHoveredTile()
        {
            Vector2 mousePosition = Mouse.current.position.ReadValue();
            Vector2 worldPosition = mainCamera.ScreenToWorldPoint(mousePosition);

            CheckHoveredObject(worldPosition);
            UpdateSelectionSquare();

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

        private void UpdateSelectionSquare()
        {
            if (hoveredTile == null)
            {
                selectionSquare.gameObject.SetActive(false);
            }
            else
            {
                selectionSquare.gameObject.SetActive(true);
                selectionSquare.transform.position = hoveredTile.transform.position;
            }
        }

        private void CheckHoveredObject(Vector2 worldPosition)
        {
            int count = Physics2D.RaycastNonAlloc(worldPosition, Vector2.zero, results, 0.0f, plantLayerMask);

            hoveredPlant = null;
            hoveredProduce = null;
            hoveredTile = null;

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
                    else if (results[i].collider.TryGetComponent(out PlantableTile plantable))
                    {
                        hoveredTile = plantable;
                    }
                }
            }
        }
    }
}