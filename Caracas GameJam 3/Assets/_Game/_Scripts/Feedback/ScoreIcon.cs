using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreIcon : MonoBehaviour
{
    [SerializeField] private float lifeTime;
    [SerializeField] private float verticalSpeed;

    private void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    private void FixedUpdate()
    {
        transform.position += Vector3.up * verticalSpeed * Time.fixedDeltaTime;
    }
}
