using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamageText : MonoBehaviour
{
    [SerializeField] private TextMeshPro damageText;
    public Enemy enemy;
    public Vector3 pos;
    [SerializeField] private DamageText damageTextPrefab;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.up * 3f * Time.deltaTime; // Schwebt nach oben
    }
    public void updateDamage(float dmg)
    {
        damageText.text = dmg.ToString();

    }
    public void deleteDmg()
    {
        Destroy(gameObject, 0.5f);

    }
    public void setPosition(Vector3 posi)
    {
        pos = posi;
    }
   
    public void spawnDmg(float damage)
    {
        DamageText dmgText = Instantiate(damageTextPrefab, pos, Quaternion.identity);
        dmgText.updateDamage(damage);
        dmgText.deleteDmg(); // automatisch nach 1 Sekunde zerstören
    }
   
}
