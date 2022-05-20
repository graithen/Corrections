using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewsManager : MonoBehaviour
{
    [SerializeField]
    private GameObject newsPrefab;
    [SerializeField]
    private Transform newsContainer;

    [SerializeField]
    private int minArticlesPerDay;
    [SerializeField]
    private int maxArticlesPerDay;

    [SerializeField]
    private DateTimeSystem dateTimeSystem;

    [SerializeField]
    private List<NewsData> allNewsData;
    private List<NewsData> todaysNewsData = new List<NewsData>();
    private Dictionary<string, NewsData> finalNewsFeed = new Dictionary<string, NewsData>();

    private void Update()
    {
        PublishFeed();
    }

    public void ConstructDailyFeed() // Called at Start of Day (DayTime NewDayEvent)
    {
        todaysNewsData.Clear();

        int numberOfArticles = Random.Range(minArticlesPerDay, maxArticlesPerDay);     
        for(int i = 0; i < numberOfArticles; i++)
        {
            int x = Random.Range(1, allNewsData.Count);
            todaysNewsData.Add(allNewsData[x]);
        }

        foreach (var newsArticle in todaysNewsData)
        {
            finalNewsFeed.Add(dateTimeSystem.PickRandomTime(), newsArticle);
        }
    }

    private void PublishFeed()
    {
        NewsData thisArticle;
        bool hasNews = finalNewsFeed.TryGetValue(dateTimeSystem.TimeGet, out thisArticle);

        if(hasNews)
        {
            GameObject newsTweet = Instantiate(newsPrefab, newsContainer);
            newsTweet.GetComponent<TweetUI>().Populate(thisArticle.title, thisArticle.body);
        }
    }
}
