using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TweetUI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI title;
    [SerializeField]
    private TextMeshProUGUI body;

    public void Populate(string _title, string _body)
    {
        title.text = _title;
        body.text = _body;
    }
}
