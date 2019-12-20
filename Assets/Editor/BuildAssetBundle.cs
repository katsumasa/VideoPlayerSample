using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public sealed class BuildAssetBundle  {
    [MenuItem("VideoPlayerSample/Build AssetBundle")]
	static void Build()
    {
        string outputpath = Application.streamingAssetsPath + "/AssetBundle/" + EditorUserBuildSettings.activeBuildTarget;

        if (System.IO.Directory.Exists(outputpath) == false)
        {
            System.IO.Directory.CreateDirectory(outputpath);
        }

        BuildPipeline.BuildAssetBundles(
            outputpath,
            BuildAssetBundleOptions.UncompressedAssetBundle,
            EditorUserBuildSettings.activeBuildTarget
            );
    }
}
