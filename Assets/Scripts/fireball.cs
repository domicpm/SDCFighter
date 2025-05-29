using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireball : MonoBehaviour
{
    public Transform player;
    public int ppos = 175;
    [SerializeField] private float moveSpeed = 0.01f;
    private float angle;
    Vector3 direction;
    public GameObject Prefab;
    bool hasLaunched = false;
    float timer = 0f;
    public float interval = 1f;
    public enemyWeapon enemy;
    private Vector3 originalScale;
    public PlayerMovement p;
    private bool isOriginal = true; // Flag to identify the original prefab
    public Animator animator;
    public AttackRangeCircle arc;
    private void Start()
    {
        originalScale = transform.localScale;
        animator = GetComponent<Animator>();
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
     
        if (p.getDead() == false)
        {
            // Original prefab spawns new fireballs
            if (isOriginal)
            {
                timer += Time.deltaTime;
                if (timer >= interval && p != null && !p.getDead())
                {
                    spawn();
                    timer = 0f;
                }
            }
            // Clones move in their set direction
            else if (hasLaunched)
            {
                // WICHTIGE ÄNDERUNG: "transform.right" statt "- transform.right"
                transform.position += transform.right * moveSpeed * Time.deltaTime;
            }
        }
        else
        {
            gameObject.SetActive(false);
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
        if (p != null && p.getDead() == false && enemy != null && arc.getInRange() == true)
            
        {
            GameObject newFireball = Instantiate(Prefab, enemy.gameObject.transform.position, Quaternion.identity);

            // Setze Referenzen für den geklonten Feuerball
            fireball newFireballScript = newFireball.GetComponent<fireball>();

            if (newFireballScript != null)
            {
                newFireballScript.isOriginal = false; // Markiere als Klon
                newFireballScript.player = player;
                newFireballScript.moveSpeed = moveSpeed;
                newFireballScript.p = p;

                // Berechne Richtung zum Spieler
                Vector3 fireballDirection = (player.position - newFireball.transform.position).normalized;

                // Setze die Rotation des Feuerballs so, dass er in die richtige Richtung fliegt
                float fireballAngle = Mathf.Atan2(fireballDirection.y, fireballDirection.x) * Mathf.Rad2Deg;
                newFireball.transform.rotation = Quaternion.Euler(0, 0, fireballAngle);

                newFireball.transform.localScale = new Vector3(-Mathf.Abs(newFireball.transform.localScale.x), newFireball.transform.localScale.y, newFireball.transform.localScale.z);
               


                // Setze den Rigidbody für die Bewegung
                Rigidbody2D rb = newFireball.GetComponent<Rigidbody2D>();
                if (rb != null)
                {
                    rb.velocity = fireballDirection * moveSpeed; // Geschwindigkeit anwenden
                    rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
                }
            }
        }
    }





    public void Launch()
    {
        hasLaunched = true;
    }
}