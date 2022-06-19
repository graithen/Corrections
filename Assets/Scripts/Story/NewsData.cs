using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewNewsData", menuName = "Story/NewsData")]
public class NewsData : ScriptableObject
{
    [TextArea(2,5)]
    public string title;
    [TextArea(10,5)]
    public string body;

    [HideInInspector]
    public string publishTime;
}
