using UnityEngine;
using System.Linq;
using UnityEditor.Animations;

public class SoundEvent : MonoBehaviour
{
    AudioSource _audioSource;
    [SerializeField] AudioClip[] _clips;

    private void Awake()
    {
        _audioSource = gameObject.GetComponent<AudioSource>();
    }

    public void CheckClip()
    {
        //if(clip al que esta asignado este event == clip actual/ con mas peso dentro del blend tree)
            //PlaySoundOnFrame()
    }
    void PlaySoundOnFrame() //al blendearse las animaciones, el sonido se duplica/ continúa sonando
    {
        _audioSource.PlayOneShot(_clips[Random.Range(0, _clips.Length)]);
    }

}
