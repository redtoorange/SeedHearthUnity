using System;
using UnityEngine;

namespace SeedHearth.Cards
{
    public class CardMovementController : MonoBehaviour
    {
        public Action OnMovementComplete;
        
        [SerializeField] private float movementTime = 10.0f;
        private RectTransform rectTransform;

        private bool isMoving = false;
        private Vector2 startingLocation;
        private Vector2 targetLocation;
        private float elapsed = 0.0f;


        private void Awake()
        {
            rectTransform = GetComponent<RectTransform>();
        }

        private void Update()
        {
            if (!isMoving) return;

            elapsed += Time.deltaTime;
            ProcessMoving();
            HasReachedTargetLocation();
        }


        public void MoveTo(Vector2 newPosition)
        {
            elapsed = 0.0f;
            startingLocation = rectTransform.position;
            targetLocation = newPosition;
            isMoving = true;
            OnMovementComplete?.Invoke();
        }

        private void ProcessMoving()
        {
            rectTransform.position = Vector2.Lerp(startingLocation, targetLocation, elapsed / movementTime);
        }

        private void HasReachedTargetLocation()
        {
            if (Vector2.Distance(rectTransform.position, targetLocation) < 0.01f)
            {
                rectTransform.position = targetLocation;
                isMoving = false;
            }
        }
    }
}