using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamageText : MonoBehaviour
{
    [SerializeField] private TextMeshPro damageText;
    public Enemy enemy;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.up * Time.deltaTime; // Schwebt nach oben
    }
    public void updateDamage(float dmg)
    {
        damageText.text = dmg.ToString();
    }
    public void deleteDmg()
    {
        Destroy(gameObject, 2f);
    }
    public void spawnDmg()
    {
        Vector3 pos = enemy.getCurrentEnemyPos();
        Vector3 posUpdate = new Vector3(6.16f, 2.1f, -1.6f);
        pos += posUpdate;
        Instantiate(gameObject, pos , Quaternion.identity);
    }
   
}
