using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int score;
    [SerializeField] private TMP_Text score_tmp;

    [SerializeField] private float xp;
    [SerializeField] private int level;
    [SerializeField] private Slider xp_slider;
    [SerializeField] private TMP_Text level_tmp;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        level = PlayerPrefs.GetInt("Level");
    }

    private void FixedUpdate()
    {
        score = PlayerPrefs.GetInt("Score");
        score_tmp.text = "Score: " + score.ToString();

        xp = PlayerPrefs.GetFloat("Xp");

        if(xp_slider.maxValue <= xp)
        {
            xp -= xp_slider.maxValue;
            PlayerPrefs.SetFloat("Xp", xp);
            PlayerPrefs.Save();

            level++;

            PlayerPrefs.SetInt("Level", level);
            PlayerPrefs.Save();
        }
        else
        {
            xp_slider.value = xp;
        }

        level_tmp.text = level.ToString();
    }
}
