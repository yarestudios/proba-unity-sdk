namespace Proba.Serializables
{
    [System.Serializable]
    public class AchievementCollection : BaseResponse
    {
        public Achievement[] achievements;
        public Achievement[] notAwarded;
    }

}
