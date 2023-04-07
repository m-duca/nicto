using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    [SerializeField] private Direction direction;
    [SerializeField] private Animator _anim;

    private enum Direction
    {
        Right,
        Left,
        Up,
        Down
    }

    private void Awake()
    {
        switch (direction)
        {
            case Direction.Right:
                _anim.Play("Arrow Right Animation");
                break;

            case Direction.Left:
                _anim.Play("Arrow Left Animation");
                break;

            case Direction.Up:
                _anim.Play("Arrow Up Animation");
                break;

            case Direction.Down:
                _anim.Play("Arrow Down Animation");
                break;
        }
    }
}
