using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuInput : MonoBehaviour
{
    [SerializeField] private string nextLevel;

    // References
    private AudioManager _audioManager;

    private bool _played = false;

    private void Awake()
    {
        _audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    private void Start()
    {
        if (SceneManager.GetActiveScene().name == "Start") _audioManager.PlayMusic("Musiquinha");
    }

    private void Update()
    {
        if (Input.GetButtonDown("Jump") && !_played)
        {
            _played = true;
            _audioManager.PlaySFX("ObjectiveMet");
            Invoke("NextLevel", 0.95f);
        }
    }

    private void NextLevel()
    {
        if (SceneManager.GetActiveScene().name == "End")
        {
            PlayerCollision.Score = 0;
            PlayerCollision.isEnd = false;
        }
        SceneManager.LoadScene(nextLevel);
    }
}
