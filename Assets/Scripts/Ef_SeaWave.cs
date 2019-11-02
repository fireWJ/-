using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ef_SeaWave : MonoBehaviour {

    private Vector3 temp;//临时变量，用来记录浪的位置
	// Use this for initialization
	void Start () {
        temp = -transform.position;
	}
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, temp, 0.8f* Time.deltaTime);
    }
}
