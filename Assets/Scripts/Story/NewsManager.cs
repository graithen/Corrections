using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewsManager : MonoBehaviour
{
    public bool generateFluffNews;

    [Header("UI")]
    [SerializeField]
    private GameObject newsPrefab;
    [SerializeField]
    private GameObject fluffNewsPrefab;
    [SerializeField]
    private Transform newsContainer;
    [SerializeField]
    private Scrollbar verticalScroll;
    [SerializeField]
    private GameObject noNewsText;

    [Header("Custom Components")]
    [SerializeField]
    private DateTimeSystem dateTimeSystem;
    [SerializeField]
    private StoryManager storyManager;

    [Header("News Articles")]
    [SerializeField]
    private int minArticlesPerDay;
    [SerializeField]
    private int maxArticlesPerDay;

    private List<NewsData> allNewsData;
    [SerializeField]
    private List<NewsData> chapterOneFluffNews;
    [SerializeField]
    private List<NewsData> chapterTwoFluffNews;
    [SerializeField]
    private List<NewsData> chapterThreeFluffNews;
    [SerializeField]
    private List<NewsData> chapterFourFluffNews;

    private List<NewsData> todaysNewsData = new List<NewsData>();
    public Dictionary<string, NewsData> finalNewsFeed;


    private void OnEnable()
    {
        UpdateNoNewsText();
    }

    public void ConstructDailyFeed() // Called at Start of Day (DayTime NewDayEvent)
    {
        finalNewsFeed = new Dictionary<string, NewsData>();
        finalNewsFeed.Clear();
        todaysNewsData.Clear();

        if(generateFluffNews)
        {
            int numberOfArticles = Random.Range(minArticlesPerDay, maxArticlesPerDay);

            if (storyManager.currentChapter == StoryManager.Chapter.One)
            {
                allNewsData = chapterOneFluffNews;
            }
            else if (storyManager.currentChapter == StoryManager.Chapter.Two)
            {
                allNewsData = chapterTwoFluffNews;
            }
            else if (storyManager.currentChapter == StoryManager.Chapter.Three)
            {
                allNewsData = chapterThreeFluffNews;
            }
            else if (storyManager.currentChapter == StoryManager.Chapter.Four)
            {
                allNewsData = chapterFourFluffNews;
            }

            if (allNewsData.Count < numberOfArticles)
            {
                Debug.Log("NOT ENOUGH ARTICLES IN LIST!");
                return;
            }
            else
            {
                for (int i = 0; i < numberOfArticles; i++)
                {
                    int x = Random.Range(1, allNewsData.Count);
                    if (!todaysNewsData.Contains(allNewsData[x]))
                    {
                        todaysNewsData.Add(allNewsData[x]);
                        allNewsData.Remove(allNewsData[x]); //Uncomment to make it so news doesn't repeat on other days
                    }
                    else
                    {
                        continue;
                    }
                }

                foreach (var newsArticle in todaysNewsData)
                {
                    finalNewsFeed.Add(dateTimeSystem.PickRandomTime(), newsArticle);
                }
            }
        }

        storyManager.CheckForStoryNews();
    }

    public void PublishFeed()
    {
        if(finalNewsFeed != null)
        {
            NewsData thisArticle;
            bool hasNews = finalNewsFeed.TryGetValue(dateTimeSystem.TimeGet, out thisArticle);

            if (hasNews)
            {
                if(string.IsNullOrEmpty(thisArticle.body))
                {
                    GameObject newsTweet = Instantiate(fluffNewsPrefab, newsContainer);
                    newsTweet.GetComponent<TweetUI>().Populate(thisArticle.title, thisArticle.body);
                    newsContainer.GetComponent<RectTransform>().sizeDelta += new Vector2(0, fluffNewsPrefab.GetComponent<RectTransform>().sizeDelta.y + 16);
                }
                else
                {
                    GameObject newsTweet = Instantiate(newsPrefab, newsContainer);
                    newsTweet.GetComponent<TweetUI>().Populate(thisArticle.title, thisArticle.body);
                    newsContainer.GetComponent<RectTransform>().sizeDelta += new Vector2(0, newsPrefab.GetComponent<RectTransform>().sizeDelta.y + 16);
                }

                verticalScroll.value = 0;
            }
        }

        UpdateNoNewsText();
    }

    private void UpdateNoNewsText()
    {
        if (newsContainer.childCount > 0)
        {
            noNewsText.SetActive(false);
        }
        else
        {
            noNewsText.SetActive(true);
        }
    }
}
