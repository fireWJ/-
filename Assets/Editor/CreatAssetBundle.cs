using UnityEditor;
using System.IO;

public class CreatAssetBundle {

    [MenuItem("Assets/Bulid AssetBundles")]
	public static void BulidAssetBundles()
    {
        string dir = "AssetBundles";
        if (Directory.Exists(dir)==false)
        {
            Directory.CreateDirectory(dir);
        }
        BuildPipeline.BuildAssetBundles(dir, BuildAssetBundleOptions.None, BuildTarget.StandaloneWindows64);
    }
}
