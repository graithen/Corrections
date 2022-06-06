using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryManager : MonoBehaviour
{
    public enum Chapter
    {
        One,
        Two,
        Three,
        Four
    }
    [Header("Chapters")]
    public Chapter currentChapter = Chapter.One;

    public int chapterOneStart = 1;
    public int chapterTwoStart = 14;
    public int chapterThreeStart = 31;
    public int chapterFourStart = 45;
    public int finalGameDay = 60;

    [Header("Custom Components")]
    [SerializeField]
    private DateTimeSystem dateTimeSystem;
    [SerializeField]
    private GameplayTracking gameplayTracking;
    [SerializeField]
    private NewsManager newsManager;
    [SerializeField]
    private EmailManager emailManager;

    [Header("Stories")]
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
        else if (gameplayTracking.completedDays + 1 == chapterFourStart)
        {
            currentChapter = Chapter.Four;
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
        bool hasStory = storedStories.TryGetValue(gameplayTracking.completedDays + 1, out thisStory);

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

    public void CheckForStoryEmails()
    {
        StoryData thisStory;
        bool hasStory = storedStories.TryGetValue(gameplayTracking.completedDays + 1, out thisStory);

        if(hasStory)
        {
            SendEmailsToEmailManager(thisStory);
        }
    }

    private void SendEmailsToEmailManager(StoryData story)
    {
        if(story.emails.Count > 0)
        {
            for (int i = 0; i < story.emails.Count; i++)
            {
                emailManager.todaysEmails.Add(story.emails[i]);
            }
        }
    }
}
