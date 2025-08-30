using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InputDevice : MonoBehaviour
{
    public Button ButtonController;
    public Button ButtonMouse;
    public static bool mouse = false;
    private bool firstTimeCheck = false;
    public static bool isClicked = false;
    // Start is called before the first frame update
    void Start()
    {
        ButtonController.onClick.AddListener(OnButtonClick);
        ButtonMouse.onClick.AddListener(OnButtonClick1);
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void Awake()
    {
    }
    void OnButtonClick()
    {
        mouse = false;
        isClicked = true;
        SceneManager.LoadScene("SampleScene");
        LevelSuccess.waveTime = LevelSuccess.waveTime + Time.time;
        LevelSuccess.roundStarted = true;
        firstTimeCheckfunc();
    }
    void OnButtonClick1()
    {
        mouse = true;
        isClicked = true;
        SceneManager.LoadScene("SampleScene");
        LevelSuccess.waveTime = LevelSuccess.waveTime + Time.time;
        LevelSuccess.roundStarted = true;
        firstTimeCheckfunc();
    }
    void firstTimeCheckfunc()
    {
        if (firstTimeCheck == true)
        {
            EnemyManager.Instance.InitializeLevel(1, true);
        }
        else
        {
            EnemyManager.Instance.InitializeLevel(1, false);
        }
        firstTimeCheck = true;
    }
}