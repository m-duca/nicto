using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialLampsTrigger : MonoBehaviour
{
    [SerializeField] private FadeVFX lampIcon;
    [SerializeField] private int playerLayer = 9;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.layer == playerLayer)
        {
            lampIcon.StartFade(FadeVFX.FadeType.FadeOut);
        }
    }
}
