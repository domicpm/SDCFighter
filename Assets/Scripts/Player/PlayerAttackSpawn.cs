using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackSpawn : MonoBehaviour
{
    public Bullets bullet;
    private float lastFireTime;
    private float lastFireTimeSpell;
    [SerializeField]public float fireCooldown = 0.9f;
    public static float fireCooldownSpell = 0f;
    public PlayerMovement player;
    public RotatePlayerSprite rt;
    private bool cooldown = false;
    // Start is called before the first frame update
    void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {
        if (Enemy.allCleared == true)
            return;
        if (player.isDead == false)
        {

            if (Input.GetKey(KeyCode.JoystickButton7) || Input.GetKey(KeyCode.Mouse0) || Input.GetKey(KeyCode.J))
            {
                if (Time.time - lastFireTime >= fireCooldown)
                {
                    bullet.shoot();
                    lastFireTime = Time.time; // Setze die Zeit des letzten Schusses auf die aktuelle Zeit
                }
            }
            if (Input.GetKey(KeyCode.JoystickButton7) || Input.GetKey(KeyCode.Mouse1))
            {
                if (Time.time - lastFireTimeSpell >= fireCooldownSpell)
                {
                    bullet.shootLeft();
                    lastFireTimeSpell = Time.time; // Setze die Zeit des letzten Schusses auf die aktuelle Zeit
                    fireCooldownSpell = 8f;
                }
            }
        }
    }
}
