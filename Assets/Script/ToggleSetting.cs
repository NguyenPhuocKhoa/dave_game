using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ToggleSetting : MonoBehaviour
{
    [SerializeField] private Toggle _isSoundOnOff;
    [SerializeField] private RectTransform _pnlSetting;
    // Start is called before the first frame update
    void Start()
    {
        if (_pnlSetting.gameObject.activeSelf)
            _isSoundOnOff.isOn = AudioListener.pause;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            SceneManager.LoadScene("StartScene");
        } 
    
    }
}
