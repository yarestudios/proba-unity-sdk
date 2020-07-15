using UnityEngine;
using Proba.Serializables;
using Proba.Http;

namespace Proba
{
    public class Achievements
    {
        public delegate void OnRequestFinish(AchievementCollection achievements);
        public delegate void OnAwardedFinish(Achievement achievement);

        /// <summary>
        /// This method will award an achievement to the current
        /// player device by it's achievement key
        /// </summary>
        /// <param name="key">Achievement key string</param>
        public static void Award(string key, OnAwardedFinish callback)
        {
            string awardUrl = string.Format(
                "/games/{0}/achievements/award",
                Proba.Configuration.GameId
            );

            Request request = new Request(awardUrl);
            request
                .AddParam("key", key)
                .Post()
                .onFinish += (r) => {
                    AchievementAwardedResponse res = JsonUtility.FromJson<AchievementAwardedResponse>(r.Response);
                    if (res.success) {
                        callback(res.achievement);
                    }
                };
        }

        /// <summary>
        /// Get a collection of all the achievements available
        /// to the player so we can display them in the game
        /// </summary>
        public static void List(OnRequestFinish callback)
        {
            string achievementsListUrl = string.Format(
                "/games/{0}/achievements",
                Proba.Configuration.GameId
            );

            Request request = new Request(achievementsListUrl);
            request.Get().onFinish += (r) => {
                AchievementCollection collection = JsonUtility.FromJson<AchievementCollection>(r.Response);
                callback(collection);
            };
        }

    }
}
