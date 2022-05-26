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

    private EmailManager emailManager;

    public void InitButton(EmailData email, EmailManager manager)
    {
        emailManager = manager;

        myEmailData = email;
        senderName.text = email.sender;
    }

    public void DisplayEmail()
    {
        unreadMarker.SetActive(false);
        emailManager.DisplayEmail(myEmailData);
    }
}
