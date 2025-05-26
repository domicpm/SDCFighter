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
    public ContinueGame continueGameButton;
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
            GameOverTxt.gameObject.SetActive(true);
            exitGameButton.gameObject.SetActive(true);
            retryGameButton.gameObject.SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            continueGameButton.gameObject.SetActive(true);
            continueGameButton.myText.gameObject.SetActive(true);
            PauseGame();
        }
    }
    void PauseGame()
    {
        Time.timeScale = 0f;
    }
}
