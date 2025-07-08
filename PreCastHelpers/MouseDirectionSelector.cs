using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class MouseDirectionSelector : MonoBehaviour
{

    public static MouseDirectionSelector Instance;
    public LineRenderer previewLine;
    private Action<Vector2> onDirectionChosen;
    private Transform originTransform;
    private Camera mainCamera;
    private bool isSelecting = false;
    private Mouse mouse;

    void Awake()
    {
        mainCamera = Camera.main;
        previewLine.gameObject.SetActive(false);
        Instance = this;
        mouse = Mouse.current;
    }

    public void BeginSelection(Transform origin, Action<Vector2> callback, bool preview = true)
    {
        onDirectionChosen = callback;
        originTransform = origin;
        isSelecting = true;
        previewLine.gameObject.SetActive(preview);

        // Optionally show a UI tooltip or cursor change
    }

    void Update()
    {
        if (!isSelecting) return;

        Vector3 mouseWorld = mainCamera.ScreenToWorldPoint(mouse.position.ReadValue());
        Vector3 origin = originTransform.position;

        // Update preview line
        previewLine.SetPosition(0, origin);
        previewLine.SetPosition(1, new Vector3(mouseWorld.x, mouseWorld.y, origin.z));

        if (mouse.leftButton.wasPressedThisFrame)
        {
            Vector2 dir = (mouseWorld - origin).normalized;

            isSelecting = false;
            previewLine.gameObject.SetActive(false);
            onDirectionChosen?.Invoke(dir);
        }
    }
}
