using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instance = null;
    public GameObject PauseMenu;
    public PlayerManager mPlayerManager;
    public RectTransform HealthBar;
    public Text HealthBarText;

    float HelperTime;
    public Image Ability1;
    public Image Ability2;
    public Image Ability3;

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
        if (Input.GetKeyDown(KeyCode.F1))
        {
            if (!PauseMenu.activeSelf)
            {
                PauseMenu.SetActive(true);
                Time.timeScale = 0;
            }
            else
            {
                Resume();
            }
        }
        
    }
    public void Restart()
    {
        SceneManager.LoadScene(1);
    }
    public void Resume()
    {
        PauseMenu.SetActive(false);
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

        CDIMage.fillAmount -= 1.0f / (CD + (HelperTime - CD)) * Time.deltaTime;

        if (CDIMage.fillAmount <= 0)
        {
            CDIMage.fillAmount = 1;
            HelperTime = -1;
            condition = true;
        }
    }
    void Start()
    {
        HealthBar = GameObject.Find("HP").GetComponent<RectTransform>();
        HealthBarText = GameObject.Find("HP Text").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if(mPlayerManager ==null)
        {
            mPlayerManager = GameObject.Find("NewBorn").GetComponent<PlayerManager>();
        }
        HealthBarText.text = mPlayerManager.CurrentHealth + "/" + mPlayerManager.MaxHealth;
        HealthBar.sizeDelta = new Vector2(mPlayerManager.CurrentHealth * 3, HealthBar.sizeDelta.y);
        CallPauseMenu();
    }
}
