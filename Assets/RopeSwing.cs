using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeSwing : MonoBehaviour
{
    public float ropeStrength;
    public float maxRopeLength;
    [Space]
    public Transform raycasterOrigin;
    public Transform ropeVisualOrigin;
    public LineRenderer lineRenderer;
    
    private Vector3? _ropeAttachPoint;
    private float _ropeLength;
    private Rigidbody _rigidbody;
    
    
    
    // Start is called before the first frame update
    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetMouseButton(1) && _ropeAttachPoint == null)
        {
            Ray ray = new Ray(raycasterOrigin.position, raycasterOrigin.forward);
            RaycastHit rayHit;
            if(Physics.Raycast(ray, out rayHit, maxRopeLength))
                AttachRope(rayHit.point);
        }


        if(Input.GetMouseButtonUp(1))
            Detach();

        if (_ropeAttachPoint != null)
        {
            //Render Line
            lineRenderer.SetPosition(0, ropeVisualOrigin.position);
            lineRenderer.SetPosition(1, _ropeAttachPoint.Value);
            
            
            float distanceToRopePoint = Vector3.Distance(transform.position, _ropeAttachPoint.Value);
            if (distanceToRopePoint > _ropeLength)
            {
                Vector3 directionToRopePoint = (_ropeAttachPoint.Value - transform.position).normalized;
                //Get magnitude of force pointing straight away from rope attach point in player velocity
                float playerOpposingForceStrength = Vector3.Dot(_rigidbody.velocity, -directionToRopePoint);
                
                //Add rope normal force
                _rigidbody.velocity += directionToRopePoint * playerOpposingForceStrength;
            }
            
            /*
            float distanceToRopePoint = Vector3.Distance(transform.position, _ropeAttachPoint.Value);
            if (distanceToRopePoint > _ropeLength)
            {
                //float ropeOverstretchLength = distanceToRopePoint - _ropeLength;
                Vector3 directionToRope = (_ropeAttachPoint.Value - transform.position).normalized;
                //float ropeCentripetalForceStrength = ropeOverstretchLength * ropeStrength ;
                
                _rigidbody.AddForce(
                    directionToRope * ropeStrength * Time.fixedTime * Time.timeScale);
            }*/
        }
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
}
