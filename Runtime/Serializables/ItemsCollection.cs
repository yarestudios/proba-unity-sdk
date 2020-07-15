using UnityEngine;
using System.Collections;

namespace Proba.Serializables
{
    [System.Serializable]
    public class ItemsCollection : BaseResponse
    {
        public InventoryItem[] items;
    }
}
