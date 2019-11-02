using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XLua;

[Hotfix]
public class Ef_GooldMove : MonoBehaviour {

    public GameObject goldCollect;
	// Use this for initialization
	void Start () {
        goldCollect = GameObject.Find("GoldCollet");
	}
	
	// Update is called once per frame
    [LuaCallCSharp]
	void Update () {
        transform.position = Vector3.Lerp(transform.position, goldCollect.transform.position, 2 * Time.deltaTime);
	}
}
