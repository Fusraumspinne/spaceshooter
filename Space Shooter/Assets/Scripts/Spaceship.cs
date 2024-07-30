using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spaceship : MonoBehaviour
{
    [SerializeField] private float move_speed;
    [SerializeField] private GameObject laser_object;
    [SerializeField] private GameObject cannon_object_1;
    [SerializeField] private GameObject cannon_object_2;
    [SerializeField] private bool cannon_active_switch;

    [SerializeField] private float cooldown_time_laser;
    private float last_trigger_time_laser;

    private void Update()
    {
        Movement();
        Shooting();
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

    void Shooting()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (Time.time > last_trigger_time_laser + cooldown_time_laser)
            {
                if (cannon_active_switch) 
                {
                    Instantiate(laser_object, cannon_object_1.transform.position, laser_object.transform.rotation);
                    cannon_active_switch = false;
                }
                else
                {
                    Instantiate(laser_object, cannon_object_2.transform.position, laser_object.transform.rotation);
                    cannon_active_switch = true;
                }

                last_trigger_time_laser = Time.time;
            }
        }
    }

}
