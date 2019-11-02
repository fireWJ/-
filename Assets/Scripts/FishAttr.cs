using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XLua;

[Hotfix]
public class FishAttr : MonoBehaviour {

	public int maxNum;//每次生成鱼的最大数量
    public int maxSpeed;//鱼的最大速度 
    public int hp;//鱼的生命值 
    public GameObject fishDie;//
    public int exp;//打死鱼后获取的经验值
    public int gold;//打死鱼后获取的金币数量；
    public GameObject rewardGold;//打死鱼后奖励的金币
    void Update()//判断鱼是否出界，如果是则销毁
    {
        if (transform.localPosition.x > 900 || transform.localPosition.x < -900 || transform.localPosition.y > 500 || transform.localPosition.y < -500)
        {
            Destroy(this.gameObject);
        }
    }
    [LuaCallCSharp]
    public void TakeDamage(int value)//鱼被子弹攻击
    {
        hp -= value;
       // Debug.Log(hp);
        if (hp <= 0)
        {
            Die();
        }         
    }

    private void Die()// 处理鱼死亡后的事情
    {
        GameControl.instance.gold += gold;
        GameControl.instance.exp += exp;
        Destroy(gameObject);
        GameObject fish = Instantiate(fishDie);
        fish.transform.SetParent(gameObject.transform.parent, false);
        fish.transform.position = transform.position;
        fish.transform.rotation = transform.rotation;
        GameObject rewardGolds = Instantiate(rewardGold);
        rewardGolds.transform.SetParent(gameObject.transform.parent, false);
        rewardGolds.transform.position = transform.position;
        rewardGolds.transform.rotation = transform.rotation;
        if (gameObject.GetComponent<Ef_EffectPlay>() != null)
        {
            Audiomanager.Instance.PlayEffectSound(Audiomanager.Instance.rewardClip);
            gameObject.GetComponent<Ef_EffectPlay>().PlayEffect();
        }
    }

}
