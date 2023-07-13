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
        //Apply new tilt from mouse movement
        currentTilt += Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime * Time.timeScale;

        //Return tilt to 0
        currentTilt = Mathf.Lerp(currentTilt, 0, returnSpeed * Time.deltaTime * Time.timeScale);
        
        //Make sure tilt value is in a valid range
        currentTilt = Mathf.Clamp(currentTilt, -maxTilt, maxTilt);
        
        
        transform.localEulerAngles = new Vector3(
                transform.localEulerAngles.x, 
                transform.localEulerAngles.y, 
                currentTilt);
    }
}
