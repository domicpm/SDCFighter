using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heal : MonoBehaviour
{
    public GameObject Prefab;

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

        if (collision.gameObject.CompareTag("Player"))
        {
             Destroy(gameObject);
        }
    }
    public void spawn(Vector3 enemypos)
    {
        GameObject newHeal = Instantiate(Prefab, enemypos, Quaternion.identity);

        Heal newHealScript = newHeal.GetComponent<Heal>();
        newHealScript.Prefab = Prefab;

        gameObject.transform.position = enemypos;

    }
   
    
}
