using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    public Transform player;
    public int ppos = 175;
    [SerializeField]private float moveSpeed = 12f;
    private float angle;
    Vector3 direction;
    public GameObject Prefab;
    bool hasLaunched = false;
    float timer = 0f;
    public float interval = 0.5f;
    public float intervalMelee = 0.2f;
    public EnemyAttackSpawn enemySpawn;
    public EnemyAttackSpawn enemySpawn2;
    public EnemyAttackSpawn enemySpawn3;

    private Vector3 originalScale;
    public PlayerMovement p;
    private bool isOriginal = true;
    private bool archerDuplicate; // cooldown auf duplizieren von arrows
    public Animator animator;
    public AttackRangeCircle arc;
    public Enemy boss;
    public RotateEnemySprite res;
    public bool isGolem = false;
    public bool isAttacking = false;
    public bool insideArc = true;
    private void Start()
    {
        originalScale = transform.localScale;
        animator = GetComponent<Animator>();
        gameObject.SetActive(true);
        archerDuplicate = true;
        // Only the clones should move, not the original prefab
        if (!isOriginal)
        {
            CalculateInitialDirection();
            Launch();
        }
    }

    void CalculateInitialDirection()
    {
        // Calculate direction only once when created
        if (player != null)
        {
            direction = player.position - transform.position;
            angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.localRotation = Quaternion.Euler(0, 0, angle);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(isGolem && !insideArc)
        {
            Destroy(gameObject);
        }
        if (p.getDead() == false)
        {
            // Original prefab spawns new Fireballs
            if (isOriginal)
            {
                timer += Time.deltaTime;
                if (timer >= interval && p != null && !p.getDead() && isGolem == false)
                {
                    spawn();
                    timer = 0f;
                }
                else if (timer >= intervalMelee && p != null && !p.getDead() && isGolem == true)
                {
                    spawn();
                    timer = 0f;
                }
            }
            // Clones move in their set direction
            else if (hasLaunched)
            {
                transform.position += transform.right * moveSpeed * Time.deltaTime;
            }
        }
        else
        {
          //  gameObject.SetActive(false);
        }
        if (LevelSuccess.levelDoneText)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isOriginal && (collision.gameObject.CompareTag("Wall") || collision.gameObject.CompareTag("Player")))
        {
            Destroy(gameObject);
        }
    }

    public void spawn()
    {
        if (p != null && p.getDead() == false && enemySpawn != null && arc.getInRange() == true && boss.isDead == false)         
        {
            this.insideArc = true;
            float spread = 30f; // Grad-Abweichung
            Quaternion leftSpread = Quaternion.Euler(0, 0, -spread);
            Quaternion rightSpread = Quaternion.Euler(0, 0, spread);

            CreateFireBall(enemySpawn.gameObject.transform.position, Quaternion.identity); // Mitte
            
            isAttacking = true;
            res.setCastAnimation();
            if (boss.isArcher && archerDuplicate || boss.isBoss)
            {
                StartCoroutine(ArrowDelay());
                CreateFireBall(enemySpawn2.gameObject.transform.position, leftSpread);          // Links
                CreateFireBall(enemySpawn3.gameObject.transform.position, rightSpread);         // Rechts
                archerDuplicate = false;
            }
        }
    }
    public void CreateFireBall(Vector3 spawnpos, Quaternion rotation)
    {
        if (p.getDead() || LevelSuccess.levelDoneText)
        {
            Destroy(gameObject);
            return;
        }

        GameObject newFireball = Instantiate(Prefab, spawnpos, rotation);
        newFireball.transform.localScale *= boss.fireballSizeMultiplier;

        Fireball newFireballScript = newFireball.GetComponent<Fireball>();
        if (newFireballScript != null)
        {
            newFireballScript.isOriginal = false;
            newFireballScript.player = player;
            newFireballScript.p = p;
            newFireballScript.interval = boss.fireballInterval;
            newFireballScript.moveSpeed = boss.fireballSpeed;

            // Basisrichtung (Richtung Spieler)
            Vector3 baseDir = (player.position - newFireball.transform.position).normalized;

            // Rotation (Spread) auf die Basisrichtung anwenden
            Vector3 spreadDir = rotation * baseDir;

            // Rigidbody für Bewegung
            Rigidbody2D rb = newFireball.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.velocity = spreadDir * newFireballScript.moveSpeed;
                rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
            }
        }
    }


    IEnumerator ArrowDelay()
    {
        yield return new WaitForSeconds(5f);
        archerDuplicate = true;
    }



    public void Launch()
    {
        hasLaunched = true;
    }   
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("arc"))
        {
            insideArc = false;
        }
    }

}