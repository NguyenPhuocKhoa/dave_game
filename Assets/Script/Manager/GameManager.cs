using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Assign singleton vao 1 object.
    private static GameManager _instance;
    public AudioSource _audio;
    public string _txtAnCup;
    private bool _isTouchCup;
    private bool _isHaveGun;
    private bool _isHaveFly;
    public bool IsTouchCup { get => _isTouchCup; set => _isTouchCup = value; }
    public bool IsHaveGun { get => _isHaveGun; set => _isHaveGun = value; }
    public bool isHaveFly { get => _isHaveFly; set => _isHaveFly = value; }

    public static GameManager Instance
    {
        get
        {
            // Lazy (tu sinh ra khi no duoc su dung)
            if (_instance == null)
            {
                _instance = FindObjectOfType<GameManager>();
                if (_instance == null)
                {
                    GameObject gameObj = new GameObject();
                    gameObj.name = "GameManager";
                    _instance = gameObj.AddComponent<GameManager>();
                }
            }
            return _instance;
        }
    }

    // Dung de tao ra cai manager.
    private void Awake()
    {
        if (_instance == false)
        {
            _instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this);
    }

    void Start()
    {
        AudioListener.pause = PlayerPrefs.GetInt("soundOnOff") == 0 ? true : false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!IsTouchCup)
        {
            _txtAnCup = string.Empty;
            
        }

        if (IsTouchCup)
        {
            GameObject[] objList = SceneManager.GetActiveScene().GetRootGameObjects();
            foreach (GameObject go in objList)
            {
                if (go.name == "Door")
                {
                    go.SetActive(true);
                    IsTouchCup = false;
                    break;
                }
            }
            _txtAnCup = "GO TO THE DOOR";
        }
    }

    public void PlayOneShot(AudioClip _clip)
    {
        _audio.clip = _clip;
        _audio.PlayOneShot(_audio.clip);
    }

    public string GetNextLevel()
    {
        string strLevel = Constants._curLevelStr.Substring(Constants._curLevelStr.Length - 1, 1);
        switch(strLevel)
        {
            case "1": return Constants.Level2;
            case "2": return Constants.Level3;
            case "3": return Constants.Level4;
            case "4": return Constants.Level5;
            case "5": return Constants.EndGame;
            case "6": return Constants.Level7;
            case "7": return Constants.Level8;
            case "8": return Constants.Level9;
            case "9": return Constants.Level10;
            default: return string.Empty;
        }
    }


}
