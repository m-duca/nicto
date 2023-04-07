using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCollision : MonoBehaviour
{
    [Header("Increase Score:")] 
    [SerializeField] private int increase;

    [Header("Decrease Score:")]
    [SerializeField] private float decreaseScoreTime;
    [SerializeField] private int decrease;

    [Header("Collision Layers:")] 
    [SerializeField] private int enemyLayer;
    [SerializeField] private int lampLayer;
    [SerializeField] private int switchLayer;
    [SerializeField] private int objectiveLayer;

    // Components
    private SpriteRenderer _spr;

    // Score
    public static int Score;

    // Switch
    private Switch _currentSwitch;

    // Sort Order
    //private int _defaultOrder;

    // Decrease Score
    private bool _isDecreasing = false;

    // Objective
    private ObjectiveTrigger _currentObjective;

    private enum GameOvers
    {
        Monster,
        Time,
        Dark
    }

    private void Start()
    {
        _spr = GetComponent<SpriteRenderer>();
        //_defaultOrder = _spr.sortingOrder;

        if (Score != 0)
        {
            _isDecreasing = true;
            StartCoroutine(DecreaseScore(decreaseScoreTime));
        }
    }

    private void Update()
    {
        if (Input.GetButtonDown("Jump") && _currentSwitch != null)
        {
            if (_currentSwitch.lamps[0].VisibleDark && !_currentSwitch.AlreadyScored)
            {
                ChangeScore(increase);
                _currentSwitch.AlreadyScored = true;
            }
            _currentSwitch.ChangeLamps();
        }

        if (Input.GetButtonDown("Jump") && _currentObjective != null)
        {
            _currentObjective.CompleteObjective();
        }

        if (Score != 0 && !_isDecreasing)
        {
            _isDecreasing = true;
            StartCoroutine(DecreaseScore(decreaseScoreTime));
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
        else if (col.gameObject.layer == objectiveLayer)
        {
            _currentObjective = col.gameObject.GetComponent<ObjectiveTrigger>();
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.layer == switchLayer)
        {
            _currentSwitch.StartCooldown();
            _currentSwitch = null;
        }
        else if (col.gameObject.layer == objectiveLayer)
        {
            _currentObjective = null;
        }

        //if (col.gameObject.layer == lampLayer) _spr.sortingOrder = _defaultOrder;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.layer == enemyLayer)
        {
            GameOver(GameOvers.Monster);
        }

        if (col.gameObject.layer == lampLayer)
        {
            var lamp = col.gameObject.GetComponent<LampStatus>();
            //_spr.sortingOrder = lamp.darkSpr.sortingOrder;

            if (lamp.VisibleDark && !lamp.IsOn)
            {
                lamp.IsOn = true;
                lamp.ChangeLight();
            }
        }
    }

    private void ChangeScore(int value)
    {
        Score += value;
        if (Score <= 0) GameOver(GameOvers.Time);
    }

    private void GameOver(GameOvers type)
    {
        switch (type)
        {
            case GameOvers.Dark:
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                break;
            case GameOvers.Time:
                Score = 0;
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                break;

            case GameOvers.Monster:
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                break;
        }
    }

    private IEnumerator DecreaseScore(float t)
    {
        yield return new WaitForSeconds(t);
        ChangeScore(-decrease);

        StartCoroutine(DecreaseScore(decreaseScoreTime));
    }
}
