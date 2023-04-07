using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeVFX : MonoBehaviour
{
    // Unity Access Fields
    [SerializeField] private float fadeInSpeed;
    [SerializeField] private float fadeOutSpeed;

    private bool isFading = false;
    private FadeType type;

    public enum FadeType
    {
        FadeOut,
        FadeIn
    }

    // Components
    private SpriteRenderer _spr;

    private void Start()
    {
        _spr = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (isFading) ApplyFade();
    }

    public void StartFade(FadeType t)
    {
        type = t;
        isFading = true;
    }

    private void ApplyFade()
    {
        var color = _spr.color;
        var alpha = color.a;

        if (type == FadeType.FadeOut)
        {
            if (alpha > 0.0f)
            {
                alpha -= fadeOutSpeed * Time.deltaTime;
                color.a = alpha;
                _spr.color = color;
            }
            else
            {
                isFading = false;
                if (gameObject.layer == 8) gameObject.transform.parent.GetComponent<LampStatus>().IsOn = true;
            }
        }
        else
        {
            if (alpha < 1.0f)
            {
                alpha += fadeInSpeed * Time.deltaTime;
                color.a = alpha;
                _spr.color = color;
            }
            else
            {
                isFading = false;
                if (gameObject.layer == 8)
                {
                    var lampStatus = gameObject.transform.parent.GetComponent<LampStatus>();
                    lampStatus.IsOn = false;
                    lampStatus.VisibleDark = false;
                }
            }
        }
    }
}
