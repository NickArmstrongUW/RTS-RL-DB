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

    private float maxDistance = float.PositiveInfinity; // for spells that have a casting range
    private Vector3? referencePoint = null; // nullable Vector3

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

    // starts mouse tracking, but only allows for selection within a certain distance from a reference point
    public void BeginSelection(
        Action<Vector2> callback, 
        float maxDistance, 
        Vector3 referencePoint, 
        Sprite customSprite = null)
    {

        this.maxDistance = maxDistance;
        this.referencePoint = referencePoint;

        BeginSelection(callback, customSprite);
    }

    // initializes mouse tracking, will return the Position of the mouse to the callback function
     public void BeginSelection(Action<Vector2> callback, Sprite customSprite = null)
    {
        onPositionChosen = callback;
        isSelecting = true;
        // Hide the system cursor
        Cursor.visible = false;
        
        if (previewSprite != null)
        {
            previewSprite.SetActive(true);
            
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

        float refDistance = 0f;
        if (referencePoint.HasValue) {
            refDistance = Vector3.Distance(mouseWorld, referencePoint.Value);
        }

        // Update preview sprite position to follow mouse
        if (previewSprite != null && previewSprite.activeSelf)
        {
            previewSprite.transform.position = mouseWorld;
            if (refDistance > maxDistance) {
                spriteRenderer.color = Color.red;
            }
            else {
                spriteRenderer.color = Color.white; // Reset to normal color
            }
        }

        if (mouse.leftButton.wasPressedThisFrame && refDistance <= maxDistance)
        {
            isSelecting = false;
            if (previewSprite != null)
                previewSprite.SetActive(false);
            
            // Show the system cursor again
            Cursor.visible = true;
            
            // reset maxDistance and referencepoint
            maxDistance = float.PositiveInfinity;
            referencePoint = null;

            onPositionChosen?.Invoke(mouseWorld);
        }
    }
}
