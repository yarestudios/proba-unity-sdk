using UnityEngine;
using System.Collections;

namespace Proba
{
    public class Configuration : ScriptableObject
    {
        public const string API_BASE_URL = "http://proba.io/api";
        public const string RESOURCE_NAME = "Proba";
        public const string RESOURCE_PATH = "Assets/Resources/Proba.asset";

        public static Configuration Instance;

        // These are the config properties
        public string   apiBaseUrl = API_BASE_URL;
        public int      gameId     = 0;
        public string   apiToken   = "The game token";
        public bool     debug      = false;

        /// <summary>
        /// Property to return the API base Url
        /// </summary>
        /// <value>string | url</value>
        public static string APIBaseUrl {
            get {
                return (Instance.apiBaseUrl.Length > 0)
                    ? Instance.apiBaseUrl
                    : API_BASE_URL;
            }
        }

        /// <summary>
        /// Property to return the API base Url
        /// </summary>
        /// <value>string | url</value>
        public static string APIToken {
            get {
                return Instance.apiToken;
            }
        }

        /// <summary>
        /// Return the configured game id
        /// </summary>
        /// <value>the Game id</value>
        public static int GameId {
            get {
                return Instance.gameId;
            }
        }

        /// <summary>
        /// If we want to print request data in the console
        /// </summary>
        /// <value>bool</value>
        public static bool Debug {
            get {
                return (Instance.debug)
                    ? Instance.debug
                    : false;
            }
        }

        /**
         * This is called then we initialize the Engine
         * so all the configurations are part of the game
         */
        public static Configuration Load()
        {
            // We only need to initialize the config once
            if (Instance == null) {
                Instance = Resources.Load(Configuration.RESOURCE_NAME) as Configuration;
            }
            return Instance;
        }
    }
}
