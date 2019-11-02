using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audiomanager : MonoBehaviour {

    private static Audiomanager _instance;
    public static Audiomanager Instance
    {
        get
        {
            return _instance;
        }
    }
    public AudioSource AudioSource;
    public AudioClip seaWaveClip;//海浪声音
    public AudioClip goldClip;//金币落袋声音
    public AudioClip rewardClip;//奖励声音
    public AudioClip fireClip;//开火的声音
    public AudioClip changeFireClip;//换枪声音
    public AudioClip lvUpClip;//升级声音
    private bool isMute=false;//判断游戏是否静音
    public bool IsMute
    {
        get
        {
            return isMute;
        }
    }
    private void Awake()
    {
        _instance = this;
        isMute = (PlayerPrefs.GetInt("mute",0) ==0?false:true);
        DoMute();
    }

    public  void SwitchSoundState(bool isOn)//声音状态切换
    {
        isMute = !isOn;
        DoMute();
    }
    void DoMute()
    {
        if (isMute)//如果静音，关闭所有声音
        {
            AudioSource.Pause();
        }
        else
        {
            AudioSource.Play();//否则播放声音
        }
    }

    public void PlayEffectSound(AudioClip ac)
    {
        if (!isMute)//如果不静音
        {
            AudioSource.PlayClipAtPoint(ac, new Vector3(0,0,-5));
        }
    }
}
