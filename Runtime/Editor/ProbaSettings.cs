using UnityEngine;
using UnityEditor;
using System.Collections;

namespace Proba.Editor
{
    public class ProbaSettings : EditorWindow
    {
        Proba.Configuration _conf;

        private GUIStyle probaTitleGUIStyle = new GUIStyle();

        [MenuItem ("Window/Proba/Settings")]
        public static void ShowWindow() {
            GetWindow<ProbaSettings>("Proba Settings", true);
        }

        /// <summary>
        /// This function is called when the object becomes enabled and active.
        /// </summary>
        void OnEnable()
        {
            _conf = GetInstance();
        }

        /// <summary>
        /// OnGUI is called for rendering and handling GUI events.
        /// This function can be called multiple times per frame (one call per event).
        /// </summary>
        void OnGUI()
        {
            EditorGUILayout.Space();

            probaTitleGUIStyle.fontSize = 30; //change the font size
            EditorGUILayout.LabelField("Proba", probaTitleGUIStyle, GUILayout.Height(50));

            // Here we update the values for the Proba configuration
            _conf.apiBaseUrl    = EditorGUILayout.TextField("API url", _conf.apiBaseUrl);
            _conf.gameId        = EditorGUILayout.IntField("Game Id", _conf.gameId);
            _conf.debug         = EditorGUILayout.Toggle("Debug", _conf.debug);

            // API Token field
            EditorStyles.textField.wordWrap = true;
            GUILayout.Label("API Token", EditorStyles.boldLabel);
            GUILayout.Label("You can find this token on the game details page");
            _conf.apiToken  = EditorGUILayout.TextArea(
                _conf.apiToken,
                GUILayout.Height(50)
            );

            // Save config button
            if(GUILayout.Button("Save", GUILayout.Width(100)))
            {
                EditorUtility.SetDirty(_conf);
                SaveConfig();
            }
        }

        /// <summary>
        /// Gets the instance for the configuration object so
        /// we can update its properties with this window
        /// </summary>
        /// <returns>Proba.Configuration instance</returns>
        Configuration GetInstance()
        {
            Configuration config = Configuration.Load();

            // It could happen (for new projects) that the Proba asset
            // resource is not available. This will make sure to
            // create it with the default properties and values
            if(config == null)
            {
                config = CreateInstance<Configuration>();
                AssetDatabase.CreateAsset(config , Configuration.RESOURCE_PATH);
            }

            return config;
        }

        /**
        * This will update the assets that we have been modifying in here
        * for example the Proba.asset file that is living under Resources
        */
        void SaveConfig()
        {
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }

    }
}
