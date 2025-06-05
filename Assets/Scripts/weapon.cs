using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weapon : MonoBehaviour
{
    public Bullets bullet;
    private float lastFireTime;
    private float lastFireTimeSpell;
    private float fireCooldown = 0.2f;
    private float fireCooldownSpell = 2f;
    public PlayerMovement player;
    private bool cooldown = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        if (player.isDead == false)
        {
            if (Input.GetKey(KeyCode.JoystickButton7) || Input.GetKey(KeyCode.Mouse0))
            {
                if (Time.time - lastFireTime >= fireCooldown)
                {
                    bullet.shoot();
                    lastFireTime = Time.time; // Setze die Zeit des letzten Schusses auf die aktuelle Zeit

                }
            }
            if (Input.GetKey(KeyCode.JoystickButton7) || Input.GetKey(KeyCode.Mouse1))
            {
                if(cooldown == false)
                {
                    bullet.shootLeft();
                    cooldown = true;
                }
                if (Time.time - lastFireTimeSpell >= fireCooldownSpell)
                {
                    bullet.shootLeft();
                    lastFireTimeSpell = Time.time; // Setze die Zeit des letzten Schusses auf die aktuelle Zeit

                }
            }
        }
    }
}
