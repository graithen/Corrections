using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    [SerializeField]
    private AudioMixer masterMixer;
    [SerializeField]
    private AudioMixerSnapshot normalSnapshot;
    [SerializeField]
    private AudioMixerSnapshot transitionSnapshot;

    [Header("UI")]
    [SerializeField]
    private AudioSource uiAudioSource;

    [Header("BGM")]
    [SerializeField]
    private AudioSource alienSource;
    [SerializeField]
    private AudioSource bassSource;
    [SerializeField]
    private AudioSource kickSource;
    [SerializeField]
    private AudioSource pianoSource;
    [SerializeField]
    private AudioSource celloSource;
    [SerializeField]
    private float transitionTime = 0.1f;

    public BGMData chapterOne;
    public BGMData chapterTwo;
    public BGMData chapterThree;
    public BGMData chapterFour;

    private StoryManager.Chapter lastChapter;

    private void Start()
    {
        SetBGMAudioClips(chapterOne);
        StartCoroutine(PlayBGMSources());
    }

    public void PlayUIAudio(AudioClip Audio)
    {
        //Debug.Log("Playing audio!");
        uiAudioSource.PlayOneShot(Audio);
    }

    public void PlayBGM(StoryManager.Chapter currentChapter)
    {
        if(lastChapter != currentChapter)
        {
            switch(currentChapter)
            {
                case StoryManager.Chapter.One:
                    transitionSnapshot.TransitionTo(transitionTime);
                    SetBGMAudioClips(chapterOne);
                    break;
                case StoryManager.Chapter.Two:
                    transitionSnapshot.TransitionTo(transitionTime);
                    SetBGMAudioClips(chapterTwo);
                    break;
                case StoryManager.Chapter.Three:
                    transitionSnapshot.TransitionTo(transitionTime);
                    SetBGMAudioClips(chapterThree);
                    break;
                case StoryManager.Chapter.Four:
                    transitionSnapshot.TransitionTo(transitionTime);
                    SetBGMAudioClips(chapterFour);
                    break;
            }

            StartCoroutine(PlayBGMSources());
            lastChapter = currentChapter;
        }
    }

    private void SetBGMAudioClips(BGMData data)
    {
        alienSource.clip = data.alienClip;
        bassSource.clip = data.bassClip;
        kickSource.clip = data.kickClip;
        pianoSource.clip = data.pianoClip;
        celloSource.clip = data.celloClip;
    }

    private IEnumerator PlayBGMSources()
    {
        alienSource.Play();
        bassSource.Play();
        kickSource.Play();
        pianoSource.Play();
        celloSource.Play();

        yield return new WaitForSeconds(transitionTime);

        normalSnapshot.TransitionTo(transitionTime);
    }
}
