using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class SimpleClickSelector : MonoBehaviour
{
    public static SimpleClickSelector Instance;
    
    private Action onClick;
    private bool isWaitingForClick = false;
    private Mouse mouse;

    void Awake() {
        Instance = this;
        mouse = Mouse.current;
    }

    public void WaitForClick(Action callback) {
        onClick = callback;
        isWaitingForClick = true;
        Debug.Log("Waiting for click...");
    }

    void Update() {
        if (!isWaitingForClick) return;

        // Use new Input System
        if (Mouse.current.leftButton.wasPressedThisFrame) {
            isWaitingForClick = false;
            onClick?.Invoke();
        }
    }

    public void Cancel() {
        isWaitingForClick = false;
        onClick = null;
    }
}