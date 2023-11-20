//=========================================================================================
/**
	asset cleaner
	Copyright (c) 2015 Tatsuhiko Yamamura

    This software is released under the MIT License.
    http://opensource.org/licenses/mit-license.php
*/
//=========================================================================================
using UnityEngine;
using System.Collections.Generic;
using UnityEditor;
using System.IO;
using System.Linq;
//=========================================================================================
namespace AssetClean
{
    //===============================================================
    public class FindUnusedAssets : EditorWindow
	{
        //-------------------------------
		AssetCollector collection = new AssetCollector ();
		List<DeleteAsset> deleteAssets = new List<DeleteAsset> ();
		Vector2 scroll;
        //-------------------------------
        [MenuItem("Tools/Delete Unused Assets/only resource", false, 50)]
        static void InitWithoutCode ()
		{
			var window = FindUnusedAssets.CreateInstance<FindUnusedAssets> ();
			window.collection.useCodeStrip = false;
			window.collection.Collection ();
			window.CopyDeleteFileList (window.collection.deleteFileList);
			
			window.Show ();

        }// static void InitWithoutCode ()
        //-------------------------------
        [MenuItem("Tools/Delete Unused Assets/unused by editor", false, 51)]
        static void InitWithout ()
		{
			var window = FindUnusedAssets.CreateInstance<FindUnusedAssets> ();
			window.collection.Collection ();
			window.CopyDeleteFileList (window.collection.deleteFileList);
			
			window.Show ();

        }// static void InitWithout ()
        //-------------------------------
        [MenuItem("Tools/Delete Unused Assets/unused by game", false, 52)]
        static void Init ()
		{
			var window = FindUnusedAssets.CreateInstance<FindUnusedAssets> ();
			window.collection.saveEditorExtensions = false;
			window.collection.Collection ();
			window.CopyDeleteFileList (window.collection.deleteFileList);
			
			window.Show ();

        }// static void Init ()
        //-------------------------------
        void OnGUI ()
		{
            using (var horizonal = new EditorGUILayout.HorizontalScope("box"))
            {
                EditorGUILayout.LabelField("delete unreference assets from buildsettings and resources");

                if (GUILayout.Button("Delete", GUILayout.Width(120), GUILayout.Height(40)) && deleteAssets.Count != 0)
                {
                    RemoveFiles();
                    Close();
                }

            }// using (var horizonal = new EditorGUILayout.HorizontalScope("box"))

            using (var scrollScope = new EditorGUILayout.ScrollViewScope(scroll))
            {
                scroll = scrollScope.scrollPosition;
                foreach (var asset in deleteAssets)
                {
                    if (string.IsNullOrEmpty(asset.path))
                        continue;

                    using (var horizonal = new EditorGUILayout.HorizontalScope())
                    {
                        asset.isDelete = EditorGUILayout.Toggle(asset.isDelete, GUILayout.Width(20));
                        var icon = AssetDatabase.GetCachedIcon(asset.path);
                        GUILayout.Label(icon, GUILayout.Width(20), GUILayout.Height(20));

                        if (GUILayout.Button(asset.path, EditorStyles.largeLabel))
                            Selection.activeObject = AssetDatabase.LoadAssetAtPath<Object>(asset.path);

                    }// using (var horizonal = new EditorGUILayout.HorizontalScope())

                }// foreach (var asset in deleteAssets)

            }// using (var scrollScope = new EditorGUILayout.ScrollViewScope(scroll))

        }// void OnGUI ()
        //-------------------------------
        static void CleanDir()
        {
			RemoveEmptyDirectry ("Assets");
			AssetDatabase.Refresh ();

        }// static void CleanDir()
        //-------------------------------
        void CopyDeleteFileList(IEnumerable<string> deleteFileList)
		{
			foreach (var asset in deleteFileList)
            {
				var filePath = AssetDatabase.GUIDToAssetPath (asset);

                if (string.IsNullOrEmpty (filePath) == false)
					deleteAssets.Add (new DeleteAsset (){ path = filePath});

            }// foreach (var asset in deleteFileList)

        }// void CopyDeleteFileList(IEnumerable<string> deleteFileList)
        //-------------------------------
        void RemoveFiles ()
		{
			try
            {
				string exportDirectry = "BackupUnusedAssets";
				Directory.CreateDirectory (exportDirectry);
				var files = deleteAssets.Where (item => item.isDelete == true).Select (item => item.path).ToArray ();
				string backupPackageName = exportDirectry + "/package" + System.DateTime.Now.ToString ("yyyyMMddHHmmss") + ".unitypackage";
				EditorUtility.DisplayProgressBar ("export package", backupPackageName, 0);
				AssetDatabase.ExportPackage (files, backupPackageName);

				int i = 0;
				int length = deleteAssets.Count;

				foreach (var assetPath in files)
                {
					i++;
					EditorUtility.DisplayProgressBar ("delete unused assets", assetPath, (float)i / length);
					AssetDatabase.DeleteAsset (assetPath);

                }// foreach (var assetPath in files)

                EditorUtility.DisplayProgressBar ("clean directory", "", 1);
				foreach (var dir in Directory.GetDirectories("Assets"))
					RemoveEmptyDirectry (dir);

				System.Diagnostics.Process.Start (exportDirectry);

				AssetDatabase.Refresh ();

            }// try
            catch ( System.Exception e ){ Debug.Log(e.Message); }
            finally { EditorUtility.ClearProgressBar (); }

        }// void RemoveFiles ()
        //-------------------------------
        static void RemoveEmptyDirectry (string path)
		{
			var dirs = Directory.GetDirectories (path);
			foreach (var dir in dirs)
                RemoveEmptyDirectry (dir);

			var files = Directory.GetFiles (path, "*", SearchOption.TopDirectoryOnly).Where (item => Path.GetExtension (item) != ".meta");
			if (files.Count () == 0 && Directory.GetDirectories (path).Count () == 0)
            {
				var metaFile = AssetDatabase.GetTextMetaFilePathFromAssetPath(path);
				UnityEditor.FileUtil.DeleteFileOrDirectory (path);
				UnityEditor.FileUtil.DeleteFileOrDirectory (metaFile);

            }// if (files.Count () == 0 && Directory.GetDirectories (path).Count () == 0)

        }// static void RemoveEmptyDirectry (string path)
        //-------------------------------
        class DeleteAsset
		{
			public bool isDelete = true;
			public string path;

        }// class DeleteAsset
        //-------------------------------

    }// public class FindUnusedAssets : EditorWindow
    //===============================================================

}// namespace AssetClean
//=========================================================================================