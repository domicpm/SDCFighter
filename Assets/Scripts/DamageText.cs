using TMPro;
using UnityEngine;

public class DamageText : MonoBehaviour
{
    [SerializeField] private TextMeshPro damageText;

    void Start()
    {
        Destroy(gameObject, 0.5f);
    }

    void Update()
    {
        transform.position += Vector3.up * 2f * Time.deltaTime;
    }

    public void SetDamage(float dmg)
    {
        damageText.text = dmg.ToString();

        // Style-Regeln direkt hier:
        if (dmg > 80) // Beispiel: starker Schaden
        {
            damageText.fontSize = 10f;
            damageText.fontStyle = FontStyles.Bold | FontStyles.Italic;
            damageText.color = Color.red;
        }
        else
        {
            damageText.fontSize = 6f;
            damageText.fontStyle = FontStyles.Normal;
        }
    }
}
