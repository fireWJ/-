using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ef_fishRotate : MonoBehaviour {

    public float speed=5f;
	// Use this for initialization
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(Vector3.forward * Time.deltaTime * speed);
	}
}
