using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackingRocket : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float rotationSpeed;

    private Transform target;

    public void SetTarget(GameObject newTarget)
    {
        if (newTarget != null)
        {
            target = newTarget.transform;
        }
    }

    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }


        Vector3 direction = (target.position - transform.position).normalized;

        float step = rotationSpeed * Time.deltaTime;
        Vector3 newDirection = Vector3.RotateTowards(transform.up, direction, step, 0.0f);
        transform.up = newDirection;

        transform.position += transform.up * speed * Time.deltaTime;

    }
}
