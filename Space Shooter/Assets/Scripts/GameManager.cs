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
    [SerializeField] private float max_xp;
    [SerializeField] private int xp_increase_per_level;
    [SerializeField] private int level;
    [SerializeField] private Slider xp_slider;
    [SerializeField] private TMP_Text level_tmp;

    [SerializeField] private bool reset;

    [SerializeField] private bool paused;

    void Start()
    {
        if (reset)
        {
            PlayerPrefs.SetFloat("Xp", 0);
            PlayerPrefs.SetInt("Level", 0);
            PlayerPrefs.SetFloat("MaxXp", 25);
            PlayerPrefs.SetInt("Score", 0);
            PlayerPrefs.SetFloat("Health", 100);
            PlayerPrefs.Save();
        }

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        level = PlayerPrefs.GetInt("Level");

        max_xp = PlayerPrefs.GetFloat("MaxXp");
        xp_slider.maxValue = max_xp;
    }

    private void FixedUpdate()
    {
        score = PlayerPrefs.GetInt("Score");
        score_tmp.text = "Score: " + score.ToString();

        xp = PlayerPrefs.GetFloat("Xp");

        if(xp_slider.maxValue <= xp)
        {
            xp -= xp_slider.maxValue;
            level++;

            max_xp = 25 + (level * xp_increase_per_level);

            PlayerPrefs.SetFloat("Xp", xp);
            PlayerPrefs.SetInt("Level", level);
            PlayerPrefs.SetFloat("MaxXp", max_xp);
            PlayerPrefs.Save();

            xp_slider.maxValue = max_xp;
        }
        else
        {
            xp_slider.value = xp;
        }

        level_tmp.text = level.ToString();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) 
        {
            if (paused)
            {   
                paused = false;
                
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;

                Time.timeScale = 1;
            }
            else
            {
                paused = true;

                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;

                Time.timeScale = 0;
            }
        }
    }
}
