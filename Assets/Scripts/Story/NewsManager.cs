using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewsManager : MonoBehaviour
{
    [SerializeField]
    private GameObject newsPrefab;
    [SerializeField]
    private Transform newsContainer;
    [SerializeField]
    private Scrollbar verticalScroll;

    [SerializeField]
    private int minArticlesPerDay;
    [SerializeField]
    private int maxArticlesPerDay;

    [SerializeField]
    private DateTimeSystem dateTimeSystem;

    [SerializeField]
    private List<NewsData> allNewsData;
    private List<NewsData> todaysNewsData = new List<NewsData>();
    private Dictionary<string, NewsData> finalNewsFeed;


    public void ConstructDailyFeed() // Called at Start of Day (DayTime NewDayEvent)
    {
        finalNewsFeed = new Dictionary<string, NewsData>();
        finalNewsFeed.Clear();
        todaysNewsData.Clear();

        int numberOfArticles = Random.Range(minArticlesPerDay, maxArticlesPerDay);
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
            }

            foreach (var newsArticle in todaysNewsData)
            {
                finalNewsFeed.Add(dateTimeSystem.PickRandomTime(), newsArticle);
            }
        }   
    }

    public void PublishFeed()
    {
        if(finalNewsFeed != null)
        {
            NewsData thisArticle;
            bool hasNews = finalNewsFeed.TryGetValue(dateTimeSystem.TimeGet, out thisArticle);

            if (hasNews)
            {
                GameObject newsTweet = Instantiate(newsPrefab, newsContainer);
                newsTweet.GetComponent<TweetUI>().Populate(thisArticle.title, thisArticle.body);
                newsContainer.GetComponent<RectTransform>().sizeDelta += new Vector2(0, newsPrefab.GetComponent<RectTransform>().sizeDelta.y + 32);
                verticalScroll.value = 0;
            }
        } 
    }
}
