using UnityEngine;

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
    public Vector2 randomPosition;
    private void Start()
    {
        healthbar.setMaxHealth(maxhp);

    }
    public void destroyObj()
    {
        spawn();
        Destroy(gameObject);
       
    }
   
    public Vector3 getCurrentEnemyPos()
    {
        return transform.position;
    }
    public void Update()
    {
        
    }
    public void spawn()
    {
        
        
            maxhp = 1000;

             randomPosition = new Vector3(Random.Range(-9f, 9f), Random.Range(-4f, 4f));
            Instantiate(gameObject, randomPosition, Quaternion.identity);
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            isHit = true;
            maxhp = maxhp - bullet.getDmg();
            healthbar.setHealth(maxhp);
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
