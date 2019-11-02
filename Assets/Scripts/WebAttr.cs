using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WebAttr : MonoBehaviour {

    public int damage;// 网的伤害

    public float disappearTime;//撒的网消失的时间

	// Use this for initialization
	void Start () {
        Destroy(this.gameObject, disappearTime);
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Fish")
        {
            collision.SendMessage("TakeDamage", damage);
        }
    }
}
