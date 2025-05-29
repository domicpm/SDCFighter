using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weapon : MonoBehaviour
{
    public Bullets bullet;
    private float lastFireTime;
    public float fireCooldown = 0.01f;
    public PlayerMovement player;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        if (player.isDead == false)
        {
            if (Input.GetKey(KeyCode.JoystickButton7) || Input.GetKeyDown(KeyCode.Mouse0))
            {
                if (Time.time - lastFireTime >= fireCooldown)
                {
                    bullet.shoot();
                    lastFireTime = Time.time; // Setze die Zeit des letzten Schusses auf die aktuelle Zeit

                }
            }
        }
    }
}
