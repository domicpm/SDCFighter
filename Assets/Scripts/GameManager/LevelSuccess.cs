using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelSuccess : MonoBehaviour
{
    public static LevelSuccess Instance;
    public Button continueLevelButton;
    public Button continueLevelButtoninRoom;
    public Button teleportButton;
    public Button shopButton;
    public Text continueLevelText;
    public Text timeLeftText;
    public EnemyManager enemyManager;
    public PlayerMovement player;
    public static Vector2 nextSpawnPosition;
    public static bool isInLootRoom = false;
    public static bool levelDoneText = false;
    private bool isTeleported = false;
    public static float waveTime = 40;
    public float fixedWaveTime = 40;
    public static bool roundStarted = false;
    public npcAzriel azriel;
    public Interact interactRange;
    public AzrielShop shop;
    // Start is called before the first frame update
    void Start()
    {

        if (roundStarted)
            setAct();


        continueLevelText.gameObject.SetActive(false);
        teleportButton.gameObject.SetActive(false);
        continueLevelButton.gameObject.SetActive(false);
        continueLevelButton.onClick.AddListener(OnContinueLevelButtonClicked);
        teleportButton.onClick.AddListener(OnTeleportClicked);
        continueLevelButtoninRoom.onClick.AddListener(OnContinueLevelButtoninRoomClicked);
        azriel.gameObject.SetActive(false);
        shopButton.onClick.AddListener(OnShopButtonClicked);
    }
    private void Awake()
    {
        Instance = this;
        continueLevelButtoninRoom.gameObject.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        if (enemyManager.level == 2 || enemyManager.level == 5 || enemyManager.level == 9 || enemyManager.level == 13)
        {
            timeLeftText.text = "";
        }
        else
        {
            if (!PauseManager.Instance.isPaused && roundStarted)
            {
                if ((waveTime - Time.time) > 0)
                {
                    timeLeftText.text = Mathf.RoundToInt(waveTime - Time.time).ToString();
                }
                else
                {
                    timeLeftText.text = "done";
                }
            }
        }

    }
    public void OnContinueLevelButtonClicked()
    {
        enemyManager.InitializeLevel(enemyManager.level, true);
        continueLevelText.gameObject.SetActive(false);
        teleportButton.gameObject.SetActive(false);
        teleportButton.gameObject.SetActive(false);
        levelDoneText = false;
        Enemy.bossDead = false;
        EnemyManager.bossSpawned = false;
        Enemy.killCount = 0;
        enemyManager.level++;
        waveTime = Time.time + fixedWaveTime;
        roundStarted = true;
        setAct();
    }
    public void OnShopButtonClicked()
    {
        shop.gameObject.SetActive(true);
        azriel.gameObject.SetActive(false);
    }
    public void OnContinueLevelButtoninRoomClicked()
    {
        nextSpawnPosition = new Vector2(-23.53f, -3.23f);
        player.transform.position = nextSpawnPosition;
        continueLevelButtoninRoom.gameObject.SetActive(false);
        azriel.gameObject.SetActive(false);
        OnContinueLevelButtonClicked();
        isInLootRoom = false;
        azriel.gameObject.SetActive(false);
        interactRange.gameObject.SetActive(false);
        Enemy.isDummy = false;
        levelDoneText = false;
        Enemy.bossDead = false;
        EnemyManager.bossSpawned = false;
        Enemy.killCount = 0;
        enemyManager.level++;
        waveTime = Time.time + fixedWaveTime;
        roundStarted = true;
        setAct();
    }

    public void OnTeleportClicked()
    {
        isTeleported = true;
        nextSpawnPosition = new Vector2(82.75f, -86.44f);
        player.transform.position = nextSpawnPosition;
        continueLevelText.gameObject.SetActive(false);
        teleportButton.gameObject.SetActive(false);
        //player.Hp.gameObject.SetActive(false);
        //healthbar.gameObject.SetActive(false);
        teleportButton.gameObject.SetActive(false);
        isInLootRoom = true;
        interactRange.gameObject.SetActive(true);
        Enemy.isDummy = true;
    }


    public void setAct()
    {
        if (player.getDead()) return;
        //if ((Enemy.killCount == enemyManager.maxEnemies + 1 && Enemy.bossDead == true) || (Enemy.killCount == enemyManager.maxEnemies && EnemyManager.bossSpawned == false)) 
        if (enemyManager.level != 2 && enemyManager.level != 5 && enemyManager.level != 9 && enemyManager.level != 13)
        {
            StartCoroutine(WaveTimer());
        }
        else if (Enemy.bossDead)
        {
            levelDoneText = true;
            continueLevelText.gameObject.SetActive(true);
            continueLevelButton.gameObject.SetActive(true);
            teleportButton.gameObject.SetActive(true);
            continueLevelText.text = "Wave " + (enemyManager.level) + " completed!";
        }

    }
    private void OnDisable()
    {
        Debug.Log("LevelSuccess wurde deaktiviert!");
    }

    private void OnEnable()
    {
        Debug.Log("LevelSuccess wurde aktiviert!");
    }

    IEnumerator WaveTimer()
    {
        Debug.Log("Now Started.");
        Debug.Log("WavetimE:" + fixedWaveTime);
        yield return new WaitForSeconds(fixedWaveTime);
        Debug.Log("WavetimE:" + fixedWaveTime);
        roundStarted = false;
        levelDoneText = true;
        continueLevelText.gameObject.SetActive(true);
        continueLevelButton.gameObject.SetActive(true);
        teleportButton.gameObject.SetActive(true);
        continueLevelText.text = "Wave " + (enemyManager.level) + " completed!";
        Debug.Log("Now Finished.");
    }

}