using System.IO;
using UnityEditor;
using UnityEngine;

public static class DoubleInstallAvoider
{
	[InitializeOnLoadMethod]
	static void OnLoad()
	{
		CheckDoubleInstall();
	}
	
	static void CheckDoubleInstall()
	{
		if (Directory.Exists("Assets/Adjust"))
		{
			if (EditorUtility.DisplayDialog("Double Adjust Install", "You have installed Adjust both in Packages and in Assets.\nDelete the Assets/Adjust folder?", "OK", "Cancel"))
			{
				Directory.Delete("Assets/Adjust", true);
				File.Delete("Assets/Adjust.meta");
				Debug.Log("Deleted Assets/Adjust. Refreshing Assets.");
				AssetDatabase.Refresh(ImportAssetOptions.ForceUpdate | ImportAssetOptions.ImportRecursive | ImportAssetOptions.ForceSynchronousImport);
			}
		}
	}
	
}
