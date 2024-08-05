using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float destroy_timer;
    [SerializeField] private int score_value;
    [SerializeField] private int xp_value;
    [SerializeField] private List<string> destroy_tags;
    bool TimerFinished()
    {
        destroy_timer -= Time.deltaTime;

        if (destroy_timer <= 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void Update()
    {
        transform.Rotate(new Vector3(180, 180, 0) * Time.deltaTime);
        transform.Translate(Vector3.back * speed * Time.deltaTime, Space.World);

        if (TimerFinished())
        {
            Destroy(gameObject);
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (destroy_tags.Contains(other.tag))
        {
            int currentScore = PlayerPrefs.GetInt("Score", 0);
            currentScore += score_value;

            PlayerPrefs.SetInt("Score", currentScore);

            float currentXp = PlayerPrefs.GetFloat("Xp", 0);

            if (PlayerPrefs.GetInt("Xpmulti") == 1) 
            {
                currentXp += xp_value * 3;
            }
            else
            {
                currentXp += xp_value;
            }

            PlayerPrefs.SetFloat("Xp", currentXp);
            PlayerPrefs.Save();

            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}
