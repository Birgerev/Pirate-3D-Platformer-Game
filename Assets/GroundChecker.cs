using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundChecker : MonoBehaviour
{
    [Tooltip("Maximum slope the character can jump on")]
    [Range(5f, 60f)]
    public float slopeLimit = 45f;
    
    private CapsuleCollider _capsuleCollider;
    
    public bool IsGrounded()
    {
        float capsuleHeight = Mathf.Max(_capsuleCollider.radius * 2f, _capsuleCollider.height);
        Vector3 capsuleBottom = transform.TransformPoint(_capsuleCollider.center - Vector3.up * capsuleHeight / 2f);
        float radius = transform.TransformVector(_capsuleCollider.radius, 0f, 0f).magnitude;
        Ray ray = new Ray(capsuleBottom + transform.up * .01f, -transform.up);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, radius * 5f))
        {
            float normalAngle = Vector3.Angle(hit.normal, transform.up);
            if (normalAngle < slopeLimit)
            {
                float maxDist = radius / Mathf.Cos(Mathf.Deg2Rad * normalAngle) - radius + .02f;
                if (hit.distance < maxDist)
                    return true;
            }
        }

        return false;
    }
    
    private void Awake()
    {
        _capsuleCollider = GetComponent<CapsuleCollider>();
    }
}
