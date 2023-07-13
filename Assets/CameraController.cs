using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Vector2 sensitivity;
    
    // Update is called once per frame
    void Update()
    {
        Vector3 cameraMovement = new Vector3(
            Input.GetAxis("Mouse Y") * sensitivity.y,
            Input.GetAxis("Mouse X") * sensitivity.x, 
            0);
        Vector3 finalEulerAngles = transform.localEulerAngles + cameraMovement;
        
        //Clamp rotation
        if(finalEulerAngles.x is > 89 and < 180)
            finalEulerAngles.x = 89;
        if(finalEulerAngles.x is < 271 and > 180) 
            finalEulerAngles.x = 271;
        
        transform.localEulerAngles = finalEulerAngles;
    }
}
