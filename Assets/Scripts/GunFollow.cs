﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XLua;

[Hotfix]
public class GunFollow : MonoBehaviour {

    public RectTransform UGUICavans;//定义画布
    public Camera mainCamera;
    [LuaCallCSharp]
	void Update () {
      
        Vector3 mousePos;
        RectTransformUtility.ScreenPointToWorldPointInRectangle(UGUICavans, new Vector2(Input.mousePosition.x, Input.mousePosition.y), mainCamera, out mousePos);
        float z;
        if (mousePos.x > transform.position.x)
        {
            z = -Vector3.Angle(Vector3.up, mousePos - transform.position);
        }
        else
        {
            z = Vector3.Angle(Vector3.up, mousePos - transform.position);
        }
        transform.localRotation = Quaternion.Euler(0, 0, z);
	}  
}
