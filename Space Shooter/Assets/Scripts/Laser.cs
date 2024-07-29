using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float destroy_timer;

    private void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);

        if (TimerFinished())
        {
            Destroy(gameObject);
        }
    }

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

}
