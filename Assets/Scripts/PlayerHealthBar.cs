using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBar : MonoBehaviour
{
    public Slider sli;
    private float safe;

    public void setPlayerMaxHealth(float health)
    {
        sli.maxValue = health;
        sli.value = health;
        safe = health;
    }

    public void setPlayerHealth(float health)
    {
        sli.value = health;
    }
    public float getPlayerHealth()
    {
        return sli.value;
    }
}
