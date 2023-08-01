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

            // tileMap.get
            TileBase tile = wetDirtMap.GetTile(intPosition);
            if (tile != null)
            {
                // Debug.Log("Tile: " + tile.name);
                selectionSquare.transform.position = new Vector3(intPosition.x, intPosition.y);
                return;
            }
            
            tile = dirtMap.GetTile(intPosition);
            if (tile != null)
            {
                // Debug.Log("Tile: " + tile.name);
                selectionSquare.transform.position = new Vector3(intPosition.x, intPosition.y);
                return;
            }
            
            tile = grassMap.GetTile(intPosition);
            if (tile != null)
            {
                // Debug.Log("Tile: " + tile.name);
                selectionSquare.transform.position = new Vector3(intPosition.x, intPosition.y);
            }
        }
    }
}