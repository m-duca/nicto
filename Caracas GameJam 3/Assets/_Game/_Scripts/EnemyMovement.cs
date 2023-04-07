using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private Vector2 initialMoveDir;

    // Components
    private Rigidbody2D _rb;

    private Vector2 _moveDir;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _moveDir = initialMoveDir;
    }

    private void FixedUpdate()
    {
        _rb.velocity = _moveDir * moveSpeed;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        switch (col.gameObject.tag)
        {
            case "Right":
                _moveDir = Vector2.right;
                break;

            case "Left":
                _moveDir = Vector2.left;
                break;

            case "Up":
                _moveDir = Vector2.up;
                break;

            case "Down":
                _moveDir = Vector2.down;
                break;
        }
    }
}
