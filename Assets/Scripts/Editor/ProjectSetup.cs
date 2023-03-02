using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Lukens
{
    public class ProjectSetup : Editor
    {
        [MenuItem("Lukens/Project Setup/Setup Folders")]
        public static void CreateFolders()
        {
            AssetDatabase.CreateFolder("Assets", "Prefabs");
            AssetDatabase.CreateFolder("Assets", "Animations");
            AssetDatabase.CreateFolder("Assets", "BLENDER");
            AssetDatabase.CreateFolder("Assets", "Images");
            AssetDatabase.CreateFolder("Assets", "ScriptableObjects");
            AssetDatabase.CreateFolder("Assets", "Scripts");
            AssetDatabase.CreateFolder("Scripts", "Editor");
            AssetDatabase.CreateFolder("Assets", "Materials");
            AssetDatabase.CreateFolder("Materials", "PhysicsMats");
        }
    }
}