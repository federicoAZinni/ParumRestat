using UnityEngine;

public class ButtonAudio : MonoBehaviour
{
    [SerializeField] private AudioSource[] _audioSources;

    public void HoverSound()
    {
        _audioSources[0].Play();
    }

    public void ClickSound()
    {
        _audioSources[1].Play();
    }
}
