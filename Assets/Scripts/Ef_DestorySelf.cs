using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ef_DestorySelf : MonoBehaviour {

    public float delayTime = 1.5f;

	// Use this for initialization
	void Start () {
        Destroy(this.gameObject, delayTime);
	}
}
