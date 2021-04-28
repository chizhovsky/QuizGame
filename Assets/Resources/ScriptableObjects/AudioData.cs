using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu( fileName = "AudioData", menuName = "Audio Data", order = 1 )]
public class AudioData : ScriptableObject
{
    [System.Serializable]
    public class NetworkAudio
    {
        public string clipName;
        public AudioClip clip;
    }

    public NetworkAudio buttonPressed;
    public NetworkAudio assPressed;
    public NetworkAudio mainMenuMusic;
}
