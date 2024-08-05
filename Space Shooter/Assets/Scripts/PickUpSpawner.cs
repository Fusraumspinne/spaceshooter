using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpSpawner : MonoBehaviour
{
    [SerializeField] private GameObject pick_up;
    [SerializeField] private float cooldown_time_pick_up;
    private float last_trigger_time_pick_up;

    private void Update()
    {
        if (Time.time > last_trigger_time_pick_up + cooldown_time_pick_up)
        {
            Vector3 spawnPosition = new Vector3(Random.Range(-20, 20), 0, 25);
            Instantiate(pick_up, spawnPosition, pick_up.transform.rotation);
            last_trigger_time_pick_up = Time.time;
        }
    }
}
