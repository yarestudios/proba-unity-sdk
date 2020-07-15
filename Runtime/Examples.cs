using UnityEngine;
using Proba.Http;

public class Examples : MonoBehaviour
{
    private string BaseUrl = "http://phama.test/api";

    void Start()
    {
        // WinAchievementExample();
        // GetAchievementsExample();
        // BetterWinAchievementExample();
        PurchaseMoreCredits();
    }

    protected void WinAchievementExample()
    {
        Request request = new Request(this.BaseUrl + "/games/1/achievements/1/award");
        request.AddParam("name", "enrique")
               .Post();
    }

    void GetAchievementsExample()
    {
        Request request = new Request(this.BaseUrl + "/games/1/achievements");
        request.Get();
    }

    void BetterWinAchievementExample()
    {
        Proba.Engine.Initialize(() => {});
        Proba.Achievements.Award("gameplay.done2", (achievement) => {
            // AchievementAwarded.Open(achievement);
        });
    }

    void PurchaseMoreCredits()
    {
        Proba.Engine.Initialize(() => {});
        string transactionId = "123123";
        string receipt = "{\"trasactionId\": \"123123\"}";
        Proba.Purchases.Credits(100, transactionId, receipt);
    }

}
