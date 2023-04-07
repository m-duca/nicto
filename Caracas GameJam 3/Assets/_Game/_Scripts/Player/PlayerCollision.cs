using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayerCollision : MonoBehaviour
{
    [Header("Collision Layers:")] 
    [SerializeField] private int enemyLayer;
    [SerializeField] private int lampLayer;
    [SerializeField] private int switchLayer;

    // Components
    private SpriteRenderer _spr;

    // Score
    public static int Score = 0;

    // Switch
    private Switch _currentSwitch;

    // Sort Order
    private int _defaultOrder;

    private enum GameOvers
    {
        Monster,
        Time,
        Dark
    }

    private void Start()
    {
        _spr = GetComponent<SpriteRenderer>();
        _defaultOrder = _spr.sortingOrder;
    }

    private void Update()
    {
        if (Input.GetButtonDown("Jump") && _currentSwitch != null)
        {
            if (_currentSwitch.lamps[0].VisibleDark) ChangeScore(100);
            _currentSwitch.ChangeLamps();
        }
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.layer == lampLayer)
        {
            var lamp = col.gameObject.GetComponent<LampStatus>();
            if (!lamp.IsOn && !lamp.VisibleDark) GameOver(GameOvers.Dark);
        }
        else if (col.gameObject.layer == switchLayer)
        {
            _currentSwitch = col.gameObject.GetComponent<Switch>();
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.layer == switchLayer)
        {
            _currentSwitch = null;
        }

        if (col.gameObject.layer == lampLayer) _spr.sortingOrder = _defaultOrder;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.layer == enemyLayer)
        {
            GameOver(GameOvers.Monster);
        }

        if (col.gameObject.layer == lampLayer) _spr.sortingOrder = col.gameObject.GetComponent<LampStatus>().darkSpr.sortingOrder;
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
