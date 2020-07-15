using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Proba.Http;

public class GiveAchievementButton : MonoBehaviour
{
    public int achievementId;

    public void Award()
    {
        string url = "http://phama.test/api/games/1/achievements/" + achievementId + "/award";

        Request request = new Request(url);
        request.Post();
    }

}
