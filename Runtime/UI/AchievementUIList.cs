using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Proba;
using Proba.Serializables;

public class AchievementUIList : MonoBehaviour
{
    private ScrollRect scrollRect;
    public GameObject achievementElementPrefab;
    public string BaseUrl = "http://phama.test/api";


    protected void Start()
    {
        this.scrollRect = GetComponent<ScrollRect>();
        LoadAchievements();
    }

    void LoadAchievements()
    {
        Proba.Engine.Initialize(() => {});
        Proba.Achievements.List(DisplayAchievements);
    }

    void DisplayAchievements(AchievementCollection collection)
    {
        int count = 0;
        foreach (Achievement achievement in collection.achievements)
        {
            var element = Instantiate(this.achievementElementPrefab, scrollRect.content);
            RectTransform rectTransform = element.GetComponent<RectTransform>();
            rectTransform.Translate(Vector2.down * ((85 * count) + 10));

            AchievementUIElement item = element.GetComponent<AchievementUIElement>();
            item.title.text = achievement.title;
            item.description.text = achievement.description;

            count++;
        }
    }
}
