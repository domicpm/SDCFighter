using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatePlayerSprite : MonoBehaviour
{
    public Transform sprite;
    Animator animator;
    public PlayerMovement player;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void rotate(bool right)
    {
        if (player.isDead == false && PauseManager.Instance.IsPaused == false)
        {
            if (right)
            {
                sprite.localScale = new Vector3(1, 1, 1); // Rechts
            }
            else
            {
                sprite.localScale = new Vector3(-1, 1, 1); // Links

            }
        }
    }
    public void setGotHitAnimation()
    {
        animator.SetTrigger("GotHit");
    }
    public void setDeadAnimation()
    {
        animator.SetTrigger("IsDead");
    }
}
