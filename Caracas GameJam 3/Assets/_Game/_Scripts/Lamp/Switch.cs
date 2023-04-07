using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    // Unity Access Fields
    public LampStatus[] lamps;
    [SerializeField] private Animator lampIcon;
    public SpriteRenderer LampIconSpr;
    [SerializeField] private Switch nextSwitch;
    [SerializeField] private float cooldownTime;

    [HideInInspector] public BoxCollider2D Hitbox;
    [HideInInspector] public bool AlreadyScored = false;

    // Components
    [SerializeField] private FadeVFX _fade;
    [SerializeField] private bool canFade;

    private void Awake()
    {
        Hitbox = GetComponent<BoxCollider2D>();
    }

    private void Start()
    {
        if (nextSwitch != null)
        {
            nextSwitch.LampIconSpr.enabled = false;
            nextSwitch.Hitbox.enabled = false;
        }
    }

    private void Update()
    {
        lampIcon.SetBool("isOn", !lamps[0].IsOn);

        if (!lamps[0].IsOn && !lamps[0].VisibleDark && canFade) _fade.StartFade(FadeVFX.FadeType.FadeOut);
    }

    public void ChangeLamps()
    {
        foreach (var lamp in lamps)
        {
            lamp.ChangeLight();
        }

        if (nextSwitch != null && lamps[0].IsFalling) 
        {
            nextSwitch.LampIconSpr.enabled = true;
            nextSwitch.Hitbox.enabled = true;
        }
    }

    public void StartCooldown()
    {
        if (lamps[0].IsFalling)
        {
            Hitbox.enabled = false;
            StartCoroutine(SetCooldown(cooldownTime));
        }
    }

    private IEnumerator SetCooldown(float t)
    {
        yield return new WaitForSeconds(t);
        Hitbox.enabled = true;
    }
}
