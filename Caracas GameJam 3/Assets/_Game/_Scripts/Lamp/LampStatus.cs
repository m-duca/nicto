using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LampStatus : MonoBehaviour
{
    // Unity Access Fields
    [Header("Lamp Sprites:")]
    [SerializeField] private Sprite lampOn;
    [SerializeField] private Sprite lampOff;

    [Header("Timer:")] 
    [SerializeField] private float changeTime;

    [Header("Status:")]
    [SerializeField] private bool isOn = false;

    [Header("Dark:")] 
    [SerializeField] private FadeVFX dark;

    [Header("Score:")] 
    public int ScoreValue;

    // Components
    private SpriteRenderer _spr;

    private void Start()
    {
        _spr = GetComponent<SpriteRenderer>();
        StartCoroutine(ChangeStatus(changeTime));
    }

    private IEnumerator ChangeStatus(float t)
    {
        yield return new WaitForSeconds(t);
        isOn = !isOn;
        if (_spr.sprite == lampOn)
        {
            _spr.sprite = lampOff;
            dark.StartFade(FadeVFX.FadeType.FadeIn);
        }
        else
        {
            _spr.sprite = lampOn;
            dark.StartFade(FadeVFX.FadeType.FadeOut);
        }

        StartCoroutine(ChangeStatus(changeTime));
    }

    public bool IsOn()
    {
        return isOn;
    }
}
