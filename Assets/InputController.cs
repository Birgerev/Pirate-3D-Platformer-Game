using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    private CharacterMovement _charMovement;

    void Awake()
    {
        _charMovement = GetComponent<CharacterMovement>();
    }

    private void FixedUpdate()
    {
        // Get input values
        int vertical = Mathf.RoundToInt(Input.GetAxisRaw("Vertical"));
        int horizontal = Mathf.RoundToInt(Input.GetAxisRaw("Horizontal"));
        bool jump = Input.GetKey(KeyCode.Space);
        _charMovement.ForwardInput = vertical;
        _charMovement.SidewaysInput = horizontal;
        _charMovement.JumpInput = jump;
    }
}
