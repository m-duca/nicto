using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private Vector2 moveDir;
    [SerializeField] private Transform trigger;
    [SerializeField] private float triggerCooldownTime;

    // Components
    private Rigidbody2D _rb;
    private FadeVFX _fade;

    private Vector3 _warpPos;
    private Vector2 _lastDir;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _fade = GetComponent<FadeVFX>();
    }

    private void Update()
    {
        if (!_fade.isFading && _warpPos != Vector3.zero)
        {
            transform.position = _warpPos;
            moveDir = _lastDir;
            _warpPos = Vector3.zero;
        }
    }

    private void FixedUpdate()
    {
        _rb.velocity = moveDir * moveSpeed;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.layer == 11)
        {
            _lastDir = moveDir;
            moveDir = Vector2.zero;
            _fade.StartFade(FadeVFX.FadeType.FadeOut);
            StartCoroutine(TriggerCooldown(triggerCooldownTime, trigger.gameObject.GetComponent<BoxCollider2D>()));
            _warpPos = trigger.position;
        }
    }

    private IEnumerator TriggerCooldown(float t, BoxCollider2D hitbox)
    {
        hitbox.enabled = false;
        yield return new WaitForSeconds(t);
        hitbox.enabled = true;
    }
}
