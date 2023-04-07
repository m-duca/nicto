using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Unity Access Fields
    [Header("Horizontal Movement:")]
    [SerializeField] private float maxSpeed;
    [SerializeField] private float acceleration;

    // Components
    private Rigidbody2D _rb;
    private SpriteRenderer _spr;
    private Animator _anim;

    private Vector2 _moveInput;
    
    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _spr = GetComponent<SpriteRenderer>();
        _anim = GetComponent<Animator>();
    }
    
    private void Update()
    {
        Animate();
        GetMoveInput();
        FlipX();
    }

    private void FixedUpdate()
    {
        ApplyMove();
    }

    private void GetMoveInput()
    {
        if (Input.GetButton("Right")) _moveInput = Vector2.right;
        else if (Input.GetButton("Left")) _moveInput = Vector2.left;
        else if (Input.GetButton("Up")) _moveInput = Vector2.up;
        else if (Input.GetButton("Down")) _moveInput = Vector2.down;
        else _moveInput = Vector2.zero;
    }

    private Vector2 SetVelocity(Vector2 goal, Vector2 curVel, float accel)
    {
        var move = Vector2.zero;

        var velDifferenceX = goal.x - curVel.x;
        var velDifferenceY = goal.y - curVel.y;
        
        if (velDifferenceX > accel) move.x = curVel.x + accel;
        else if (velDifferenceX < -accel) move.x = curVel.x - accel;
        else move.x = goal.x;

        if (velDifferenceY > accel) move.y = curVel.y + accel;
        else if (velDifferenceY < -accel) move.y = curVel.y - accel;
        else move.y = goal.y;
        
        return move;
    }
    
    private void ApplyMove()
    {
        _rb.velocity = SetVelocity(maxSpeed * _moveInput, _rb.velocity, acceleration);
    }

    private void FlipX()
    {
        if (_moveInput.x == -1f) _spr.flipX = true;
        else if (_moveInput.x == 1f) _spr.flipX = false;
    }

    private void Animate()
    {
        if (_moveInput.x != 0 || _moveInput.y != 0) _anim.SetBool("move", true);
        else _anim.SetBool("move", false);
    }
}
