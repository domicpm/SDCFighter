using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 10f;
    private Rigidbody2D rb;
    private Vector2 mousePos;
    public float angle;
    public const float maxhp = 500;
    public PlayerHealthBar hp;
    public float damageFromEnemy = 5;
    public Text Hp;
    public Heal heal;
    public float healamount = 10;
    public float newhp;
    [HideInInspector] public int potamount = 3;
    public bool PlayerGotDamage;
    public PotImageUI potui;
    public Bullets s;
    public bool isDead = false;
    private bool canDash = true;
    private bool isDashing;
    private float dashingPower = 60f;
    private float dashingTime = 0.1f;
    private float dashingCooldown = 1f;
    public Enemy enemy;
    [SerializeField] private TrailRenderer tr;
    public GameObject player;
    public RotatePlayerSprite playerSprite;
    // Start is called before thefirst frame update
    void Start()
    {
        newhp = maxhp;
        hp.setPlayerMaxHealth(maxhp);
        rb = GetComponent<Rigidbody2D>();
        Hp.text = hp.getPlayerHealth().ToString();
        potamount = 3;
    }
    // Update is called once per frame
    void Update()
    {
        if (isDead == false)
        {
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");


            //controller right stick (aiming)


            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            angle = Mathf.Atan2(mousePos.y - transform.position.y, mousePos.x - transform.position.x) * Mathf.Rad2Deg - 90f;
            Vector3 movement = transform.position + new Vector3(horizontalInput, verticalInput, 0) * speed * Time.deltaTime;
            transform.position = movement;

            //float rightStickX = Input.GetAxis("RightStickHorizontal");
            //float rightStickY = Input.GetAxis("RightStickVertical");
            //Vector2 lookDirection = new Vector2(rightStickX, rightStickY);

            ////when stick is moved
            //if (lookDirection.sqrMagnitude > 0.01f)
            //{
            //    float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;
            //    transform.rotation = Quaternion.Euler(0f, 0f, angle - 90f);
            //}

            if (Input.GetKeyDown(KeyCode.H))
            {
                if (potamount > 0 && newhp < maxhp)
                {
                    newhp = newhp + healamount;
                    if (newhp > maxhp)
                    {
                        newhp = maxhp;
                    }
                    potamount--;
                    hp.setPlayerHealth(newhp);
                    Hp.text = newhp.ToString();

                }
                else { Debug.Log("Keine Pots"); }
            }
            if (Input.GetKeyDown(KeyCode.Joystick1Button1) || Input.GetKeyDown(KeyCode.Space))
            {
                StartCoroutine(Dash());
            }

        }

        IEnumerator Dash()
        {
            Vector2 originalVelocity = rb.velocity;
            float originalGravity = rb.gravityScale;
            rb.gravityScale = 0f;
            tr.emitting = true;
            float dashDirectionX = Input.GetAxisRaw("Horizontal");
            float dashDirectionY = Input.GetAxisRaw("Vertical");

            if (dashDirectionX != 0)
            {
                rb.velocity = new Vector2(dashDirectionX * dashingPower, rb.velocity.y);

            }
            else if (dashDirectionY != 0)
            {
                rb.velocity = new Vector2(rb.velocity.x, dashDirectionY * dashingPower);
            }
            else
            {
                Debug.Log("No dash input detected");
            }
            yield return new WaitForSeconds(dashingTime);
            tr.emitting = false;
            rb.velocity = originalVelocity;
            rb.gravityScale = originalGravity;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("HealPot"))
        {
            increasepot();
        }
            if (collision.gameObject.CompareTag("AttackSpeedBuff"))
            {
            s.speed += 5;
            }

            if (collision.gameObject.CompareTag("Enemy"))
            {
            //PlayerGotDamage = true;
            //newhp = newhp - damageFromEnemy;
            //hp.setPlayerHealth(newhp);
            //Hp.text = newhp.ToString();
            //if (newhp <= 0 && !isDead)
            //{
            //    isDead = true;
            //    playerSprite.setDeadAnimation();
            //}
        }

        if (collision.gameObject.CompareTag("FireBall"))
            {
            playerSprite.setGotHitAnimation();
            newhp = newhp - damageFromEnemy;
            hp.setPlayerHealth(newhp);
            Hp.text = newhp.ToString();
            if (newhp <= 0 && !isDead)
            {
                Hp.text = "0";
                isDead = true;
                playerSprite.setDeadAnimation();
            }
        }


    }
    public void  increasepot()
    {
        potamount++;
    }

    public void destroyObj()
    {
        gameObject.SetActive(false);
    }
    public bool getDead()
    {
        return isDead;
    }

}