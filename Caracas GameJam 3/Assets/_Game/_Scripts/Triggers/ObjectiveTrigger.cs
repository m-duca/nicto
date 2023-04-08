using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ObjectiveTrigger : MonoBehaviour
{
    public string Level;
    [SerializeField] private GameObject[] objectiveArrows;
    [SerializeField] private GameObject[] nextArrows;
    [SerializeField] private GameObject levelTrigger;
    [SerializeField] private GameObject wall;
    [SerializeField] private GameObject icon;
    [SerializeField] private GameObject nextCheckMark;
    [SerializeField] private Image checkMark;
    [SerializeField] private Sprite checkSpr;

    private bool completed = false;

    private void Start()
    {
        Level = SceneManager.GetActiveScene().name;
    }

    public void CompleteObjective()
    {
        if (!completed)
        {
            foreach (var arrow in objectiveArrows)
            {
                Destroy(arrow);
            }

            foreach (var arrow in nextArrows)
            {
                arrow.SetActive(true);
            }

            Destroy(wall);
            levelTrigger.SetActive(true);

            icon.SetActive(true);
            checkMark.sprite = checkSpr;

            nextCheckMark.SetActive(true);

            completed = true;
        }
    }
}
