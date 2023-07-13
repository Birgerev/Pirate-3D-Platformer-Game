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
        Vector3 finalEulerAngles = transform.eulerAngles + cameraMovement;
        
        //Clamp X-axis
        finalEulerAngles.x = Mathf.Clamp(finalEulerAngles.x, -89, 89);
        
        transform.eulerAngles = finalEulerAngles;
    }
}
