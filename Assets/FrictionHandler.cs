using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrictionHandler : MonoBehaviour
{
    public float dragCoefficient;
    public float friction;
    
    private Rigidbody _rigidbody;
    private GroundChecker _groundChecker;

    private void ApplyGroundFriction()
    {
        // Calculate drag force
        Vector3 dragForce = -dragCoefficient * _rigidbody.velocity;

        // Apply the drag force
        _rigidbody.AddForce(dragForce * Time.fixedDeltaTime);
        // Check if the object is touching the ground
        if (_groundChecker.IsGrounded())
        {
            // Calculate the velocity component parallel to the ground
            Vector3 velocity = _rigidbody.velocity;

            // Calculate the friction force
            Vector3 frictionForce = -friction * velocity;

            // Apply the friction force
            _rigidbody.AddForce(frictionForce * Time.fixedDeltaTime);
        }
    }

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _groundChecker = GetComponent<GroundChecker>();
    }
}
