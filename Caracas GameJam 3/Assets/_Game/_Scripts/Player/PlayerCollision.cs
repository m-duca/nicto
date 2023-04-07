using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayerCollision : MonoBehaviour
{
    [Header("Collision Layers:")] 
    [SerializeField] private int enemyLayer;
    [SerializeField] private int lampLayer;

    [Header("Score:")] 
    [SerializeField] private float scoreCoolDown;

    // Score
    public static int Score = 0;
    private bool _canScore = true;

    private enum GameOvers
    {
        Monster,
        Time,
        Dark
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.layer == lampLayer)
        {
            var lamp = col.gameObject.GetComponent<LampStatus>();
            if (!lamp.IsOn()) GameOver(GameOvers.Dark);
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.layer == enemyLayer)
        {
            GameOver(GameOvers.Monster);
        }
        else if (col.gameObject.layer == lampLayer)
        {
            var lamp = col.gameObject.GetComponent<LampStatus>();
            if (lamp.IsOn() && _canScore)
            {
                _canScore = false;
                ChangeScore(lamp.ScoreValue);
                StartCoroutine(SetScoreCooldown(scoreCoolDown));
            }
        }
    }

    private IEnumerator SetScoreCooldown(float t)
    {
        yield return new WaitForSeconds(t);
        _canScore = true;
    }

    private void ChangeScore(int value)
    {
        Score += value;
        if (Score <= 0) GameOver(GameOvers.Time);
    }

    private void GameOver(GameOvers type)
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
