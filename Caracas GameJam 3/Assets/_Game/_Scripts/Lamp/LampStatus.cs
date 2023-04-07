using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LampStatus : MonoBehaviour
{
    // Unity Access Fields
    [Header("Status:")]
    public bool IsOn = false;
    public bool IsFalling = false;
    public bool VisibleDark = true;

    [Header("Dark:")] 
    [SerializeField] private FadeVFX dark;
    public SpriteRenderer darkSpr;

    public void ChangeLight()
    {
        IsOn = !IsOn;
        if (IsOn)
        {
            dark.StartFade(FadeVFX.FadeType.FadeOut);
            IsFalling = false;
        }
        else
        {
            dark.StartFade(FadeVFX.FadeType.FadeIn); 
            IsFalling = true;
        }
    }
}
