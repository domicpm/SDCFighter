using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    public Text GameOverTxt;
     public PlayerMovement player;
    public ExitGame exitGameButton;
    public RetryGame retryGameButton;
    // Start is called before the first frame update
    void Start()
    {
        GameOverTxt.gameObject.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        if(player.getDead() == true)
        {
            StartCoroutine(ShowGameOverUI());
        }
        
    }
    IEnumerator ShowGameOverUI()
    {
        yield return new WaitForSeconds(1.5f); // 1 Sekunde warten

        GameOverTxt.gameObject.SetActive(true);
        exitGameButton.gameObject.SetActive(true);
        retryGameButton.gameObject.SetActive(true);
    }

   
}
