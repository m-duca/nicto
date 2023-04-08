using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCollision : MonoBehaviour
{
    [Header("Increase Score:")] 
    [SerializeField] private int increase;
    [SerializeField] private GameObject scoreIconPrefab;

    [Header("Decrease Score:")]
    [SerializeField] private float decreaseScoreTime;
    [SerializeField] private int decrease;

    [Header("Collision Layers:")] 
    [SerializeField] private int enemyLayer;
    [SerializeField] private int lampLayer;
    [SerializeField] private int switchLayer;
    [SerializeField] private int objectiveLayer;
    [SerializeField] private int endLayer;

    // References
    private AudioManager _audioManager;

    // Components
    private SpriteRenderer _spr;
    private PlayerMovement _playerMovement;

    // Score
    public static int Score;

    // Switch
    private Switch _currentSwitch;

    // Sort Order
    //private int _defaultOrder;

    // Decrease Score
    private bool _isDecreasing = false;

    public static bool isEnd = false;

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
        _playerMovement = GetComponent<PlayerMovement>();
        _audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        //_defaultOrder = _spr.sortingOrder;

        if (Score != 0 && !isEnd)
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
                Instantiate(scoreIconPrefab, transform.position, Quaternion.identity);
            }
            _currentSwitch.ChangeLamps();
            _audioManager.PlaySFX("Lampada ligando");
        }

        if (Input.GetButtonDown("Jump") && _currentObjective != null)
        {
            _currentObjective.CompleteObjective();
            _audioManager.PlaySFX("ObjectiveMet");
            StartCoroutine(PlayObjectiveFeedbackSFX(1f, _currentObjective.Level));
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
        else if (col.gameObject.layer == endLayer)
        {
            _audioManager.PlaySFX("ObjectiveMet");
            DisableMove();
            Invoke("End", 0.95f);
        }
    }

    private void ChangeScore(int value)
    {
        Score += value;
        if (Score <= 0) GameOver(GameOvers.Time);
    }

    private void GameOver(GameOvers type)
    {
        DisableMove();
        switch (type)
        {
            case GameOvers.Dark:
                StartCoroutine(Restart(1f, SceneManager.GetActiveScene().name));
                break;
            case GameOvers.Time:
                Score = 0;
                StartCoroutine(Restart(1f, "Start"));
                break;

            case GameOvers.Monster:
                _audioManager.PlaySFX("Risada " + Random.Range(1, 3).ToString());
                StartCoroutine(Restart(0.65f, SceneManager.GetActiveScene().name));
                break;
        }
    }

    private IEnumerator DecreaseScore(float t)
    {
        yield return new WaitForSeconds(t);
        ChangeScore(-decrease);

        StartCoroutine(DecreaseScore(decreaseScoreTime));
    }

    private IEnumerator Restart(float t, string sceneName)
    {
        yield return new WaitForSeconds(t);
        SceneManager.LoadScene(sceneName);
    }

    private void End()
    {
        StopAllCoroutines();
        isEnd = true;
        SceneManager.LoadScene("End");
    }

    private void DisableMove()
    {
        _playerMovement.CanMove = false;
    }

    private IEnumerator PlayObjectiveFeedbackSFX(float t, string level)
    {
        yield return new WaitForSeconds(t);
        if (level == "Kitchen") _audioManager.PlaySFX("Bebendo");
        else if (level == "Bathroom") _audioManager.PlaySFX("Mijando");
    }
}
