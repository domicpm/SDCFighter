using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class newGame : MonoBehaviour
{
    public Button myButton;

    // Start is called before the first frame update
    void Start()
    {
        myButton.onClick.AddListener(OnButtonClick);
        Debug.Log("Aktive Szene: " + SceneManager.GetActiveScene().name);

        for (int i = 0; i < SceneManager.sceneCount; i++)
        {
            Debug.Log("Geladene Szene: " + SceneManager.GetSceneAt(i).name);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown)
        {
            SceneManager.LoadScene("SampleScene");
            //Application.Quit();
        }
    }
    void OnButtonClick()
    {
        Debug.Log("Button Clicked!");
        SceneManager.LoadScene("SampleScene");
    }
}
