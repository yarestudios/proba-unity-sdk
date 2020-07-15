using UnityEngine;
using Proba.Serializables;
using Proba.Http;

namespace Proba
{
    public class Player
    {
        public delegate void OnRequestFinish(PlayerData playerData);
        public delegate void SimpleCallback();

        // Raw data from API response
        private static PlayerData data;

        // Property to return the credits
        public static int Credits {
            get {
                if (Player.data != null) {
                    return Player.data.credits;
                }
                return 0;
            }
        }

        /// <summary>
        /// This will be called when we are giving credits to a player
        /// after he completed the IAP purchase. The transactionId
        /// and the Receipt can be null since they are comming from
        /// the data that we get from the Product object
        /// </summary>
        /// <param name="amount">Amount of credits bought</param>
        /// <param name="transactionId">Purchase transaction ID</param>
        /// <param name="receipt">The receipt json string</param>
        public static void Initialize(OnRequestFinish callback)
        {
            string initPlayerUrl = string.Format(
                "/games/{0}/player/data",
                Proba.Configuration.GameId
            );

            Request request = new Request(initPlayerUrl);
            request.Get().onFinish += (r) => {
                PlayerDataResponse response = JsonUtility.FromJson<PlayerDataResponse>(r.Response);
                Player.data = response.data;
                callback(response.data);
            };
        }

        /// <summary>
        /// Force a reload of the player data
        /// </summary>
        public static void Reload()
        {
            Player.Initialize((data) => {
                // The player data will be updated on the
                // on Finish event for this request
            });
        }

        /// <summary>
        /// Helper to let us know if the player is loaded from the
        /// database and API
        /// </summary>
        /// <returns>Boolean</returns>
        public static bool IsLoaded()
        {
            return Player.data != null;
        }

        /// <summary>
        /// This will retrieve the data for the player. Since the data can change depending
        /// on the game we need to pass the reponse serializable definition so we can
        /// parse it from the APIs JSON response
        /// </summary>
        public static void LoadData(OnRequestFinish callback)
        {
            string playerDataUrl = string.Format(
                "/games/{0}/player/data",
                Proba.Configuration.GameId
            );

            Request request = new Request(playerDataUrl);
            request.Get().onFinish += (r) => {
                PlayerDataResponse response = JsonUtility.FromJson<PlayerDataResponse>(r.Response);
                callback(response.data);
            };
        }

        /// <summary>
        /// Earn some credits
        /// </summary>
        /// <param name="callback"></param>
        public static void EarnCredits(int amount)
        {
            string earnCreditsUrl = string.Format(
                "/games/{0}/player/credits/earn",
                Proba.Configuration.GameId
            );

            Request request = new Request(earnCreditsUrl);
            request.AddParam("amount", amount).Post();
        }

        /// <summary>
        /// Saves the email for the player data
        /// </summary>
        /// <param name="callback"></param>
        public static void Identify(string email, SimpleCallback callback)
        {
            string identifyUrl = string.Format(
                "/games/{0}/player/identify",
                Proba.Configuration.GameId
            );

            Request request = new Request(identifyUrl);
            request
                .AddParam("email", email)
                .Post()
                .onFinish += (r) => {
                    BaseResponse res = JsonUtility.FromJson<BaseResponse>(r.Response);
                    if (res.success) {
                        callback();
                    }
                };
        }

    }
}
