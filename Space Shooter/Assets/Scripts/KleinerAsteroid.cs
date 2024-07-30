using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KleinerAsteroid : MonoBehaviour
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
        transform.Rotate(new Vector3(180, 180, 0) * Time.deltaTime);
        transform.Translate(Vector3.back * speed * Time.deltaTime, Space.World);

        if (TimerFinished())
        {
            Destroy(gameObject);
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("laser"))
        {
            Debug.Log("Treffer");
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}
