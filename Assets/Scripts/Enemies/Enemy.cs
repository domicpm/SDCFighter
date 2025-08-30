using UnityEngine;
using UnityEngine.UI;
using System.Collections;
public class Enemy : MonoBehaviour
{
    public float maxhp;
    public float hp;
    public Transform sprite;
    public HealthBar healthbar;
    public char tierTag;
    public Bullets bullet;
    public  bool isBoss = false;
    public bool isHit = false;
    public Vector2 enemydeathpos;
    public Vector3 targetPos;
    public ItemDrops itemType;
    public ItemDrops item;
    public PlayerMovement p;
    public Vector3 randomPosition;
    public bool isSpawned = false;
    public static bool bossDead = false;
    public Text hpEnemy;
    public Transform dmgtextSpawnLocation;
    public int spawnAfterKill = 1;
    public static int enemyCount = 0;
    public AttackRangeCircle atc;
    public float speed = 7f;
    public DamageText damageTextPrefab;
    public GameObject enemyprefab;
    public Fireball fb;
    public bool isDead = false;
    public RotateEnemySprite res;
    private SpriteRenderer spriteRenderer;
    public Thunder thunder;
    public EnemyTier et;
    [HideInInspector] public int fireballSizeMultiplier = 1;
    [HideInInspector]public float fireballInterval = 0.7f;
    public float fireballSpeed = 8f;
    public static bool allCleared = false;
    public static bool isDummy = false;
    private Vector3 baseScale;
    private static bool bulletSpawned = false;
    public static int killCount = 0;
    private Vector3 personalOffset;
    public bool isGolem = false;
    public bool isArcher = false;
    private bool isDone = false;
    private Color originalColor;
    private void Start()
    {
        healthbar.setMaxHealth(maxhp);
        hpEnemy.text = maxhp.ToString();
        hp = maxhp;
        atc.inRange = false;
        spriteRenderer = sprite.GetComponent<SpriteRenderer>();
        baseScale = sprite.localScale; // Ausgangsskalierung speichern

        personalOffset = Random.insideUnitCircle * 2f;

    }
    public void destroyObj()
    {
        enemyCount++;
        this.isDead = true;
        Collider2D col = GetComponent<Collider2D>();
        if (col != null)
            col.enabled = false;
        healthbar.gameObject.SetActive(false);
        hpEnemy.gameObject.SetActive(false);
        //fb.gameObject.SetActive(false);
        res.setDeadAnimation();
        StartCoroutine(DestroyAfterDelay(2f));
    }

    private IEnumerator DestroyAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }


    public Vector3 getCurrentEnemyPos()
    {
        return randomPosition;
    }

    void Update()
    {
        if (isDead || LevelSuccess.isInLootRoom == true || isDummy == true) return;  // kein Movement, wenn tot oder im Loot Raum oder wenn Dummy aktiv
        if (atc.inRange == false)
        {            
            res.setWalkingAnimation(true);            
            targetPos = p.transform.position + personalOffset;         
            Vector2 dir = (targetPos - transform.position).normalized;
            transform.position += (Vector3)(dir * speed * Time.deltaTime);
        }
        else
        {
            res.setWalkingAnimation(false);
        }
        if(LevelSuccess.levelDoneText == true)
        {
            isDone = true;
            deathHandler();
        }
        FlipSprite();
    }
    void FlipSprite()
    {
        Vector3 scale = baseScale;
        if (transform.position.x > p.transform.position.x)
            scale.x = -Mathf.Abs(scale.x);  // nach links flippen
        else
            scale.x = Mathf.Abs(scale.x);   // nach rechts flippen

        sprite.localScale = scale;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isDead) return;  // keine Collision , wenn tot
        if (collision.CompareTag("Bullet"))
        {
            if (SkillTree.isHeal)
            {
                int randomLifesteal = Random.Range(1, 101);
                if (randomLifesteal <= p.lifesteal && p.newhp < p.maxhp) p.newhp += 1;
                else if (randomLifesteal <= 5 && p.newhp < p.maxhp) p.newhp += 5;
            }
            var bulletComponent = collision.GetComponent<Bullets>();
            if (bulletComponent != null && !bulletComponent.dmgApplied)
            {
                isHit = true;
                bulletComponent.dmgApplied = true;
                hp -= bulletComponent.getDmg();
                healthbar.setHealth(hp);
                hpEnemy.text = hp.ToString();

                Vector3 offset = new Vector3(Random.Range(-0.5f, 0.5f), 1.5f, 0);
                Vector3 spawnPos = dmgtextSpawnLocation.position + offset;

                DamageText dmgText = Instantiate(damageTextPrefab, spawnPos, Quaternion.identity);
                dmgText.SetDamage(bulletComponent.getDmg());

                if (hp <= 0)
                {
                    deathHandler();
                }
            }
        }

        if (collision.gameObject.CompareTag("Spell"))
        {
            UpdateDamageSpell();
            isHit = true;
            hp -= bullet.getDmgSpell();
            healthbar.setHealth(hp);
            hpEnemy.text = hp.ToString();

            Vector3 offset = new Vector3(Random.Range(-0.5f, 0.5f), 1.5f, 0);
            Vector3 spawnPos = dmgtextSpawnLocation.transform.position + offset;

            DamageText dmgText = Instantiate(damageTextPrefab, spawnPos, Quaternion.identity);

            dmgText.SetDamage(bullet.getDmgSpell());


            if (hp <= 0)
            {
                deathHandler();
            }
        }
    }
    void deathHandler()
    {
        
        enemydeathpos = transform.position;
        killCount++;
        Transform shadow = transform.Find("SpriteWraith/Circle");

        if (shadow == null)
                shadow = transform.Find("SpriteEnemy/Circle");

        if (shadow == null)
                shadow = transform.Find("SpriteWraith/MagicCircle");
        if (shadow != null)
        {
                shadow.gameObject.SetActive(false);
        }
        if (!isDone)
        {
            int dropChance = Random.Range(1, 101);
            if (dropChance <= 50 && isBoss)
            {
                item.spawnItemsWithEffects(enemydeathpos);
            }
            else if (isBoss)
            {
                itemType.spawnItems(enemydeathpos, 4);
            }
            if (dropChance <= 5 && !CompareTag("S-Tier-Enemy"))
            {
                itemType.spawnItems(enemydeathpos, 1);
            }
            else if (dropChance <= 15 && !CompareTag("S-Tier-Enemy"))
            {
                itemType.spawnItems(enemydeathpos, 2);
            }
            else if (!CompareTag("S-Tier-Enemy") && dropChance <= 2)
            {
                itemType.spawnItems(enemydeathpos, 3);
                itemType.spawnItems(enemydeathpos, 4);
            }
            if (CompareTag("S-Tier-Enemy") && dropChance >= 50)
            {
                itemType.spawnItems(enemydeathpos, 3);
                p.experience += 50;   // if enemy S Tier, gain additional xp
            }
            else
            {
                p.experience += 50;   // if enemy S Tier, gain additional xp
            }

            if (isBoss)
            {
                bossDead = true;
            }
            else if (EnemyManager.bossSpawned == false)
            {
            }
            if (CompareTag("Boss"))
            {
                p.experience += 50;
                bossDead = true;
                StartCoroutine(Delay());
            }
            else if (!CompareTag("S-Tier-Enemy"))
            {
                if (p.experience <= p.maxExperience)
                    p.experience += 10;
            }
            destroyObj();
        }
        else
        {
            
                destroyObj();
            
        }
    }
    IEnumerator Delay()
    {
        yield return new WaitForSeconds(2f);
        LevelSuccess.roundStarted = false;
        LevelSuccess.Instance.setAct();
        //allCleared = true;
    }
    public void UpdateDamageSpell()
    {
        int random = Random.Range(1, 101);
        if (random <= Bullets.accuracySpell && !Bullets.isComboConfirmed)
        {
            bullet.damageSpell = Mathf.RoundToInt(Random.Range(Bullets.mindamageSpell, Bullets.maxdamageSpell));
        }
        else if(!Bullets.isComboConfirmed)
        {
            bullet.damageSpell = 0;
        }else if(Bullets.isComboConfirmed)
        {
            //wenn combo (dash und gleich danach spell) erfolgreich, wird schaden erhöht
            bullet.damageSpell = Mathf.RoundToInt(Random.Range(Bullets.mindamageSpell * 3f, Bullets.maxdamageSpell * 2f));
        }
    }

}
