
using UnityEditor;
using System.IO;

public class CreateAssetBundles
{
	[MenuItem("Assets/Build AssetBundles")]
	static void BuildAllAssetBundles()
	{
		string assetBundleDirectory = "Assets/StreamingAssets";
		if(!Directory.Exists(assetBundleDirectory))
		{
			Directory.CreateDirectory(assetBundleDirectory);
		}

		BuildPipeline.BuildAssetBundles(
			assetBundleDirectory,
			BuildAssetBundleOptions.None,
			EditorUserBuildSettings.activeBuildTarget);
	}

	[MenuItem("Assets/Build AssetBundles Uncompressed")]
	static void BuildAllAssetBundlesUncompressed()
	{
		string assetBundleDirectory = "Assets/StreamingAssets";
		if(!Directory.Exists(assetBundleDirectory))
		{
			Directory.CreateDirectory(assetBundleDirectory);
		}

		BuildPipeline.BuildAssetBundles(
			assetBundleDirectory,
			BuildAssetBundleOptions.UncompressedAssetBundle,
			EditorUserBuildSettings.activeBuildTarget);
	}
}