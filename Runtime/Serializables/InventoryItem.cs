using UnityEngine;
using System.Collections;
using Proba.Http;

namespace Proba.Serializables
{
    [System.Serializable]
    public class InventoryItem : Product
    {
        public int      quantity;
        public int      max_quantity;

        /// <summary>
        /// Consume this item
        /// </summary>
        /// <param name="amount"></param>
        public void Consume(int amount = 1)
        {
            // Not enough
            if (amount > quantity) {
                Debug.Log("Not enough items to consume");
                return;
            }

            // Nothing to do here
            if (amount <= 0) {
                return;
            }

            string consumeItemUrl = string.Format(
                "/games/{0}/products/{1}/consume",
                Proba.Configuration.GameId,
                this.id
            );

            Request request = new Request(consumeItemUrl);
            request
                .AddParam("quantity", amount)
                .Post();

            this.quantity -= amount;
        }

    }

}
