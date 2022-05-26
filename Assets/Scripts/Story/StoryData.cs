using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class StoryData
{
    public int releaseDay; //x+n ... (x = day started, n = days elapsed)
    public NewsData news;
    public List<EmailData> emails;
}
