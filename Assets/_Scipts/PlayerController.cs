using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public int score;
    public float jumpForce;
    public float moveSpeed;
    public int maxJumps;

    private float _curMoveInput = 0;
    private int _jumpsAvailable;

    [Header("Components")]
    public Rigidbody2D rig;


    private void FixedUpdate()
    {
        Move();
    }

    public void OnCollisionEnter2D(Collision2D col)
    {
        if (col.contacts[0].point.y < transform.position.y)
            _jumpsAvailable = maxJumps;
    }

    private void Move()
    {
        rig.velocity = new Vector2(_curMoveInput * moveSpeed, rig.velocity.y);
    }

    private void Jump()
    {
        rig.velocity = new Vector2(rig.velocity.x, 0);
        rig.AddForce(Vector2.up* jumpForce, ForceMode2D.Impulse);
    }

    public void OnMoveInput(InputAction.CallbackContext context)
    {
        _curMoveInput = context.ReadValue<float>();
    }

    public void OnJumpInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            if (_jumpsAvailable > 0)
            {
                _jumpsAvailable--;
                Jump();
            }
        }
    }
    
    
}
