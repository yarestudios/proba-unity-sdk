using UnityEngine;
using System.Collections;

namespace Proba.Serializables
{
    [System.Serializable]
    public class AchievementAwardedResponse : BaseResponse
    {
        public Achievement achievement;
    }
}
