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

    private float _wallJumpCooldown = 1f;

    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;

    private float _curMoveInput = 0;
    private int _jumpsAvailable;
    private bool _isGrounded;
    private BoxCollider2D _boxCollider;

    private Rigidbody2D _rig;

    private int _facing;


    private void Awake()
    {
        _rig = GetComponent<Rigidbody2D>();
        _boxCollider = GetComponent<BoxCollider2D>();
    }

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
        _rig.velocity = new Vector2(_curMoveInput * moveSpeed, _rig.velocity.y);
    }

    private void Jump()
    {
        if (IsGrounded())
        {
            _rig.velocity = new Vector2(_rig.velocity.x, 0);
            _rig.AddForce(Vector2.up* jumpForce, ForceMode2D.Impulse);
        }
        else if (IsOnWall() && !IsGrounded())
        {
            if (_curMoveInput == 0)
            {
                _rig.velocity = new Vector2(-Mathf.Sign(_facing) * 10, 6);
                _facing *= -1;
            }
            else
            {
                _rig.velocity = new Vector2(-Mathf.Sign(_facing) * 3, 6);
            }
            
            _wallJumpCooldown = 0;
            
        }
        
    }

    private bool IsGrounded()
    {
        RaycastHit2D hit = Physics2D.BoxCast(
            _boxCollider.bounds.center, 
            _boxCollider.bounds.size, 
            0, 
            Vector2.down, 
            0.1f, 
            groundLayer); 
        
        return hit.collider != null;
    }
    
    private bool IsOnWall()
    {
        RaycastHit2D hit = Physics2D.BoxCast(
            _boxCollider.bounds.center, 
            _boxCollider.bounds.size, 
            0, 
            new Vector2(transform.localScale.x, 0), 
            0.1f, 
            wallLayer);
        
        //Debug.Log("Is OnWall");
        return hit.collider != null;
    }

    public void OnMoveInput(InputAction.CallbackContext context)
    {
        _curMoveInput = context.ReadValue<float>();

        if (_curMoveInput > 0.01f)
            _facing = 1;
        else if (_curMoveInput < -0.01f)
            _facing = -1;
    }

    public void OnJumpInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            Jump();
           
        }
    }
    
    
}
