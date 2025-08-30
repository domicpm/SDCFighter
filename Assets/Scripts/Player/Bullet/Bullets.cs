using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bullets : MonoBehaviour
{
    [HideInInspector] public static float mindamage = 200;
    [HideInInspector] public static float maxdamage = 400;
    [HideInInspector] public static float mindamageSpell = 350;
    [HideInInspector] public static float maxdamageSpell = 550;
    public static float accuracy = 75;
    public static float accuracySpell = 75;
    public float speed = 4f;
    public bool dmgApplied = false;
    public static bool isComboConfirmed = false;
    public float damage;
    public float damageSpell;
    public ObjectPooling objectPooling;
    //public Sprite newSprite;
    public PlayerMovement player;
    private Vector3 originalScale;
    private CooldownUI cdUI;
    public GameObject b;
    public Spell spell;
    public Items item;
    private void Awake()
    {
        objectPooling = FindObjectOfType<ObjectPooling>();
    }

    private void Start()
    {
        cdUI = FindObjectOfType<CooldownUI>();
        UpdateDamage();
        originalScale = transform.localScale;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            objectPooling.RemoveObject(gameObject);
        }
        if (collision.gameObject.CompareTag("Dummy"))
        {
            objectPooling.RemoveObject(gameObject);
        }

        if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Boss") || collision.gameObject.CompareTag("S-Tier-Enemy"))
        {
            objectPooling.RemoveObject(gameObject);
        }
    }
    public float getDmg()
    {
        return damage;
    }

    public float getDmgSpell()
    {
        return damageSpell;
    }
    public void UpdateDamage()
    {
        int random = Random.Range(1, 101);
        if (random <= accuracy)
        {
            damage = Mathf.RoundToInt(Random.Range(mindamage, maxdamage));
        }
        else
        {
            damage = 0;
        }
    }
    public void shoot()
    {
        if (PauseManager.Instance.gameFreezed || LevelSuccess.isInLootRoom || LevelSuccess.levelDoneText)
            return;

        GameObject bullet = objectPooling.ActivateObject(objectPooling.resourceTypeA, player.bp.transform.position, Quaternion.identity);
        if (bullet == null) return;

        var bulletScript = bullet.GetComponent<Bullets>();
        bulletScript.speed = speed;
        bulletScript.UpdateDamage();  

        var renderer = bullet.GetComponent<SpriteRenderer>();
        bullet.transform.localScale = originalScale;

        if (Items.looted && ItemDrops.type == 1)
        {
            renderer.color = new Color32(0xA2, 0xCF, 0x00, 0xFF);
            bullet.transform.localScale = bullet.transform.localScale * 2f;
        }
        else if (Items.looted && ItemDrops.type == 2)
        {
            renderer.color = new Color32(0x5C, 0x5F, 0x98, 0xFF);
            bullet.transform.localScale = bullet.transform.localScale * 2f;
        }
        {

        }

        Vector3 bulletDir = player.bp.transform.up;
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0f;
        bullet.transform.rotation = player.bp.transform.rotation;
        rb.AddForce(bulletDir * speed, ForceMode2D.Impulse);
    }

    public void shootLeft()
    {
               if (PauseManager.Instance.gameFreezed || LevelSuccess.isInLootRoom == true) // wenn Pause gedrückt, werden keine weiteren Bullets gespawnt
            return;
        
        cdUI.spellCooldownImage.gameObject.SetActive(true);
        cdUI.ResetCooldown("spell");
        //var bullet = Instantiate(spell, player.bp.transform.position, Quaternion.identity);
        GameObject spell = objectPooling.ActivateObject(objectPooling.resourceTypeB, player.bp.transform.position, Quaternion.identity);
        Transform thunder = spell.transform.Find("Thunder");

        if (PlayerMovement.isCombo)
        {
            thunder.gameObject.SetActive(true);
            isComboConfirmed = true;
        }
        else
        {
            thunder.gameObject.SetActive(false);
            isComboConfirmed = false;
        }
        var renderer = spell.GetComponent<SpriteRenderer>();
        if (Items.looted && ItemDrops.type == 3)
        {
            renderer.color = new Color32(0xD1, 0x15, 0x15, 0xFF);
        }

        if (spell == null) return;
        //UpdateDamageSpell();
        Vector3 bulletDir = player.bp.transform.up;
        spell.GetComponent<Rigidbody2D>().AddForce(bulletDir * speed, ForceMode2D.Impulse);
        transform.localScale += new Vector3(0.1f, 0.1f, 0.1f) * Time.deltaTime;
    }
}
