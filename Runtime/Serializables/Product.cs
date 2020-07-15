using UnityEngine;
using System.Collections;
using Proba.Http;

namespace Proba.Serializables
{
    [System.Serializable]
    public class Product
    {
        public int      id;
        public string   key;
        public string   name;
        public string   description;
        public int      price;


        /// <summary>
        /// Tell the API that we just purchased this product
        /// </summary>
        /// <param name="product"></param>
        public void Purchase(int quantity, Products.SimpleCallback callback)
        {
            string buyProductUrl = string.Format(
                "/games/{0}/products/{1}/purchase",
                Proba.Configuration.GameId,
                this.id
            );

            Request request = new Request(buyProductUrl);
            request
                .AddParam("quantity", quantity)
                .Post()
                .onFinish += (r) => {
                    BaseResponse res = JsonUtility.FromJson<BaseResponse>(r.Response);
                    callback(res);
                };
        }

    }
}
