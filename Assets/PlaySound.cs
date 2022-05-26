using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySound : MonoBehaviour
{
    public AudioClip Audio;
    public AudioManager AudioManager;

    public void PlayAudio()
    {
        AudioManager.PlayAudio(Audio);
    }
}
