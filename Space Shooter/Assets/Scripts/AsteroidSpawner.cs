using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    [SerializeField] private GameObject kleiner_asteroid;
    [SerializeField] private float cooldown_time_kleiner_asteroid;
    private float last_trigger_time_kleiner_asteroid;

    private void Update()
    {
        if (Time.time > last_trigger_time_kleiner_asteroid + cooldown_time_kleiner_asteroid)
        {
            Vector3 spawnPosition = new Vector3(Random.Range(-25, 25), 0, 25);
            Instantiate(kleiner_asteroid, spawnPosition, kleiner_asteroid.transform.rotation);
            last_trigger_time_kleiner_asteroid = Time.time;
        }
    }
}
