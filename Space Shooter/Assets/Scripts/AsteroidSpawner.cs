using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    [SerializeField] private GameObject asteroid;
    [SerializeField] private float base_cooldown_time_asteroid;
    [SerializeField] private float cooldown_reduction;
    [SerializeField] private int levels_per_reduction;
    [SerializeField] private int max_level_for_reduction;
    [SerializeField] private float cooldown_time_asteroid;
    private float last_trigger_time_asteroid;

    [SerializeField] private int level;

    private void Start()
    {
        UpdateCooldownTime();
    }

    private void Update()
    {
        level = PlayerPrefs.GetInt("Level", 1);
        UpdateCooldownTime();

        if (Time.time > last_trigger_time_asteroid + cooldown_time_asteroid)
        {
            Vector3 spawnPosition = new Vector3(Random.Range(-25, 25), 0, 25);
            Instantiate(asteroid, spawnPosition, asteroid.transform.rotation);
            last_trigger_time_asteroid = Time.time;
        }
    }

    void UpdateCooldownTime()
    {
        int reduction_steps = Mathf.Min(level / levels_per_reduction, max_level_for_reduction / levels_per_reduction);
        cooldown_time_asteroid = base_cooldown_time_asteroid - (reduction_steps * cooldown_reduction);
    }
}
