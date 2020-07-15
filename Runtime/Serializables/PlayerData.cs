using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Proba.Http;

namespace Proba.Serializables
{
    [System.Serializable]
    public class PlayerData
    {
        public delegate void SimpleCallback(BaseResponse response);

        public int id;
        public string email;
        public string device_identifier;
        public int game_id;
        public int credits;
        public PlayerProperty[] properties;

        /// <summary>
        /// Get a property by name
        /// </summary>
        /// <param name="name"></param>
        /// <param name="default"></param>
        /// <returns>String</returns>
        public string GetProperty(string name, string defaultValue)
        {
            foreach (PlayerProperty prop in properties)
            {
                if (prop.name == name) {
                    return prop.value;
                }
            }
            return defaultValue;
        }

        /// <summary>
        /// Save a property into the properties list for this player
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        public void SaveProperty(string name, string value, SimpleCallback callback = null)
        {
            string savePropertyUrl = string.Format(
                "/games/{0}/player/properties",
                Proba.Configuration.GameId
            );

            Request request = new Request(savePropertyUrl);
            request.AddParam("name", name);
            request.AddParam("value", value);

            // Send request
            request
                .Post()
                .onFinish += (r) => {
                    if (callback != null) {
                        BaseResponse res = JsonUtility.FromJson<BaseResponse>(r.Response);
                        callback(res);
                    }
                };;
        }

        /// <summary>
        /// Spend an amount of credits
        /// </summary>
        /// <param name="credits">Amount of credits</param>
        public void Spend(int credits, SimpleCallback callback = null)
        {
            string spendCreditsUrl = string.Format(
                "/games/{0}/player/spend",
                Proba.Configuration.GameId
            );

            Request request = new Request(spendCreditsUrl);
            request.AddParam("amount", credits);

            // Send request
            request
                .Post()
                .onFinish += (r) => {
                    if (callback != null) {
                        BaseResponse res = JsonUtility.FromJson<BaseResponse>(r.Response);
                        callback(res);
                    }
                };;
        }

    }
}
