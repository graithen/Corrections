using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EmailButton : MonoBehaviour
{
    public EmailData myEmailData;

    [Header("UI")]
    [SerializeField]
    private TextMeshProUGUI senderName;
    [SerializeField]
    private GameObject unreadMarker;

    public void InitButton(EmailData email)
    {
        myEmailData = email;
        senderName.text = email.sender;
    }

    public void DisplayEmail()
    {
        unreadMarker.SetActive(false);
        //Update EMail Display UI Here!
    }
}
