using System.Collections.Generic;
using UnityEngine.Networking;
using System.Collections;
using UnityEngine;

namespace Proba.Http
{
    public class Request
    {
        public delegate void OnRequestFinish(Request request);
        public event OnRequestFinish onFinish;

        public string   Url         { get; set; }
        public string   Response    { get; set; }

        private Dictionary<string, string> headers;
        private Dictionary<string, object> parameters;
        private UnityWebRequest www;

        /// <summary>
        /// Constructor for the Request object. Here we will initialize
        /// the elements we will require to make the API calls
        /// </summary>
        /// <param name="path">The url path starting with /</param>
        public Request(string path)
        {
            this.Url = Proba.Configuration.APIBaseUrl + path;
            this.headers = new Dictionary<string, string>();
            this.parameters = new Dictionary<string, object>();

            // If we want to show debug data
            if(Proba.Configuration.Debug) {
                this.Debug();
            }
        }

        /// <summary>
        /// Adds the base headers we will be sending with every request
        /// </summary>
        protected void AddBaseHeaders()
        {
            this.www.SetRequestHeader("Accept", "application/json");
            this.www.SetRequestHeader("X-Proba-Token", Proba.Configuration.APIToken);
            this.www.SetRequestHeader("X-Proba-Device-Id", SystemInfo.deviceUniqueIdentifier);
        }

        /// <summary>
        /// GET request
        /// </summary>
        /// <returns>Self</returns>
        public Request Get()
        {
            this.www = UnityWebRequest.Get(this.Url);
            this.AddBaseHeaders();

            if (Proba.Configuration.Debug)
                UnityEngine.Debug.Log(this.Url);

            APIRequestHandler.Instance.StartCoroutine(MakeRequest());
            return this;
        }

        /// <summary>
        /// POST request.
        /// This will use the parameters added with the AddParam
        /// method of the Request instance to sent form parameters
        /// to the API endpoint
        /// </summary>
        /// <returns>Self</returns>
        public Request Post()
        {
            WWWForm form = new WWWForm();

            // Here is where we will loop through all the parameters that
            // the developer wants to send to the backend
            foreach(KeyValuePair<string, object> entry in this.parameters)
            {
                form.AddField(entry.Key, entry.Value.ToString());
            }

            if (Proba.Configuration.Debug)
                UnityEngine.Debug.Log(this.Url);

            // We need some headers when we are making the request
            this.www = UnityWebRequest.Post(this.Url, form);
            this.AddBaseHeaders();

            foreach(KeyValuePair<string, string> header in this.headers)
            {
                this.www.SetRequestHeader(header.Key, header.Value.ToString());
            }

            APIRequestHandler.Instance.StartCoroutine(MakeRequest());
            return this;
        }

        /// <summary>
        /// Adds parameters to the POST requests
        /// </summary>
        /// <param name="name">Parameter name</param>
        /// <param name="value">Parameter value</param>
        /// <returns>Self</returns>
        public Request AddParam(string name, object value)
        {
            this.parameters.Add(name, value);
            return this;
        }

        /// <summary>
        /// Adds headers to the POST requests
        /// </summary>
        /// <param name="name">Header name</param>
        /// <param name="value">Header value</param>
        /// <returns>Self</returns>
        public Request AddHeader(string name, string value)
        {
            this.headers.Add(name, value);
            return this;
        }

        /// <summary>
        /// This will acually make the request as a Coroutine so we
        /// can wait for the www request to finish
        /// </summary>
        protected IEnumerator MakeRequest()
        {
            yield return www.SendWebRequest();
            this.Response = www.downloadHandler.text;

            // If we have an event attached
            if (onFinish != null) {
                onFinish(this);
                onFinish = null;
            }
        }

        /// <summary>
        /// Print the raw server response to the Console
        /// </summary>
        /// <returns>Self</returns>
        protected Request Debug()
        {
            // We only show debug on console if we have the flag
            if (Proba.Configuration.Debug) {
                onFinish += (r) => {
                    UnityEngine.Debug.Log(r.Response);
                };
            }
            return this;
        }
    }
}
