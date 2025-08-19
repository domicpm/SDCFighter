using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector2 mousePos;
    public float angle;

    public static float spellShieldCooldown = 7f;
    public float maxhp = 1;
    public int damageFromEnemy = 3;
    public float healamount = 50;
    public float speed = 8f;
    public int potamount = 0;
    public int lifesteal = 20;
    public int experience = 0;
    public int maxExperience = 500;
    private float dashingPower = 50f;
    private float dashingTime = 0.1f;
    private bool dashOnCooldown = false;
    public static float dashCooldown = 3f;
 
    public float newhp;
    public bool PlayerGotDamage;
   
    public bool isDead = false;
    public bool godmode = false;
    private bool canDash = true;
    private bool isDashing;
    private bool shield;
    private bool onCooldown = false;
    public Enemy enemy;

    private float horizontalInput;
    private float verticalInput;

    [SerializeField] private TrailRenderer tr;
    public PotImageUI potui;
    public Bullets s;
    public PlayerHealthBar healthbar;
    public Text Hp;
    public Text HpBig;
    public ItemDrops heal;
    public RotatePlayerSprite playerSprite;
    public BulletPosition bp;
    public SpellShield spellshield;
    public CooldownUI cdUI;
    public PlayerAttackSpawn wp;
    public GameManager gameManager;
    public Godmode gm;
    public SpellAoE aoe;
    public SkillTree st;
    public static bool isCombo = false;
    private List<Chronobreak> log = new List<Chronobreak>();
    void Start()
    {
        newhp = maxhp;
        healthbar.setPlayerMaxHealth(maxhp);
        rb = GetComponent<Rigidbody2D>();
        Hp.text = healthbar.getPlayerHealth().ToString();
        HpBig.text = healthbar.getPlayerHealth().ToString();
        potamount = 0;
        cdUI = FindObjectOfType<CooldownUI>();
    }
    private void FixedUpdate()
    {
        Vector3 movement = transform.position + new Vector3(horizontalInput, verticalInput, 0) * speed * Time.deltaTime;
        transform.position = movement;
    }
    void Update()
    {
       // log.Add(new Chronobreak(Time.time, transform.position));
        if (newhp <= 0 && !isDead)
        {
            setDead();
        }
        Hp.text = newhp.ToString();
        HpBig.text = Mathf.Max(newhp, 0).ToString(); // sorgt dafür, dass hp in der healthbar nie unter 0 wird
        if (PauseManager.Instance.gameFreezed)
            return;
        //if(Enemy.allCleared == true)
        //    return;     
        if (!isDead)
        {

            horizontalInput = Input.GetAxis("Horizontal");
            verticalInput = Input.GetAxis("Vertical");
          
            bp.transform.localRotation = Quaternion.Euler(0, 0, angle);
            bool isWalking = horizontalInput != 0 || verticalInput != 0;
             playerSprite.setWalkingAnimation(isWalking);
            if (Input.GetKeyDown(KeyCode.G))
            {
                //GodMode
                godmode = true;
                gm.gameObject.SetActive(true);
                Bullets.mindamage = 1000;
                Bullets.maxdamage = 1001;
                Bullets.mindamageSpell = 2000;
                Bullets.maxdamageSpell = 2001;
                speed = 20;
                damageFromEnemy = 0;
                Bullets.accuracy = 100;
                Bullets.accuracySpell = 100;
                wp.fireCooldown = 0.1f;
            }
            if (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.JoystickButton3))
            { //SpellShield
                if (onCooldown == false)
                {
                    cdUI.spellshieldCooldownImage.gameObject.SetActive(true);
                    cdUI.ResetCooldown("spellshield");
                    StartCoroutine(Cooldown());
                    spellshield.gameObject.SetActive(true);
                    shield = true;
                    StartCoroutine(setSpellshieldTimer());
                }
            }
            //Controller rechter Stick auslesen (XY)
            float rightStickX = Input.GetAxis("RightStickHorizontal");
            float rightStickY = Input.GetAxis("RightStickVertical");

            Vector2 rightStickDir = new Vector2(rightStickX, rightStickY);

            if (rightStickDir.magnitude > 0.1f ) // Wenn Stick bewegt wird
            {
                //Angle mit Stick - Richtung berechnen
                angle = Mathf.Atan2(rightStickY, rightStickX) * Mathf.Rad2Deg - 90f;
            }
            else 
            {
                // Sonst Maus verwenden wie bisher
                mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                angle = Mathf.Atan2(mousePos.y - transform.position.y, mousePos.x - transform.position.x) * Mathf.Rad2Deg - 90f;
            }
            // Heiltrank benutzen (Taste H)
            if (Input.GetKeyDown(KeyCode.H) || Input.GetKeyDown(KeyCode.JoystickButton0))
            {
                if (potamount > 0 && newhp < maxhp)
                {
                    newhp += healamount;
                    if (newhp > maxhp) newhp = maxhp;

                    potamount--;
                    healthbar.setPlayerHealth(newhp);
                    Hp.text = newhp.ToString();
                    HpBig.text = newhp.ToString();
                }
            }
            // Dash (Leertaste oder Controller)
            if ((Input.GetKeyDown(KeyCode.Joystick1Button1) || Input.GetKeyDown(KeyCode.Space)) && !dashOnCooldown)
            {
                cdUI.dashCooldownImage.gameObject.SetActive(true);
                cdUI.ResetCooldown("dash");
                StartCoroutine(Dash());
                isCombo = true;
                StartCoroutine(Combo());
                dashOnCooldown = true;
                StartCoroutine(Delay());
            }
        }
        else
        {
            horizontalInput = 0;
            verticalInput = 0;
        }
    }
    IEnumerator Delay()
    {
        yield return new WaitForSeconds(dashCooldown);
        dashOnCooldown = false;
    }
    IEnumerator Combo()
    {
        //combo zeitraum
        yield return new WaitForSeconds(0.2f);
        isCombo = false;
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
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("HealPot"))
        {
            increasepot();
        }
        if (collision.gameObject.CompareTag("AttackSpeedBuff"))
        {
            s.speed += 1;
        }

        if (collision.gameObject.CompareTag("FireBall"))
        {          
                damageInc(10);          
        }
    }
    public void increasepot()
    {
        potamount++;
    }
    public bool getDead()
    {
        return isDead;
    }
    IEnumerator setSpellshieldTimer()
    {
        yield return new WaitForSeconds(2f);
        shield = false;
        spellshield.gameObject.SetActive(false);
    }
    IEnumerator Cooldown()
    {
        onCooldown = true;
        yield return new WaitForSeconds(spellShieldCooldown);
        onCooldown = false;
    }
    public void damageInc(int damage)  // wenn spieler schaden nimmt
    {
        if (shield == false && godmode == false && !getDead())
        {
            playerSprite.setGotHitAnimation();
            newhp -= damage;
            healthbar.setPlayerMaxHealth(maxhp);
            healthbar.setPlayerHealth(newhp);
            Hp.text = newhp.ToString();
            HpBig.text = newhp.ToString();
        }
    }
    private void setDead()
    {
        Hp.gameObject.SetActive(false);
        healthbar.gameObject.SetActive(false);
        isDead = true;
        playerSprite.setDeadAnimation();
    }
}
