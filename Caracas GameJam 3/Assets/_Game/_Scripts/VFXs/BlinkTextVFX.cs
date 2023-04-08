using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class BlinkTextVFX : MonoBehaviour
{
    [SerializeField] private float time;
    [SerializeField] private float interval;
    [SerializeField] private bool playOnStart;
    [SerializeField] private bool undefined;

    // Components
    private TextMeshProUGUI _txt;

    private bool _isBlinking = false;

    private void Awake()
    {
        _txt = GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        if (playOnStart) SetBlink();
    }

    public void SetBlink()
    {
        _isBlinking = true;
        StartCoroutine(ApplyBlink(interval));
        if (!undefined) StartCoroutine(SetBlinkTime(time));
    }

    private IEnumerator SetBlinkTime(float time)
    {
        yield return new WaitForSeconds(time);
        _isBlinking = false;
        _txt.enabled = true;
    }

    private IEnumerator ApplyBlink(float time)
    {
        _txt.enabled = !_txt.enabled;
        yield return new WaitForSeconds(time);
        if (_isBlinking) StartCoroutine(ApplyBlink(interval));
    }
}
