using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace SeedHearth.SelectionArrow
{
    public class CardTargettingArrow : MonoBehaviour
    {
        [SerializeField] private GameObject arrowHeadPrefab;
        [SerializeField] private GameObject arrowNodePrefab;

        [SerializeField] private int arrowNodeCount;
        [SerializeField] private float scaleFactor;

        private Camera camera;
        private RectTransform originPoint;
        private List<RectTransform> arrowNodes = new List<RectTransform>();
        private List<Vector2> controlPoints = new List<Vector2>
        {
            Vector2.zero, Vector2.zero, Vector2.zero, Vector2.zero
        };

        private List<Vector2> controlPointFactors = new List<Vector2>
        {
            new Vector2(-.03f, 0.8f),
            new Vector2(0.1f, 1.4f)
        };

        private void Awake()
        {
            originPoint = GetComponent<RectTransform>();
            for (int i = 0; i < arrowNodeCount; i++)
            {
                CreateNode(arrowNodePrefab);
            }

            CreateNode(arrowHeadPrefab);
            camera = Camera.main;
            
        }

        private void Update()
        {
            RectTransformUtility.ScreenPointToWorldPointInRectangle(originPoint, Mouse.current.position.value, camera,
                out Vector3 mousePosition);
            controlPoints[0] = originPoint.position;
            controlPoints[3] = mousePosition;

            controlPoints[1] = controlPoints[0] + (controlPoints[3] - controlPoints[0]) * controlPointFactors[0];
            controlPoints[2] = controlPoints[0] + (controlPoints[3] - controlPoints[0]) * controlPointFactors[1];


            for (int i = 0; i < arrowNodes.Count; i++)
            {
                float t = Mathf.Log(1.0f * i / (arrowNodes.Count - 1) + 1.0f, 2.0f);
                arrowNodes[i].position =
                    Mathf.Pow(1 - t, 3) * controlPoints[0] +
                    3 * Mathf.Pow(1 - t, 2) * t * controlPoints[1] +
                    3 * (1 - t) * Mathf.Pow(t, 2) * controlPoints[2] +
                    Mathf.Pow(t, 3) * controlPoints[3];

                if (i > 0)
                {
                    Vector3 euler = new Vector3(0, 0, Vector2.SignedAngle(
                        Vector2.up,
                        arrowNodes[i].position - this.arrowNodes[i - 1].position
                    ));
                    arrowNodes[i].rotation = Quaternion.Euler(euler);
                }

                float scale = scaleFactor * (1.0f - 0.03f * (arrowNodes.Count - 1 - i));
                arrowNodes[i].localScale = new Vector3(scale, scale, 1.0f);
            }

            arrowNodes[0].transform.rotation = arrowNodes[1].transform.rotation;
        }

        private void CreateNode(GameObject prefab)
        {
            RectTransform nodeTransform = Instantiate(prefab, originPoint).GetComponent<RectTransform>();
            nodeTransform.position = new Vector2(-1000, -1000);
            arrowNodes.Add(nodeTransform);
        }
    }
}