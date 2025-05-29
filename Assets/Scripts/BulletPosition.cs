using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPosition : MonoBehaviour
{
    public RotatePlayerSprite player;
    private bool right = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Camera.main.ScreenToWorldPoint(Input.mousePosition).x;
        if (mouseX <= player.transform.position.x)
        {
            right = false;
            player.rotate(right);
        }
        else
        {        
            right = true;
            player.rotate(right);
        }
    }
}
