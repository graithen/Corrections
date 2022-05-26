using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewEmailData", menuName = "Story/EmailData")]
public class EmailData : ScriptableObject
{
    [TextArea(2, 5)]
    public string sender;
    [TextArea(2,5)]
    public string subject;
    [TextArea(10,5)]
    public string body;

    public int sendDay;
    public int sendHour;
    public int sendMin;
}
