using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorState : MonoBehaviour
{
    private void Start()
    {
        ToggleState();
    }

    private void Update()
    {
        // Check for escape key press to unlock the cursor
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleState();
        }
    }

    private void ToggleState()
    {
        // Show the mouse cursor
        Cursor.visible = !Cursor.visible;
            
        // Unlock the cursor
        Cursor.lockState = (Cursor.lockState==CursorLockMode.None) ? CursorLockMode.Locked : CursorLockMode.None;
    }
}
