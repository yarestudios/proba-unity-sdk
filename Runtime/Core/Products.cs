using UnityEngine;
using Proba.Serializables;
using Proba.Http;

namespace Proba
{
    public class Products
    {
        public delegate void OnRequestFinish(ProductCollection collection);
        public delegate void OnItemRequestFinish(ItemsCollection collection);
        public delegate void SimpleCallback(BaseResponse response);

        /// <summary>
        /// Method that will be called to load all the items available
        /// for purchase from the backend server
        /// </summary>
        public static void LoadAll(OnRequestFinish callback)
        {
            string getAllProductsUrl = string.Format(
                "/games/{0}/products",
                Proba.Configuration.GameId
            );

            Request request = new Request(getAllProductsUrl);
            request.Get().onFinish += (r) => {
                ProductCollection collection = JsonUtility.FromJson<ProductCollection>(r.Response);
                callback(collection);
            };
        }

        /// <summary>
        /// Method that will call the backend to get all the products
        /// for a particular gamer and player (inventory)
        /// </summary>
        /// <param name="callback"></param>
        public static void LoadInventory(OnItemRequestFinish callback)
        {
            string getInventoryProductsUrl = string.Format(
                "/games/{0}/products/inventory",
                Proba.Configuration.GameId
            );

            Request request = new Request(getInventoryProductsUrl);
            request.Get().onFinish += (r) => {
                ItemsCollection collection = JsonUtility.FromJson<ItemsCollection>(r.Response);
                callback(collection);
            };
        }

    }
}
