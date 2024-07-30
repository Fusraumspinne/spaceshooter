using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackingRocket : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private float rotationSpeed = 50f;

    private Transform target;
    private bool trackingEnabled = false;

    public void SetTarget(GameObject newTarget)
    {
        if (newTarget != null)
        {
            target = newTarget.transform;
        }
    }

    void Start()
    {
        StartCoroutine(EnableTrackingAfterDelay(0.25f));
    }

    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        if (!trackingEnabled)
        {
            transform.position += transform.forward * speed * Time.deltaTime;
        }
        else
        {
            Vector3 direction = (target.position - transform.position).normalized;

            float step = rotationSpeed * Time.deltaTime;
            Vector3 newDirection = Vector3.RotateTowards(transform.up, direction, step, 0.0f);
            transform.up = newDirection;

            transform.position += transform.up * speed * Time.deltaTime;
        }
    }

    private IEnumerator EnableTrackingAfterDelay(float delay)
    {
        // Warte die angegebene Zeitspanne
        yield return new WaitForSeconds(delay);
        // Aktiviere das Tracking
        trackingEnabled = true;
    }
}
