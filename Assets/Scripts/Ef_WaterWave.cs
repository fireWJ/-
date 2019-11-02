using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ef_WaterWave : MonoBehaviour {
    private Material material;//用来获取Plane的材质
    public Texture[] texture;//定义数组来更换图片，用来达到水波荡漾的效果
    private int index;//数组索引
	void Start ()
    {
        material = this.GetComponent<MeshRenderer>().material;
        InvokeRepeating("ChangeTexture", 0, 0.1f);//每隔一秒调用来改变texture
	}

    public void ChangeTexture()
    {
        material.mainTexture = texture[index];
        index = (index + 1) % texture.Length;
    }
}
