using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI; // Add this if you need UI components

public class MousePositionSelector : MonoBehaviour
{
    public static MousePositionSelector Instance;
    public GameObject previewSprite; 
    private Action<Vector2> onPositionChosen;
    private Camera mainCamera;
    private bool isSelecting = false;
    private Mouse mouse;
    private SpriteRenderer spriteRenderer; // to use if a function doesn't have a custom
    public Sprite defaultSprite;

    void Awake()
    {
        mainCamera = Camera.main;
        Instance = this;
        mouse = Mouse.current;
        if (previewSprite != null)
        {
            previewSprite.SetActive(false);
            spriteRenderer = previewSprite.GetComponent<SpriteRenderer>();
            
            // Add SpriteRenderer if it doesn't exist
            if (spriteRenderer == null)
            {
                spriteRenderer = previewSprite.AddComponent<SpriteRenderer>();
            }
        }
    }

    public void BeginSelection(Action<Vector2> callback, bool showPreview = true, Sprite customSprite = null)
    {
        onPositionChosen = callback;
        isSelecting = true;
        
        if (previewSprite != null)
        {
            previewSprite.SetActive(showPreview);
            
            // Update the sprite if a custom one is provided
            if (customSprite != null && spriteRenderer != null)
            {
                spriteRenderer.sprite = customSprite;
            } else if (defaultSprite != null) {
                spriteRenderer.sprite = defaultSprite;
            }
        }
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
