using Proba.Http;

namespace Proba.Serializables
{
    [System.Serializable]
    public class ProductCollection : BaseResponse
    {
        public Product[] products;
    }
}
