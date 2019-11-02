using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GameControl : MonoBehaviour {

    public GameObject[] guns;//存放枪的种类

    private int[] oneShootCost = {5,10,20,30,40,50,60,70,80,90,100,200,300,400,500,600,700,800,900,1000 };//一次射击的花费
    private string[] lvName = { "新手", "入门", "菜鸟", "青铜", "白银", "黄金", "铂金","钻石", "大师", "宗师" };

    private int indexCost=0;//射击花费索引
    public Text costText;// 每次射击花费的文本 
    public GameObject[] bullet1;// 存放所有第一类子弹
    public GameObject[] bullet2;//存放所有第二类子弹
    public GameObject[] bullet3;//存放所有第三类子弹
    public GameObject[] bullet4;//存放所有第四类子弹
    public GameObject[] bullet5;//存放所有第五类子弹
    public Transform bulletHolder;//实例化子弹后的父物体
    public int LV = 0;
    public Text oneShootCostText;//一次射击花费文本
    public Text lvText;//等级文本
    public Text lvNameText;//等级名称文本
    public Text smallCountDownText;//小倒计时文本
    public Text bigCountDownText;//大倒计时文本
    public Text goldText;//金币文本

    public  Color goldColor;// 金币颜色 

    public int lv = 0;
    public int exp = 0;
    public int gold = 500;
    const int bigCountDown = 240;
    const int smallCountDown = 60;
    public float bigTimer= bigCountDown;
    public float smallTimer=smallCountDown;

    public Button bigCountTimeButton;//大倒计时按钮
    public Button backButton;//返回按钮
    public Button settingButton;//设置按钮
    public Slider expSlider;//经验条

    public GameObject LvUpTips;//升级提示
    public GameObject fireEffect;//开火特效
    public GameObject changeFireEffect;//换枪特效
    public GameObject lvEffect;//升级特效
    public GameObject goldEffect;//发放金币特效

    public Sprite[] sprites;//存放所有的背景图片
    public GameObject seaWave;//波浪
    public Image bgImage;// 用来存放当前背景图片
    private int indexBG;
    private bool isChangeBG=true;

    public  static GameControl instance = null;//单例

    void Awake()
    {
        instance = this;
    }
    void Start()
    {
        gold = PlayerPrefs.GetInt("gold", gold);
        lv = PlayerPrefs.GetInt("lv", lv);
        exp = PlayerPrefs.GetInt("exp", exp);
        smallTimer = PlayerPrefs.GetFloat("scd", smallCountDown);
        bigTimer = PlayerPrefs.GetFloat("bcd", bigCountDown);
        UpdateUI();
    }
    void Update()
    {
        ChangeGunByScr();
        Fire();
        UpdateUI();
        ChangeBG();
    }
    void UpdateUI()//更新UI
    {
        bigTimer -= Time.deltaTime;
        smallTimer -= Time.deltaTime;
        if (smallTimer <= 0)
        {
            smallTimer = smallCountDown;
            gold += 30;
        }
        if (bigTimer <= 0 && bigCountTimeButton.gameObject.activeSelf==false)
        {
            bigCountDownText.gameObject.SetActive(false);
            bigCountTimeButton.gameObject.SetActive(true);
        }
        //所需升级经验=1000+当前等级*200
        while (exp >= 1000 + lv * 200)
        {
            exp = exp - (1000 + lv * 200);
            lv++;
            Audiomanager.Instance.PlayEffectSound(Audiomanager.Instance.lvUpClip);
            isChangeBG = true;
            LvUpTips.SetActive(true);
            LvUpTips.transform.Find("Text").GetComponent<Text>().text = lv.ToString();
            StartCoroutine(LvUpTips.GetComponent<HideSelf>().Hide(0.7f));//携程，在半秒后将提示隐藏 
            Instantiate(lvEffect);
        }
        goldText.text = "$"+gold;
        lvText.text = lv.ToString();
        if (lv % 10<=9)
        {
            lvNameText.text = lvName[lv / 10];
        }
        else
        {
            lvNameText.text = lvName[9];
        }
        smallCountDownText.text = " " + (int)smallTimer / 10 + " " + (int)smallTimer % 10;
        bigCountDownText.text = (int)bigTimer + "s";
        expSlider.value = (float)exp / (1000 + lv * 200);
    }
    private void ChangeGunByScr()//通过鼠标改变枪
    {
        if (Input.GetAxis("Mouse ScrollWheel")<0)
        {
            OnButtonAddDown();
        }
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            OnButtonMinusDown();
        }
    }
    public void OnButtonAddDown()//给枪升级，同时更新UI
    {
        guns[indexCost / 4].SetActive(false);
        indexCost++;
        Instantiate(changeFireEffect);
        indexCost = (indexCost > oneShootCost.Length - 1) ? 0 : indexCost;
        guns[indexCost / 4].SetActive(true);
        Audiomanager.Instance.PlayEffectSound(Audiomanager.Instance.changeFireClip);
        costText.text = "$"+oneShootCost[indexCost];
    }
    public void OnButtonMinusDown()//给枪降级，同时更新UI
    {
        guns[indexCost / 4].SetActive(false);
        indexCost--;
        indexCost = (indexCost <0) ? oneShootCost.Length-1 : indexCost;
        guns[indexCost / 4].SetActive(true);
        Audiomanager.Instance.PlayEffectSound(Audiomanager.Instance.changeFireClip);
        costText.text = "$" + oneShootCost[indexCost];
    }
    void Fire()//开枪 
    {
        GameObject[] useBullets=bullet5;//存放当前的子弹,初始值为bullet5
        int indexBullets;//当前子弹的类型
        if (Input.GetMouseButtonDown(0)&&EventSystem.current.IsPointerOverGameObject()==false)
        {
            if (gold >= oneShootCost[indexCost])//如果当前金币大于开枪的花费才可以开枪
            {
                switch (indexCost / 4)
                {
                    case 0: useBullets = bullet1; break;
                    case 1: useBullets = bullet2; break;
                    case 2: useBullets = bullet3; break;
                    case 3: useBullets = bullet4; break;
                    case 4: useBullets = bullet5; break;
                }
                indexBullets = (LV % 10) > 9 ? 9 : LV % 10;
                gold -= oneShootCost[indexCost];
                Instantiate(fireEffect);// 开火特效
                GameObject bullet = Instantiate(useBullets[indexBullets]);
                Audiomanager.Instance.PlayEffectSound(Audiomanager.Instance.fireClip);
                //Debug.Log(indexBullets);
                bullet.transform.SetParent(bulletHolder, false);
                bullet.GetComponent<BulletAttr>().damage = oneShootCost[indexCost];
                bullet.transform.position = guns[indexCost / 4].transform.Find("FirePos").position;
                bullet.transform.rotation = guns[indexCost / 4].transform.Find("FirePos").rotation;
                //让子弹沿着本身的y轴前进
                bullet.AddComponent<Ef_fishMove>().dir = Vector3.up;
                bullet.GetComponent<Ef_fishMove>().speed = bullet.GetComponent<BulletAttr>().speed;
            }
            else
            {
                //做提醒金币不足的提示
                StartCoroutine(GoldNotEnough());
            }
        }
    }
    public void OnbigCountDownButtonDown()//大倒计时时间到后的处理
    {
        gold += 500;
        Audiomanager.Instance.PlayEffectSound(Audiomanager.Instance.goldClip);
        Instantiate(goldEffect);
        bigCountTimeButton.gameObject.SetActive(false);
        bigCountDownText.gameObject.SetActive(true);
        bigTimer = bigCountDown; 
    }
    IEnumerator GoldNotEnough()//金币不足的提醒
    {
        goldColor = goldText.color;
        goldText.color = Color.red;
        yield return new WaitForSeconds(0.5f);
        goldText.color = goldColor;
    }
    void ChangeBG()
    {
        if (lv % 15 == 0&&isChangeBG==true&&lv!=0)// 每过十级更换一次背景图片
        {
            Instantiate(seaWave);// 实例化特效海浪 
            Audiomanager.Instance.PlayEffectSound(Audiomanager.Instance.seaWaveClip);
            isChangeBG = false;
            indexBG = lv / 15;
            if (indexBG >= 3)
            {
                bgImage.sprite = sprites[Random.Range(1,4)];
            }
            else
            {
                bgImage.sprite = sprites[indexBG];
            }
        }
    }
}
