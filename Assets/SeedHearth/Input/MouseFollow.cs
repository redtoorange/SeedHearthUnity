using UnityEngine;
using UnityEngine.InputSystem;

public class MouseFollow : MonoBehaviour
{
    private RectTransform rectTransform;
    private Camera uiCamera;
    private bool shouldFollow;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        uiCamera = Camera.main;
    }

    public void SetShouldFollow(bool shouldFollow)
    {
        this.shouldFollow = shouldFollow;
    }

    private void Update()
    {
        if (shouldFollow)
        {
            Vector2 mousePosition = Mouse.current.position.ReadValue();
            Vector2 localPosition = uiCamera.ScreenToWorldPoint(mousePosition);
            rectTransform.position = localPosition;
        }
    }
}