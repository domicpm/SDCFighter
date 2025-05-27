using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class loadingscreen : MonoBehaviour
{
    public TMP_Text myText;
    private float blinkOnDuration = 1.1f;  // Wie lange der Text sichtbar ist
    private float blinkOffDuration = 0.5f; // Wie lange der Text unsichtbar ist

    void Start()
    {
        StartCoroutine(BlinkText());
    }
     void Update()
    {
        if (Input.anyKeyDown)
        {
            SceneManager.LoadScene("SampleScene");
            //Application.Quit();
        }
    }
    IEnumerator BlinkText()
    {
        while (true)  // Endlosschleife für dauerhaftes Blinken
        {
            myText.text = "press any button to continue..";
            yield return new WaitForSeconds(blinkOnDuration);

            myText.text = "";
            yield return new WaitForSeconds(blinkOffDuration);
        }
    }
}
