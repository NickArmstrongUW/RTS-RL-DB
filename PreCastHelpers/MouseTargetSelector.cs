using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class MouseTargetSelector : MonoBehaviour
{
    public static MouseTargetSelector Instance;
    public GameObject previewSprite; 
    private Action<Vector2> onPositionChosen;
    private Camera mainCamera;
    private bool isSelecting = false;
    private Mouse mouse;

    void Awake()
    {
        mainCamera = Camera.main;
        Instance = this;
        mouse = Mouse.current;
        if (previewSprite != null)
            previewSprite.SetActive(false);
    }

    public void BeginSelection(Action<Vector2> callback, bool showPreview = true)
    {
        onPositionChosen = callback;
        isSelecting = true;
        if (previewSprite != null)
            previewSprite.SetActive(showPreview);
    }

    void Update()
    {
        if (!isSelecting) return;

        Vector3 mouseWorld = mainCamera.ScreenToWorldPoint(mouse.position.ReadValue());
        mouseWorld.z = 0; // Or whatever your game uses

        if (previewSprite != null && previewSprite.activeSelf)
            previewSprite.transform.position = mouseWorld;

        if (mouse.leftButton.wasPressedThisFrame)
        {
            isSelecting = false;
            if (previewSprite != null)
                previewSprite.SetActive(false);
            onPositionChosen?.Invoke(mouseWorld);
        }
    }
}
