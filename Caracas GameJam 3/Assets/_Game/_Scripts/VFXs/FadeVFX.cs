using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeVFX : MonoBehaviour
{
    // Unity Access Fields
    [SerializeField] private float fadeInSpeed;
    [SerializeField] private float fadeOutSpeed;

    [HideInInspector] public bool isFading = false;
    private FadeType type;
    private int lampLayer = 8;
    private int switchLayer = 10;
    private int enemyLayer = 3;

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
                if (gameObject.layer == lampLayer)
                {
                    gameObject.transform.parent.GetComponent<LampStatus>().IsOn = true;
                }
                else if (gameObject.layer == enemyLayer)
                {
                    StartCoroutine(EnemyAppearing());
                }
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
                if (gameObject.layer == lampLayer)
                {
                    var lampStatus = gameObject.transform.parent.GetComponent<LampStatus>();
                    lampStatus.IsOn = false;
                    lampStatus.VisibleDark = false;
                }
                else if (gameObject.layer == switchLayer)
                {
                    gameObject.SetActive(false);
                }
            }
        }
    }

    private IEnumerator EnemyAppearing()
    {
        yield return new WaitForSeconds(1f);
        StartFade(FadeType.FadeIn);
    }
}
