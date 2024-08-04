using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int score;
    [SerializeField] private TMP_Text score_tmp;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void FixedUpdate()
    {
        score = PlayerPrefs.GetInt("Score");
        score_tmp.text = "Score: " + score.ToString();
    }
}
