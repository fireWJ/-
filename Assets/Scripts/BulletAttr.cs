using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletAttr : MonoBehaviour {

    public int damage;//子弹的威力

    public int speed;//子弹的速度

    public GameObject web;// 子弹

    private void OnTriggerEnter2D(Collider2D collision)// 子弹碰上鱼后
    {
        if (collision.tag == "Fish")
        {
            GameObject fishWeb = Instantiate(web);
            fishWeb.transform.SetParent(gameObject.transform.parent, false);//  将生成的网和子弹放在一起 
            fishWeb.transform.position = gameObject.transform.position;
            fishWeb.GetComponent<WebAttr>().damage = damage;
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update () {
        if (transform.localPosition.x > 900 || transform.localPosition.x < -900 || transform.localPosition.y > 500 || transform.localPosition.y < -500)
        {
            Destroy(this.gameObject);
        }
    }
}
