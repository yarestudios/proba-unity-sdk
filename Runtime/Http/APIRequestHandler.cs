using UnityEngine;
using System.Collections;

namespace Proba.Http
{
    /// <summary>
    /// Monobehaviour that we will create when a request is made
    /// because we want to use the UnityEngine Coroutines to
    /// wait for the WWW responses
    /// </summary>
    public class APIRequestHandler : MonoBehaviour
    {
        protected static APIRequestHandler _instance;

        public static APIRequestHandler Instance
        {
            get
            {
                if (_instance == null)
                {
                    GameObject go = new GameObject("Proba - API Request Handler");
                    go.AddComponent<APIRequestHandler>();
                }
                return _instance;
            }
        }

        /// <summary>
        /// Here we will initialize the _instance
        /// </summary>
        void Awake()
        {
            _instance = this;
        }
    }
}
