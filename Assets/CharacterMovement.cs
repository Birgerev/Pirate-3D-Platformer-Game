using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    [Tooltip("Move speed in meters/second")]
    public float moveSpeed = 5f;
    
    public AnimationCurve accelerationCurve01 = AnimationCurve.Constant(0, 1f, 1);
    
    
    [Space]
    [Tooltip("Upward speed to apply when jumping in meters/second")]
    public float jumpSpeed = 4f;
    [Tooltip("Whether the character can jump")]
    public bool allowJump = false;
    
    public float ForwardInput { get; set; }
    public float SidewaysInput { get; set; }
    public bool JumpInput { get; set; }
    
    private Rigidbody _rigidbody;
    private GroundChecker _groundChecker;
    
    
    private void Update()
    {
        ProcessMovement();
    }

    private void FixedUpdate()
    {
        TryJump();
    }

    private void ProcessMovement()
    {
        Vector3 inputVector = new Vector3(SidewaysInput, 0, ForwardInput);
        Vector3 mappedVector = transform.TransformDirection(inputVector);
        Vector3 targetVelocity = mappedVector * moveSpeed;
        
        //Map vector onto potentially tilted ground plane
        if(_groundChecker.IsGrounded())
            targetVelocity = Vector3.ProjectOnPlane(targetVelocity, _groundChecker.GetGround().Value.normal);
        
        float accelerationControl = accelerationCurve01.Evaluate(_rigidbody.velocity.magnitude / targetVelocity.magnitude);

        //Less control in air
        if (!_groundChecker.IsGrounded())
            accelerationControl *= 0.5f;
        
        _rigidbody.velocity += targetVelocity * accelerationControl * Time.deltaTime;
    }
    
    private void TryJump()
    {
        // Check if trying to jump
        if (!JumpInput || !allowJump || !_groundChecker.IsGrounded())
            return;
    
        // Apply an upward velocity to jump
        _rigidbody.velocity += Vector3.up * jumpSpeed;
    }
    
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _groundChecker = GetComponent<GroundChecker>();
    }
}
