using UnityEditor;
using UnityEngine;

namespace Racer.QuickTools
{
    public class ToolsMenuWindow : EditorWindow
    {
        const int WIDTH = 800;
        static string subDirNames = "_Scripts Scenes Materials Models Animations Sprites Prefabs";
        static string projectRootDir = "_Project";



        [MenuItem("Quick_Tools/Setup Window")]
        static void DisplayWindow()
        {
            projectRootDir = EditorPrefs.HasKey("prd") ? EditorPrefs.GetString("prd") : projectRootDir;
            subDirNames = EditorPrefs.HasKey("sdn") ? EditorPrefs.GetString("sdn") : subDirNames;

            var window = GetWindow<ToolsMenuWindow>();

            // Add tool-tip argument as well
            window.titleContent = new GUIContent("Tools Menu Window");

            // Limit size of the window, non re-sizable
            window.minSize = new Vector2(WIDTH, WIDTH / 4);
            window.maxSize = new Vector2(WIDTH, WIDTH / 4);
        }

        private void OnGUI()
        {
            EditorGUILayout.Space(10);

            EditorGUILayout.BeginHorizontal(GUILayout.MaxWidth(WIDTH));
            EditorGUILayout.HelpBox("Input multiple sub-directories(names) by separating with either comma(,) or space(' ').", MessageType.Info);
            EditorGUILayout.EndHorizontal();

            GUILayout.Label("Directory Settings", EditorStyles.boldLabel);

            projectRootDir = EditorGUILayout.TextField("Root Directory", projectRootDir);

            EditorGUILayout.BeginHorizontal(GUILayout.Width(WIDTH - 5));
            subDirNames = EditorGUILayout.TextField("Sub-Directories", subDirNames);
            if (GUILayout.Button(new GUIContent("Create", "Creates a directory in the [Root Directory] specified."),
                GUILayout.MaxWidth(150)))
            {
                FolderCreator.CreateDirectory(rootDir: projectRootDir, subDir: subDirNames.Split(',', ' '));

                AssetDatabase.Refresh();
            }

            if (GUILayout.Button(new GUIContent("Delete", "Deletes a directory from the [Root Directory] specified."),
                GUILayout.MaxWidth(150)))
            {
                FolderCreator.DeleteDirectory(rootDir: projectRootDir, subDir: subDirNames.Split(',', ' '));

                AssetDatabase.Refresh();
            }
            EditorGUILayout.EndHorizontal();
        }

        private void OnDestroy()
        {
            EditorPrefs.SetString("sdn", subDirNames);
            EditorPrefs.SetString("prd", projectRootDir);
        }
    }
}