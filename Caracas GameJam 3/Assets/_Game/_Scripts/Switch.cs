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

    [HideInInspector] public BoxCollider2D Hitbox;

    private void Start()
    {
        Hitbox = GetComponent<BoxCollider2D>();

        if (nextSwitch != null)
        {
            nextSwitch.LampIconSpr.enabled = false;
            nextSwitch.Hitbox.enabled = false;
        }
    }

    private void Update()
    {
        lampIcon.SetBool("isOn", !lamps[0].IsOn);
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
}
