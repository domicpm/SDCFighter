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

    public DamageText damageTextPrefab; // Prefab im Inspector zuweisen

    private void Start()
    {
        healthbar.setMaxHealth(maxhp);
        hpEnemy.text = maxhp.ToString();
    }

    public void destroyObj()
    {
        spawn();
        Destroy(gameObject);
    }

    public Vector3 getCurrentEnemyPos()
    {
        return randomPosition;
    }

    public void Update()
    {
    }

    public void spawn()
    {
        maxhp = 1000;
        randomPosition = new Vector3(Random.Range(-38f, 30f), Random.Range(-10f, 30f));
        Instantiate(gameObject, randomPosition, Quaternion.identity);
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

            Debug.Log("Damage: " + bullet.getDmg());

            if (maxhp <= 0)
            {
                enemydeathpos = transform.position;
                int random = Random.Range(1, 101);
                if (random <= 50) heal.spawn(enemydeathpos);
                else ab.spawn(enemydeathpos);

                destroyObj();
            }
        }
    }

}
