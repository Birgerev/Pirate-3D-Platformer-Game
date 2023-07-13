using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovementTilt : MonoBehaviour
{
    public float mouseSensitivity;
    public float returnSpeed;
    public float maxTilt;

    [HideInInspector]
    public float currentTilt;
    
    // Update is called once per frame
    void Update()
    {
        currentTilt += Input.GetAxis("Mouse X") * mouseSensitivity;

        currentTilt = Mathf.Clamp(currentTilt, -maxTilt, maxTilt);

        currentTilt = Mathf.Lerp(currentTilt, 0, returnSpeed);
        
        
        transform.localEulerAngles = new Vector3(
                transform.localEulerAngles.x, 
                transform.localEulerAngles.y, 
                currentTilt);
    }
}
