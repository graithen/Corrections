using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private AudioSource m_AudioSource;

    private void Start()
    {
        m_AudioSource = GetComponent<AudioSource>();
    }

    public void PlayAudio(AudioClip Audio)
    {
        Debug.Log("Playing audio!");
        m_AudioSource.PlayOneShot(Audio);
    }
}
