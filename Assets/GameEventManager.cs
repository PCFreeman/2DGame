using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameEventManager : MonoBehaviour
{
    public static GameEventManager instance = null;
    BOSSTrigger mBossTrigger;
    GameObject Player;
    ParticleSystem PS;
    public AudioClip Spawn;
    public GameObject SpawnPointOne;
    public GameObject SpawnPointTwo;
    public GameObject Minion;
    GameObject door;
    GameObject Boss;
    int SpawnCout = 0;
    bool trigger = false;
    public bool restart = false;
    bool playerdonce = false;
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

    void Start()
    {
        Player = GameObject.Find("NewBorn");
        PS = GameObject.Find("Particle System").GetComponent<ParticleSystem>();
        SpawnPointOne = GameObject.Find("Final").transform.Find("SpawnSpotOne").gameObject;
        SpawnPointTwo = GameObject.Find("Final").transform.Find("SpawnSpotTwo").gameObject;
        door = GameObject.Find("Final").transform.Find("Door").gameObject;
        Boss = GameObject.Find("FinalBoss");
        mBossTrigger = GameObject.Find("Final").transform.Find("BOSSTrigger").GetComponent<BOSSTrigger>();
        Boss.SetActive(false);
    }

    public void FinalBOSSIntro()
    {
        if(mBossTrigger.isStepedon==true)
        {
            door.GetComponent<Rigidbody2D>().gravityScale = 5;
            Boss.SetActive(true);
            if (playerdonce == false)
            {
            SoundManager.instance.PlayFinalBoss();
            playerdonce = true;
            }
        }
    }
    public void Summon()
    {
        if(SpawnCout<=3)
        {
            GameObject Min = Instantiate(Minion, SpawnPointOne.transform.position, transform.rotation) as GameObject;
            GameObject Min2 = Instantiate(Minion, SpawnPointTwo.transform.position, transform.rotation) as GameObject;
            SpawnCout++;
        }
    }
    public void PlayerSpawn()
    {
        SoundManager.instance.PlaySingle(Spawn);
        Player.transform.position = PS.gameObject.transform.position;
        Player.SetActive(false);
        PS.Play();
        trigger = true;
        mBossTrigger.isStepedon = false;
    }
    // Start is called before the first frame update
  

    // Update is called once per frame
    void Update()
    {
        if(PS ==null)
        {
        PS = GameObject.Find("Particle System").GetComponent<ParticleSystem>();
        }
        if(Player ==null)
        {
            Player = GameObject.Find("NewBorn");
        }
        var sh = PS.shape;
        if (trigger==true)
        {
            sh.radius -= 0.5f * Time.deltaTime;
        }
        if(sh.radius<=0.01)
        {
            trigger = false;
            sh.radius = 1;
            PS.Stop();
            Player.SetActive(true);
            if (restart == true)
            {
                restart = false;
                SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
            }
        }
        if (Boss == null)
        {
            Boss = GameObject.Find("FinalBoss");
        }
       if(door==null)
        {
            door = GameObject.Find("Final").transform.Find("Door").gameObject;
        }
       if(mBossTrigger==null)
        {
            mBossTrigger = GameObject.Find("Final").transform.Find("BOSSTrigger").GetComponent<BOSSTrigger>();
        }
       if (mBossTrigger.isStepedon!=true)
        {
            Boss.SetActive(false);
            playerdonce = false;
        }

        FinalBOSSIntro();
    }
}
