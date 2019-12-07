using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instance = null;
    GameObject mPauseMenu;
    PlayerManager mPlayerManager;
    RectTransform HealthBar;
    Text HealthBarText;

    float CurrentTime;
    float HelperTime;
    public Image Ability1;
    public Image Ability2;
    public Image Ability3;

    public Button StartButton;
    public Button ExitButton;
    RectTransform StartButtonPosition;
    RectTransform ExitButtonPosition;

    GameObject StartMenu;
    public GameObject Over;
    GameObject InGameUI;
    GameObject Player;
    void Start()
    {
        HealthBar = GameObject.Find("HP").GetComponent<RectTransform>();
        HealthBarText = GameObject.Find("HP Text").GetComponent<Text>();
        mPauseMenu = GameObject.Find("Pannel").transform.Find("PauseMenu").gameObject;
        mPlayerManager = GameObject.Find("NewBorn").GetComponent<PlayerManager>();
        StartMenu = GameObject.Find("Pannel").transform.Find("StartMenu").gameObject;
        InGameUI = GameObject.Find("InGameUI");
        Player = GameObject.Find("NewBorn");
        Over = GameObject.Find("GameOverScreen");

        StartButtonPosition = StartButton.GetComponent<RectTransform>();
        StartButtonPosition.anchoredPosition = new Vector3(0, -325, 0);
        ExitButtonPosition = ExitButton.GetComponent<RectTransform>();
        ExitButtonPosition.anchoredPosition = new Vector3(0, -390, 0);
        InGameUI.SetActive(false);
        Player.SetActive(false);
        Over.SetActive(false);

    }
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }
    // Start is called before the first frame update

    void CallPauseMenu()
    {
        if(StartMenu.activeSelf==false)
        {
            if (Input.GetKeyDown(KeyCode.F1))
            {
                if (!mPauseMenu.activeSelf)
                {
                    InGameUI.SetActive(false);
                    mPauseMenu.SetActive(true);
                    Time.timeScale = 0;
                }
                else
                {
                    Resume();
                }
            }
        }
       
        
    }

    public void StartGame()
    {
        InGameUI.SetActive(true);
        StartMenu.SetActive(false);
        GameEventManager.instance.PlayerSpawn();
    }
    public void Restart()
    {
        Time.timeScale = 1;
        Over.SetActive(false);
        //SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
        GameEventManager.instance.restart = true;
        SoundManager.instance.PlayNormal();
        GameEventManager.instance.PlayerSpawn();
    }
    public void Resume()
    {
        mPauseMenu.SetActive(false);
        InGameUI.SetActive(true);
        Time.timeScale = 1;
    }
    public void Quit()
    {
        Application.Quit();

    }

    public void UpdateAbilityCD(Image CDIMage, float CD, ref bool condition)
    {
        if (HelperTime != CD)
        {
            HelperTime = CD;
        }
        if (CDIMage.fillAmount == 1)
        {
            CDIMage.fillAmount = 0;
        }

        CDIMage.fillAmount += 1.0f / (CD + (HelperTime - CD)) * Time.deltaTime;

        if (CDIMage.fillAmount >= 1)
        {
            CDIMage.fillAmount = 1;
            HelperTime = -1;
            condition = true;
        }
    }

    IEnumerator Move(RectTransform Button, Vector3 sPosition, Vector3 ePosition, float speed)
    {
        CurrentTime += Time.time;
        while (CurrentTime < speed)
        {
            Button.anchoredPosition = Vector3.Lerp(sPosition, ePosition, CurrentTime / speed);
            yield return null;
        }
        Button.anchoredPosition = ePosition;
    }
    // Update is called once per frame
    void Update()
    {
        if(mPlayerManager == null)
        {
            mPlayerManager = GameObject.Find("NewBorn").GetComponent<PlayerManager>();
        }
        HealthBarText.text = mPlayerManager.CurrentHealth + "/" + mPlayerManager.MaxHealth;
        HealthBar.sizeDelta = new Vector2(mPlayerManager.CurrentHealth * 3, HealthBar.sizeDelta.y);
        CallPauseMenu();

        StartCoroutine(Move(StartButton.GetComponent<RectTransform>(), StartButtonPosition.anchoredPosition, new Vector3(0, -35, 0), 200));
        StartCoroutine(Move(ExitButton.GetComponent<RectTransform>(), ExitButtonPosition.anchoredPosition, new Vector3(0, -185, 0), 200));
    }
}
