using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuInput : MonoBehaviour
{
    [SerializeField] private string nextLevel; 

    private void Update()
    {
        if (Input.GetButtonDown("Jump")) SceneManager.LoadScene(nextLevel);
    }
}
