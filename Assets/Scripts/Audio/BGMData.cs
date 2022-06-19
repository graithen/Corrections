using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewBGMData", menuName = "AudioData/BGMData")]
public class BGMData : ScriptableObject
{
    public AudioClip alienClip;
    public AudioClip bassClip;
    public AudioClip kickClip;
    public AudioClip pianoClip;
    public AudioClip celloClip;
}
