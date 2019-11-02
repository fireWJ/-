using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UISetting : MonoBehaviour {

    public GameObject SetttingPlane;
    public Toggle muteToggle;
    private void Start()
    {
        muteToggle.isOn = !Audiomanager.Instance.IsMute;
    }
    public void SwitchMute(bool isOn)
    {
        Audiomanager.Instance.SwitchSoundState(isOn);
    }
    //给按钮写要注册的方法
    public void CloseButtonDown()//关闭按钮的事件
    {
        SetttingPlane.SetActive(false);
    }

    public void SettingButtonDown()// 设置按钮的事件
    {
        SetttingPlane.SetActive(true);
    }

    public void BackButtonDown()//返回按钮的事件
    {
        //保存游戏
        PlayerPrefs.SetInt("gold", GameControl.instance.gold);
        PlayerPrefs.SetInt("lv", GameControl.instance.lv);
        PlayerPrefs.SetFloat("scd", GameControl.instance.smallTimer);
        PlayerPrefs.SetFloat("bcd", GameControl.instance.bigTimer);
        int temp = (Audiomanager.Instance.IsMute == false) ? 0 : 1;
        PlayerPrefs.SetInt("mute", temp);
        SceneManager.LoadScene(0);
    }
}

