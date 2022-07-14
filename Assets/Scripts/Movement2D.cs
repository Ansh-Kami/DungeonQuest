using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement2D : MonoBehaviour
{
    public ParticleSystem dust;


    [Header("Movement Variables")]

    private Rigidbody2D _rb;
    private Animator _anim;

    [Header("Layer Masks")]
    [SerializeField] private LayerMask _groundLayer;

    [Header("Movement Variables")]
    [SerializeField] private float _movementAcceleration = 50f;
    [SerializeField] private float _maxMoveSpeed = 3f;
    [SerializeField] private float _GroundlinearDrag = 7f;
    public float _horizontalDirection;
    private bool _changingDirection => (_rb.velocity.x > 0f && _horizontalDirection < 0f) || (_rb.velocity.x < 0f && _horizontalDirection > 0f);
    private bool _facingRight = true;

    [Header("Jump Variables")]
    [SerializeField] private float _jumpForce = 12f;
    [SerializeField] private float _airLinearDrag = 2.5f;
    [SerializeField] private float _fallMultiplier = 8f;
    [SerializeField] private float _lowJumpFallMultiplier = 5f;
    [SerializeField] private int _extraJumps = 1;
    [SerializeField] private float _hangTime = .1f;
    [SerializeField] private float _jumpBufferLength = .1f;
    private int _extraJumpsValue;
    private float _hangTimeCounter;
    private float _jumpBufferCounter;
    private bool _canJump => _jumpBufferCounter > 0f && (_hangTimeCounter > 0f || _extraJumpsValue > 0);

    [Header("Ground Collision Variables")]
    [SerializeField] private float _groundRaycastLength;
    private bool _onGround;



    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
    }
    
    private void Update()
    {
        
        _horizontalDirection = GetInput().x;
        if(Input.GetButtonDown("Jump"))
        {
            _jumpBufferCounter = _jumpBufferLength;
        }
        else
        {

            _jumpBufferCounter -= Time.deltaTime; 

        }
        if(_canJump) Jump();

        //Animation
        _anim.SetBool("isGrounded", _onGround);
        _anim.SetFloat("horizontalDirection", Mathf.Abs(_horizontalDirection));
        if (_horizontalDirection < 0f && _facingRight)
        {
            Flip();
        }
        else if(_horizontalDirection > 0f && !_facingRight)
        {
            Flip();
        }
        if (_rb.velocity.y < 0f)
        {
            //animation
            _anim.SetBool("isJumping", false);
            _anim.SetBool("isFalling", true);

        } 

    
        }
            
    

    private void FixedUpdate() 
    {
        
        
        MoveCharacter();
        ApplyGroundLinearDrag();
        
        if(_onGround)
        {
            _extraJumpsValue = _extraJumps;
            _hangTimeCounter = _hangTime;
            


            ApplyGroundLinearDrag();
            _anim.SetBool("isJumping", false);
            _anim.SetBool("isFalling", false);
        }
        else
        {
            ApplyAirLinearDrag();
            FallMultiplier();
            _hangTimeCounter -= Time.deltaTime;
        }

        
    }

    private static Vector2 GetInput()
    {
        return new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    }

    private void MoveCharacter()
    {
        _rb.AddForce(new Vector2(_horizontalDirection, 0f) * _movementAcceleration);

        if(Mathf.Abs(_rb.velocity.x) > _maxMoveSpeed)
        {
            _rb.velocity = new Vector2(Mathf.Sign(_rb.velocity.x) * _maxMoveSpeed, _rb.velocity.y);
            
        }

        
    }

    private void ApplyGroundLinearDrag()
    {
        if (Mathf.Abs(_horizontalDirection) < 0.4f || _changingDirection)
        {
            _rb.drag = _GroundlinearDrag;
        }
        else
        {
           _rb.drag = 0f;  
        }
    }

    private void ApplyAirLinearDrag()
    {
        _rb.drag = _airLinearDrag;
    }


    private void Jump()
    {

        if(!_onGround) _extraJumpsValue --;
        _rb.velocity = new Vector2(_rb.velocity.x, 0f);
        _rb.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
        _hangTimeCounter = 0f;
        _jumpBufferCounter = 0f;

        //Animation
        
        _anim.SetBool("isJumping", true);
        _anim.SetBool("isFalling", false);

        if(_onGround)
        {
            CreateDust();
        }
    }

    private void FallMultiplier(){
        if (_rb.velocity.y < 0)
        {
            _rb.gravityScale = _fallMultiplier;
        }
        else if (_rb.velocity.y > 0 && !Input.GetButton("Jump"))
        {
            _rb.gravityScale = _lowJumpFallMultiplier;
        }
        else
        {
            _rb.gravityScale = 1f;
        }
    }

    void Flip()
    {
        _facingRight = !_facingRight;
        transform.Rotate(0f, 180f, 0f);
        if(_onGround)
        {
            CreateDust();
        }
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Platform")
        {
            _onGround = true;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Platform")
        {
            _onGround = false;
        }
    }

    void CreateDust()
    {
        dust.Play();
    }

    public bool canAttack()
    {
        return Mathf.Abs(_horizontalDirection) < 1f &&  _onGround == true;
    }

    
    
}

