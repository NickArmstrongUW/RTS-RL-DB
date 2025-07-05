using System;
using UnityEngine;

public class MouseDirectionSelector : MonoBehaviour
{
    private Action<Vector2> onDirectionChosen;
    private Transform originTransform;
    private Camera mainCamera;
    private bool isSelecting = false;

    void Awake()
    {
        mainCamera = Camera.main;
    }

    public void BeginSelection(Transform origin, Action<Vector2> callback)
    {
        onDirectionChosen = callback;
        originTransform = origin;
        isSelecting = true;
        Cursor.visible = true;

        // Optionally show a UI tooltip or cursor change
    }

    void Update()
    {
        if (!isSelecting) return;

        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mouseWorld = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            Vector2 dir = (mouseWorld - originTransform.position).normalized;

            isSelecting = false;
            onDirectionChosen?.Invoke(dir);
        }
    }
}
