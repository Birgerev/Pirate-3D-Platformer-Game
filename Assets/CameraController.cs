using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Vector2 sensitivity;
    
    // Update is called once per frame
    void Update()
    {
        transform.eulerAngles += new Vector3(Input.GetAxis("Mouse Y") * sensitivity.y, Input.GetAxis("Mouse X") * sensitivity.x, 0);
    }
}
