using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ef_EffectPlay : MonoBehaviour {

    public GameObject[] effects;
	

    public void PlayEffect()
    {
        foreach(GameObject effect in effects)
        {
            Instantiate(effect);
        }
    }
}
