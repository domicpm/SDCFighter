using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public float maxhp;
    public HealthBar healthbar;
    public Bullets bullet;
    public bool isHit = false;
    public Heal heal;
    public Vector2 enemydeathpos;
    public fireball fb;
    public AttackBoost ab;
    public PlayerMovement p;
    public Vector3 randomPosition;
    public bool isSpawned = false;
    public Text hpEnemy;
    public Transform dmgtextSpawnLocation;
    public TextMesh damageText;
    private int maxEnemies = 6;
    public static int enemyCount = 0;
    public AttackRangeCircle atc;
    public DamageText damageTextPrefab; // Prefab im Inspector zuweisen

    private void Start()
    {
        healthbar.setMaxHealth(maxhp);
        hpEnemy.text = maxhp.ToString();
    }

    public void destroyObj()
    {
        Destroy(gameObject);
    }

    public Vector3 getCurrentEnemyPos()
    {
        return randomPosition;
    }

    void Update()
    {


        if (atc.inRange == false)
        {
            Vector2 dir = (p.transform.position - transform.position).normalized;
            float speed = 5f;

            transform.position += (Vector3)(dir * speed * Time.deltaTime);
        }
    }


    public void spawn()
    {
        maxhp = 1000;
        randomPosition = new Vector3(Random.Range(-36f, 30f), Random.Range(-10f, 30f));
        Instantiate(gameObject, randomPosition, Quaternion.identity);
        enemyCount++;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            isHit = true;
            maxhp -= bullet.getDmg();
            healthbar.setHealth(maxhp);
            hpEnemy.text = maxhp.ToString();

            Vector3 offset = new Vector3(Random.Range(-0.5f, 0.5f), 1.5f, 0);
            Vector3 spawnPos = dmgtextSpawnLocation.transform.position + offset; 

             DamageText dmgText = Instantiate(damageTextPrefab, spawnPos, Quaternion.identity);
           
            dmgText.SetDamage(bullet.getDmg());


            if (maxhp <= 0)
            {
                enemydeathpos = transform.position;
                int random = Random.Range(1, 101);
                if (random <= 50) heal.spawn(enemydeathpos);
                else ab.spawn(enemydeathpos);

                for (int i = 0; i < 3; i++)
                {

                    if (enemyCount <= maxEnemies)
                    {
                        spawn();
                    }
                }
                destroyObj();
            }
        }
        if (collision.gameObject.CompareTag("Spell"))
        {
            isHit = true;
            maxhp -= bullet.getDmgSpell();
            healthbar.setHealth(maxhp);
            hpEnemy.text = maxhp.ToString();

            Vector3 offset = new Vector3(Random.Range(-0.5f, 0.5f), 1.5f, 0);
            Vector3 spawnPos = dmgtextSpawnLocation.transform.position + offset;

            DamageText dmgText = Instantiate(damageTextPrefab, spawnPos, Quaternion.identity);

            dmgText.SetDamage(bullet.getDmgSpell());


            if (maxhp <= 0)
            {
                enemydeathpos = transform.position;
                int random = Random.Range(1, 101);
                if (random <= 50) heal.spawn(enemydeathpos);
                else ab.spawn(enemydeathpos);
                for (int i = 0; i < 3; i++)
                {
                    if (enemyCount <= maxEnemies)
                    {
                        spawn();
                    }
                }
                destroyObj();
            }
        }
    }

}
