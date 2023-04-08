using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelTrigger : MonoBehaviour
{
    [Header("Settings:")] 
    [SerializeField] private string scene;
    [SerializeField] CollisionLayers collisionLayers;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.layer == collisionLayers.PlayerLayer)
        {
            PlayerCollision.ScoreCount = -1;
            SceneManager.LoadScene(scene);
        }
    }
}
