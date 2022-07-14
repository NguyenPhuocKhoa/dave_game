using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CanvasController : MonoBehaviour
{
    // Top information.
    [SerializeField] private Image _heart1;
    [SerializeField] private Image _heart2;
    [SerializeField] private Image _heart3;
    [SerializeField] private Image _heart4;
    [SerializeField] private Image _heart5;
    [SerializeField] private Image _heart6;
    [SerializeField] private Image _heart7;
    [SerializeField] private Image _heart8;
    [SerializeField] private Image _heart9;
    [SerializeField] private Image _heart10;
    [SerializeField] private TMP_Text _lblCurentScore;
    [SerializeField] private TMP_Text _lblCurrentLevel;
    [SerializeField] private GameObject _cup1;
    [SerializeField] private GameObject _cup2;

    // Replay panel.
    [SerializeField] private RectTransform _pnlReplay;
    [SerializeField] private RectTransform _pnlBGGameOver;
    [SerializeField] private TMP_Text _txtLevel;
    [SerializeField] private TMP_Text _txtScore;
    [SerializeField] private TMP_Text _lblGun;
    [SerializeField] private Image _gunImage;
    [SerializeField] private TMP_Text _lblFly;
    [SerializeField] private Slider _flyImage;
    [SerializeField] private Image _flyBg;


    // Setting panel.
    [SerializeField] private RectTransform _pnlSetting;
    [SerializeField] private AudioSetting _bgmAudioSetting;
    [SerializeField] private AudioSetting _playerSfxAudioSetting;
    [SerializeField] private AudioSetting _diamondCupAudioSetting;
    [SerializeField] private Toggle _soundOnOff;

    // Bottom information.
    public TMP_Text _txtAnCup;

    void Awake()
    {
        if (SceneManager.GetActiveScene().buildIndex != 1)
        {
            Constants._curLevelStr = SceneManager.GetActiveScene().name;
            this.SetLives(Constants._curLivesInt);
            this.SetScore(Constants._curScoresInt);
            this.SetLevel(Constants._curLevelStr);
            _lblGun.enabled = false;
            _gunImage.enabled = false;
            _lblFly.enabled = false;
            _flyImage.enabled = false;
            _flyBg.enabled = false;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        _bgmAudioSetting.Setup(PlayerPrefs.GetFloat(_bgmAudioSetting.ParamName));
        _playerSfxAudioSetting.Setup(PlayerPrefs.GetFloat(_playerSfxAudioSetting.ParamName));
        _diamondCupAudioSetting.Setup(PlayerPrefs.GetFloat(_diamondCupAudioSetting.ParamName));
    }

    void Update()
    {
        if (!string.IsNullOrEmpty(GameManager.Instance._txtAnCup))
        {
            _txtAnCup.SetText(GameManager.Instance._txtAnCup);
            _cup1.SetActive(!string.IsNullOrEmpty(GameManager.Instance._txtAnCup));
            _cup2.SetActive(!string.IsNullOrEmpty(GameManager.Instance._txtAnCup));
        }

        if (Constants._isAnKimCuong)
        {
            SetScore(Constants._curScoresInt);
            Constants._isAnKimCuong = false;
        }

        if (GameManager.Instance.IsHaveGun)
        {
            _lblGun.enabled = true;
            _gunImage.enabled = true;
            GameManager.Instance.IsHaveGun = false;
        }

        if (GameManager.Instance.isHaveFly)
        {
            _lblFly.enabled = true;
            _flyImage.enabled = true;
            _flyBg.enabled = true;
            GameManager.Instance.isHaveFly = false;
        }
    }

    public void SetLives(int live)
    {
        _heart1.enabled = live >= 1;
        _heart2.enabled = live >= 2;
        _heart3.enabled = live >= 3;
        _heart4.enabled = live >= 4;
        _heart5.enabled = live >= 5;
        _heart6.enabled = live >= 6;
        _heart7.enabled = live >= 7;
        _heart8.enabled = live >= 8;
        _heart9.enabled = live >= 9;
        _heart10.enabled = live >= 10;
    }

    public void SetScore(int score)
    {
        _lblCurentScore.text = $"SCORE:{score}";
    }

    public void SetLevel(string level)
    {
        _lblCurrentLevel.text = $"LEVEL:{level.Substring(level.Length-1, 1).PadLeft(2,'0')}";
    }

    public void ShowReplayUI()
    {
        string level = Constants._curLevelStr;
        _txtLevel.text = level.Substring(level.Length - 1, 1).PadLeft(2, '0');
        _txtScore.text = Constants._curScoresInt.ToString();
        ResetAll();
        _pnlReplay.gameObject.SetActive(true);
        _pnlBGGameOver.gameObject.SetActive(true);
    }

    public void ResetAll()
    {
        // Reset live, score, level.
        Constants._curLevelStr = Constants.Level1;
        Constants._curScoresInt = 0;
        Constants._curLivesInt = Constants.PlayerLives;
        Constants._currentTotalScore = 0;
    }

    public void OnBtnReplayClicked()
    {
        ResetAll();
        Time.timeScale = 1f;
        SceneManager.LoadScene(Constants._curLevelStr);
    }

    public void OnBtnReplayCurrentScenceClicked()
    {
        if (!_pnlBGGameOver.gameObject.activeSelf)
        {
            // Reload.
            Constants._curLevelStr = SceneManager.GetActiveScene().name;
            Constants._curScoresInt = Constants._currentTotalScore;

            Time.timeScale = 1f;
            SceneManager.LoadScene(Constants._curLevelStr);
        }
    }

    public void OnBtnExitClicked()
    {
        Application.Quit();
        //bool decision = EditorUtility.DisplayDialog(
        //     "Exit Game", // title
        //     "Are you sure want to exit the game?", // description
        //     "Yes", // OK button
        //     "No" // Cancel button
        //   );

        //if (decision)
        //{
        //    Application.Quit();
        //}
    }

    public void OnBtnSettingClicked()
    {
        if (!_pnlBGGameOver.gameObject.activeSelf)
        {
            Time.timeScale = 0f;
            _pnlSetting.gameObject.SetActive(true);
            _pnlBGGameOver.gameObject.SetActive(true);
        }
    }

    public void OnBtnExitSettingClicked()
    {
        Time.timeScale = 1f;
        _pnlSetting.gameObject.SetActive(false);
        _pnlBGGameOver.gameObject.SetActive(false);
    }

    public void OnToggleClick()
    {
        if(_soundOnOff.isOn)
        {
            PlayerPrefs.SetInt("soundOnOff", 1);
            AudioListener.pause = false;
        }
        else
        {
            PlayerPrefs.SetInt("soundOnOff", 0);
            AudioListener.pause = true;
        }
    }
}
