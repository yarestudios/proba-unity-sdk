using UnityEngine;
using Proba.Http;

namespace Proba
{
    public class Purchases
    {
        public delegate void OnRequestFinish();

        /// <summary>
        /// This will be called when we are giving credits to a player
        /// after he completed the IAP purchase. The transactionId
        /// and the Receipt can be null since they are comming from
        /// the data that we get from the Product object
        /// </summary>
        /// <param name="amount">Amount of credits bought</param>
        /// <param name="transactionId">Purchase transaction ID</param>
        /// <param name="receipt">The receipt json string</param>
        public static void Credits(int amount, string transactionId, string receipt)
        {
            string purchaseCreditsUrl = string.Format(
                "/games/{0}/purchases/credits",
                Proba.Configuration.GameId
            );

            Request request = new Request(purchaseCreditsUrl);
            request.AddParam("amount", amount)
                   .AddParam("transaction_id", transactionId)
                   .AddParam("receipt", receipt)
                   .Post();
        }
    }
}
