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

    bool isTouchingFront;
    public Transform frontCheck;
    bool wallSliding;
    public float wallSlidingSpeed;

    bool wallJumping;
    public float xWallForce;
    public float yWallForce;
    public float wallJumpTime;

    private Rigidbody2D _rig;
    private AudioSource _audioSource;
    public AudioClip jump;

    public float coyoteTime = 0.2f;
    private float coyoteTimeCounter;

    public float jumpBufferTime = 0.2f;
    private float jumpBufferCounter;
    

    private int _facing;

    public bool canMove;


    private void Awake()
    {
        _rig = GetComponent<Rigidbody2D>();
        _boxCollider = GetComponent<BoxCollider2D>();
        _audioSource = GetComponent<AudioSource>();
        canMove = true;
    }

    private void FixedUpdate()
    {

        if (IsGrounded())
        {
            coyoteTimeCounter = coyoteTime;
            if (jumpBufferCounter > 0)
            {
                _rig.velocity = new Vector2(_rig.velocity.x, 0);
                _rig.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                _audioSource.PlayOneShot(jump);

                jumpBufferCounter = 0f;
            }
        }    
        else
        {
            coyoteTimeCounter -= Time.deltaTime;
            jumpBufferCounter -= Time.deltaTime;
        }

        
        


        if (canMove)
            if (IsOnWall())
                _rig.velocity = new Vector2(_rig.velocity.x, Mathf.Clamp(_rig.velocity.y, wallSlidingSpeed, float.MaxValue));
            else 
                Move();

        //isTouchingFront = Physics2D.OverlapCircle(frontCheck.position, .1f, groundLayer);


        if (IsOnWall() && !IsGrounded())
        {
            Debug.Log("On Wall");
            wallSliding = true;
        }
        else
        {
            wallSliding = false;
        }

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

        jumpBufferCounter = jumpBufferTime;

        if (coyoteTimeCounter > 0f && !IsOnWall())
        {
            _rig.velocity = new Vector2(_rig.velocity.x, 0);
            _rig.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            _audioSource.PlayOneShot(jump);

            jumpBufferCounter = 0f;
        }
        else if (IsOnWall() && !IsGrounded())
        {
            if (_curMoveInput == 0)
            {
                _rig.velocity = new Vector2(Mathf.Sign(_facing) * 10, 10);
            }
            else if (_curMoveInput > 0 || _curMoveInput < 0)
            {
                _rig.velocity = new Vector2(Mathf.Sign(_facing) * 10, 10);
            }
            

            _wallJumpCooldown = 0;
            
        }
        else if (IsOnWall() && IsGrounded())
        {
            if (_curMoveInput == 0)
            {
                _rig.velocity = new Vector2(Mathf.Sign(_facing) * 10, 10);
            }
            else if (_curMoveInput > 0 || _curMoveInput < 0)
            {
                _rig.velocity = new Vector2(Mathf.Sign(_facing) * 10, 10);
            }


            _wallJumpCooldown = 0;
        }

        if (_rig.velocity.y > 0f)
        {
            coyoteTimeCounter = 0f;
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
        return Physics2D.OverlapCircle(frontCheck.position, 0.1f, groundLayer);
        
        
        // return hit.collider != null;
    }

    public void OnMoveInput(InputAction.CallbackContext context)
    {
        _curMoveInput = context.ReadValue<float>();

        if (_curMoveInput != 0 && _curMoveInput > 0)
        {
            _facing = 1;
            //frontCheck.localPosition = new Vector3(-frontCheck.localPosition.x, frontCheck.localPosition.y, frontCheck.localPosition.z);
        }
            
        else if (_curMoveInput != 0 &&  _curMoveInput < 0)
        {
            _facing = -1;
            //frontCheck.localPosition = new Vector3(-frontCheck.localPosition.x, frontCheck.localPosition.y, frontCheck.localPosition.z);
        }
            
    }

    public void OnJumpInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            Jump();
           
        }
    }
    
    
}
