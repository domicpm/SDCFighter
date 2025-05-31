using UnityEngine;
using UnityEngine.UI;

public class PauseManager : MonoBehaviour
{
    public static PauseManager Instance { get; private set; }
    public Button continueGameButton;
    public Text pauseText;

    public bool IsPaused { get; private set; } = false;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    void Start()
    {
        pauseText.gameObject.SetActive(false);
        continueGameButton.gameObject.SetActive(false);
        continueGameButton.onClick.AddListener(OnContinueButtonClicked);
    }
    

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    public void OnContinueButtonClicked()
    {
        Resume();
        pauseText.gameObject.SetActive(false);
        continueGameButton.gameObject.SetActive(false);
    }

    public void TogglePause()
    {
        if (IsPaused)
        {
            Resume();
            pauseText.gameObject.SetActive(false);
            continueGameButton.gameObject.SetActive(false);
        }
        else
        {
            Pause();
            pauseText.gameObject.SetActive(true);
            continueGameButton.gameObject.SetActive(true);
        }
    }

    public void Pause()
    {
        Time.timeScale = 0f;
        IsPaused = true;
    }

    public void Resume()
    {
        Time.timeScale = 1f;
        IsPaused = false;
    }
}
