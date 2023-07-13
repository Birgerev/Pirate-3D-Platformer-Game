using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeSwing : MonoBehaviour
{
    public float heightChangeResistance;
    public float maxRopeLength;
    [Space]
    public Transform raycasterOrigin;
    public Transform ropeVisualOrigin;
    public LineRenderer lineRenderer;
    
    private Vector3? _ropeAttachPoint;
    private float _ropeLength;
    
    
    private Rigidbody _rigidbody;
    private GroundChecker _groundChecker;
    
    // Update is called once per frame
    void Update()
    {
        TryAttach();
        TryDetach();

        if (_ropeAttachPoint != null)
        {
            RenderRope();
            ApplyRopeNormalForce();
        }
    }

    public void ApplyRopeNormalForce()
    {
        float distanceToRopePoint = Vector3.Distance(transform.position, _ropeAttachPoint.Value);
        if (distanceToRopePoint < _ropeLength)
            return;
        
        Vector3 directionToRopePoint = (_ropeAttachPoint.Value - transform.position).normalized;
        //Get magnitude of force pointing straight away from rope attach point in player velocity
        float playerOpposingForceStrength = Vector3.Dot(_rigidbody.velocity, -directionToRopePoint);
        
        //Return if we are moving towards attach point or are still
        if (playerOpposingForceStrength <= 0)
            return;

        //Add rope normal force
        _rigidbody.velocity += directionToRopePoint * playerOpposingForceStrength;
    }

    public void RenderRope()
    {
        //Render Line
        lineRenderer.SetPosition(0, ropeVisualOrigin.position);
        lineRenderer.SetPosition(1, _ropeAttachPoint.Value);
    }
    
    public void TryAttach()
    {
        if (Input.GetMouseButton(1) && _ropeAttachPoint == null)
        {
            Ray ray = new Ray(raycasterOrigin.position, raycasterOrigin.forward);
            RaycastHit rayHit;
            if(Physics.Raycast(ray, out rayHit, maxRopeLength))
                AttachRope(rayHit.point);
        }
    }

    public void TryDetach()
    {
        if(Input.GetMouseButtonUp(1))
            Detach();
        
        if(_groundChecker.IsGrounded())
            Detach();
    }
    
    public void AttachRope(Vector3 attachPoint)
    {
        _ropeAttachPoint = attachPoint;
        _ropeLength = Vector3.Distance(transform.position, attachPoint);
    }

    public void Detach()
    {
        _ropeAttachPoint = null;
        _ropeLength = 0;
    }

    // Start is called before the first frame update
    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _groundChecker = GetComponent<GroundChecker>();
    }

}
