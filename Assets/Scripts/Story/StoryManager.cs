using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryManager : MonoBehaviour
{
    public enum Chapter
    {
        One,
        Two,
        Three
    }
    public Chapter currentChapter = Chapter.One;

    private int chapterOneStart = 1;
    private int chapterTwoStart = 14;
    private int chapterThreeStart = 31;

    [SerializeField]
    private DateTimeSystem dateTimeSystem;
    [SerializeField]
    private GameplayTracking gameplayTracking;
    [SerializeField]
    private NewsManager newsManager;

    [SerializeField]
    private List<StoryData> allStories;
    private Dictionary<int, StoryData> storedStories = new Dictionary<int, StoryData>();

    private void Start()
    {
        InitStoryDictionary();
        UpdateChapter();
    }

    public void UpdateChapter()
    {
        if(gameplayTracking.completedDays + 1 == chapterOneStart)
        {
            currentChapter = Chapter.One;
        }
        else if (gameplayTracking.completedDays + 1 == chapterTwoStart)
        {
            currentChapter = Chapter.Two;
        }
        else if (gameplayTracking.completedDays + 1 == chapterThreeStart)
        {
            currentChapter = Chapter.Three;
        }
    }

    private void InitStoryDictionary()
    {
        for (int i = 0; i < allStories.Count; i++)
        {
            storedStories.Add(allStories[i].releaseDay, allStories[i]);
        }
    }

    public void CheckForStoryNews()
    {
        StoryData thisStory;
        bool hasStory = storedStories.TryGetValue(gameplayTracking.completedDays, out thisStory);

        if(hasStory)
        {
            SendNewsArticleToNewsManager(thisStory);
        }
    }

    private void SendNewsArticleToNewsManager(StoryData story)
    {
        //Add the news article to todays News
        if(story.news != null)
        {
            newsManager.finalNewsFeed.Add(dateTimeSystem.PickRandomTime(), story.news);
        }
    }
}
