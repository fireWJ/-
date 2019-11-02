using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XLua;
using System.IO;
using XLua;
using UnityEngine.Networking;
[Hotfix]
public class HotFix : MonoBehaviour {
    private LuaEnv luaEnv;
    public static Dictionary<string, GameObject> preDic = new Dictionary<string, GameObject>();
	// Use this for initialization
    [LuaCallCSharp]
	void Awake () {
        luaEnv = new LuaEnv();
        luaEnv.AddLoader(myLoader);
        luaEnv.DoString("require'myLoader'");
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    private byte[] myLoader(ref string filePath)
    {
        string absPath = @"E:\Unity2018 Projects\Fishing\Assets\Scripts\LuaScripts\" + filePath + ".lua.txt";
        return System.Text.Encoding.UTF8.GetBytes(File.ReadAllText(absPath));
    }
    private void OnDisable()
    {
        luaEnv.DoString("require'disFunction'");
    }
    private void OnDestroy()
    {
        //Debug.Log(123);
        luaEnv.Dispose();
    }
    [LuaCallCSharp]
    public void LoadResources(string ResName,string filePath)
    {
        StartCoroutine(LoadResourcesCorotine(ResName, filePath));
    }
    IEnumerator LoadResourcesCorotine(string ResName,string filePath)
    {
        UnityWebRequest request = UnityWebRequestAssetBundle.GetAssetBundle(@"http://localhost/AssetBundles/" + filePath);
        yield return request.SendWebRequest();
        AssetBundle ab = (request.downloadHandler as DownloadHandlerAssetBundle).assetBundle;
        GameObject gameObject = ab.LoadAsset<GameObject>(ResName);
        preDic.Add(ResName, gameObject);
    }
    [LuaCallCSharp]
    public static GameObject GetGameObject(string goName)
    {
        return preDic[goName];
    }
}
