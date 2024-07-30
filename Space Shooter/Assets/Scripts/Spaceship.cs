using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spaceship : MonoBehaviour
{
    [SerializeField] private float move_speed;

    [SerializeField] private GameObject laser_object;
    [SerializeField] private GameObject cannon_object_1;
    [SerializeField] private GameObject cannon_object_2;
    [SerializeField] private float cooldown_time_laser;
    private float last_trigger_time_laser;

    [SerializeField] private GameObject normal_rocket;
    [SerializeField] private GameObject normal_rocket_cannon;
    [SerializeField] private float cooldown_time_normal_rocket;
    private float last_trigger_normal_rocket;

    [SerializeField] private GameObject tracking_rocket;
    [SerializeField] private GameObject cannon_tracking_rocket_1;
    [SerializeField] private GameObject cannon_tracking_rocket_2;
    [SerializeField] private GameObject crosshair_rocket; 
    [SerializeField] private Canvas canvas; 
    [SerializeField] private int max_crosshairs;
    [SerializeField] private Transform player_transform;
    [SerializeField] private float rocket_delay;
    [SerializeField] private float cooldown_time_tracking_rocket;
    private float last_trigger_tracking_rocket;

    private List<GameObject> asteroids = new List<GameObject>();
    private List<GameObject> selected_asteroids = new List<GameObject>();
    private Dictionary<GameObject, GameObject> asteroid_to_crosshair_map = new Dictionary<GameObject, GameObject>();

    private void Update()
    {
        Movement();
        ShootingLaser();
        ShootingNormalRocket();
        SelectAsteroids();
        UpdateCrosshairPositions();
        FireTrackingRockets();
    }

    void Movement()
    {
        Vector3 movement = Vector3.zero;

        if (Input.GetKey(KeyCode.A))
        {
            movement = Vector3.left * move_speed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            movement = Vector3.right * move_speed * Time.deltaTime;
        }

        transform.Translate(movement);

        Vector3 position = transform.position;
        position.x = Mathf.Clamp(position.x, -20, 20);
        transform.position = position;
    }

    void ShootingLaser()
    {
        if (asteroid_to_crosshair_map.Count == 0)
        {
            if (Time.time > last_trigger_time_laser + cooldown_time_laser)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    Instantiate(laser_object, cannon_object_1.transform.position, laser_object.transform.rotation);

                    last_trigger_time_laser = Time.time;
                }

                if (Input.GetMouseButtonDown(1))
                {
                    Instantiate(laser_object, cannon_object_2.transform.position, laser_object.transform.rotation);

                    last_trigger_time_laser = Time.time;
                }
            }
        }
    }

    void ShootingNormalRocket()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if(Time.time > last_trigger_normal_rocket + cooldown_time_normal_rocket)
            {
                Instantiate(normal_rocket, normal_rocket_cannon.transform.position, normal_rocket.transform.rotation);

                last_trigger_normal_rocket = Time.time;
            }
        }
    }

    void SelectAsteroids()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (Time.time > last_trigger_tracking_rocket + cooldown_time_tracking_rocket)
            {
                if (asteroid_to_crosshair_map.Count > 0)
                {
                    ClearAllCrosshairs();
                }
                else
                {
                    asteroids.Clear();
                    GameObject[] all_asteroids = GameObject.FindGameObjectsWithTag("kleiner_asteroid");
                    asteroids.AddRange(all_asteroids);

                    asteroids.Sort((a, b) => Vector3.Distance(player_transform.position, a.transform.position).CompareTo(Vector3.Distance(player_transform.position, b.transform.position)));

                    int count = Mathf.Min(max_crosshairs, asteroids.Count);

                    for (int i = 0; i < count; i++)
                    {
                        GameObject asteroid = asteroids[i];
                        GameObject crosshair = SpawnCrosshair(asteroid);
                        asteroid_to_crosshair_map.Add(asteroid, crosshair);
                    }
                }

                last_trigger_tracking_rocket = Time.time;
            }
        }
    }

    void ClearAllCrosshairs()
    {
        foreach (var entry in asteroid_to_crosshair_map)
        {
            Destroy(entry.Value);
        }
        asteroid_to_crosshair_map.Clear();
    }

    GameObject SpawnCrosshair(GameObject asteroid)
    {
        GameObject crosshair = Instantiate(crosshair_rocket, canvas.transform);

        Vector3 screen_position = Camera.main.WorldToScreenPoint(asteroid.transform.position);

        crosshair.transform.position = screen_position;

        return crosshair;
    }

    void UpdateCrosshairPositions()
    {
        List<GameObject> asteroids_to_remove = new List<GameObject>();

        foreach (var entry in asteroid_to_crosshair_map)
        {
            GameObject asteroid = entry.Key;
            GameObject crosshair = entry.Value;

            if (asteroid == null)
            {
                Destroy(crosshair);
                asteroids_to_remove.Add(asteroid);
            }
            else
            {
                Vector3 screen_position = Camera.main.WorldToScreenPoint(asteroid.transform.position);
                crosshair.transform.position = screen_position;
            }
        }

        foreach (GameObject asteroid in asteroids_to_remove)
        {
            asteroid_to_crosshair_map.Remove(asteroid);
        }
    }

    void FireTrackingRockets()
    {
        if (Input.GetMouseButtonDown(0) && asteroid_to_crosshair_map.Count > 0)
        {
            StartCoroutine(FireRocketsCoroutine());
        }
    }

    private IEnumerator FireRocketsCoroutine()
    {
        List<GameObject> targets = new List<GameObject>(asteroid_to_crosshair_map.Keys);

        foreach (GameObject target in targets)
        {
            bool switch_cannon = Random.value > 0.5f;
            GameObject spawnPosition = switch_cannon ? cannon_tracking_rocket_1 : cannon_tracking_rocket_2;
            GameObject rocket = Instantiate(tracking_rocket, spawnPosition.transform.position, spawnPosition.transform.rotation);

            TrackingRocket rocketTracker = rocket.GetComponent<TrackingRocket>();
            if (rocketTracker != null)
            {
                rocketTracker.SetTarget(target);
            }
            else
            {
                Debug.LogError("TrackingRocket component not found on tracking rocket prefab.");
            }

            yield return new WaitForSeconds(rocket_delay); 
        }
    }
}
