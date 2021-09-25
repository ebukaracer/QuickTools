using System;
using System.IO;
using UnityEngine;

namespace Racer
{
    public static class FolderCreator
    {
        public static void CreateDirectory(string rootDir, params string[] subDir)
        {
            try
            {
                string dataPath = Application.dataPath;

                string rootPath = Path.Combine(dataPath, rootDir);

                foreach (var subPathName in subDir)
                {
                    string subPaths = Path.Combine(rootPath, subPathName);

                    if (Directory.Exists(subPaths))
                        continue;

                    Directory.CreateDirectory(subPaths);
                }
            }
            catch (Exception e)
            {
                Debug.LogWarning(e.Message);
            }
        }

        public static void DeleteDirectory(string rootDir, params string[] subDir)
        {
            try
            {
                string dataPath = Application.dataPath;

                string rootPath = Path.Combine(dataPath, rootDir);

                foreach (var subPathName in subDir)
                {
                    if (string.IsNullOrEmpty(subPathName)) continue;

                    string subPaths = Path.Combine(rootPath, subPathName);
                    string subPathsMeta = subPaths + ".meta";

                    if (!Directory.Exists(subPaths)) continue;

                    Directory.Delete(subPaths);
                    File.Delete(subPathsMeta);
                }
            }
            catch (Exception e)
            {
                Debug.LogWarning(e.Message);
            }
        }
    }
}