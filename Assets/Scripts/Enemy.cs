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
    public DamageText dt;
    public bool isSpawned = false;
    public dmgTextSpawn spawnposition;
    public Text hpEnemy;
    private void Start()
    {
        dt.setPosition(spawnposition.gameObject.transform.position);
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
        Debug.Log("Damage random:" + randomPosition);
        dt.setPosition(randomPosition);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            isHit = true;
            maxhp = maxhp - bullet.getDmg();
            healthbar.setHealth(maxhp);
            hpEnemy.text = maxhp.ToString();
            if (maxhp <= 0)
            {
                
                int random = Random.Range(1, 101);
                if (random <= 50)
                {
                    enemydeathpos = gameObject.transform.position;
                    if (random <= 50){
                        heal.spawn(enemydeathpos);
                    }
                    else
                    {
                        ab.spawn(enemydeathpos);
                        Debug.Log("AtttackSpeed");
                    }

                }
               destroyObj();
              
            }
        }
    }
   
}
