using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveObjectTrigger : MonoBehaviour
{
    [SerializeField] private GameObject target;
    [SerializeField] private CollisionLayers collisionLayers;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.layer == collisionLayers.PlayerLayer)
        {
            Destroy(gameObject, 5f);
        }
    }

    private void OnDestroy()
    {
        target.SetActive(true);
    }
}
