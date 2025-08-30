using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDrops : MonoBehaviour
{
    public GameObject heal;
    public GameObject attackBoost;
    public GameObject chest;
    public GameObject spellBook;
    public PlayerMovement player;
    public GameObject prefabGreen;
    public GameObject prefabBlue;
    public GameObject prefabSpellRed;
    public static int type = 0;
    public float spellCooldown = 0f;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

       
    }
    public void spawnItems(Vector3 enemypos, int type)
    {
        GameObject newHeal;
        switch (type)
        {
            case 1:
                newHeal = Instantiate(attackBoost, enemypos, Quaternion.identity);
                gameObject.transform.position = enemypos;
                break;
            case 2:
                newHeal = Instantiate(heal, enemypos, Quaternion.identity);
                gameObject.transform.position = enemypos;
                break;
            case 3:
                newHeal = Instantiate(chest, enemypos, Quaternion.identity);
                gameObject.transform.position = enemypos;
                break;
            case 4:
                newHeal = Instantiate(spellBook, enemypos, Quaternion.identity);
                gameObject.transform.position = enemypos;
                break;
        }
    }
    public void applyItemWithEffects()
    {
        if (type == 1 && !player.godmode)
        {
            Bullets.accuracy += 2;
            Bullets.mindamage += 220;
            Bullets.maxdamage += 220;
        }
        else if (type == 2 && !player.godmode)
        {
            Bullets.accuracy += 1;
            Bullets.maxdamage += 120;
            Bullets.mindamage += 120;

        }
        else if (type == 3 && !player.godmode)
        {
            spellCooldown = 0.9f;
            Bullets.accuracySpell += 5;
            Bullets.mindamageSpell += 220;
            Bullets.maxdamageSpell += 220;
            PlayerAttackSpawn.fireCooldownSpell *= spellCooldown;
        }
      
    }
    public void spawnItemsWithEffects(Vector3 enemypos)
    {
        int spawnChance = Random.Range(1, 101);
        if (spawnChance < 20)
        {
            GameObject newBullet = Instantiate(prefabGreen, enemypos, Quaternion.identity);
            gameObject.transform.position = enemypos;
            type = 1;
        }
        else if (spawnChance < 60)
        {
            GameObject newBullet = Instantiate(prefabBlue, enemypos, Quaternion.identity);
            gameObject.transform.position = enemypos;
            type = 2;
        }
        else if(spawnChance < 15)
        {
            GameObject newSpell = Instantiate(prefabSpellRed, enemypos, Quaternion.identity);
            gameObject.transform.position = enemypos;
            type = 3;
        }
        else
        {

        }
    }

}
