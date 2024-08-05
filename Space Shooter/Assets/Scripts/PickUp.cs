using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float destroy_timer;

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
        transform.Translate(Vector3.back * speed * Time.deltaTime, Space.World);

        if (TimerFinished())
        {
            Destroy(gameObject);
        }
    }
}
