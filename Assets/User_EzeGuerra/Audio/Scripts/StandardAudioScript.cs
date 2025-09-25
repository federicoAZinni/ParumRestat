using UnityEngine;

//agrega un AudioSource automáticamente al objeto
[RequireComponent(typeof(AudioSource)), ExecuteInEditMode]
public class StandardAudioScript : MonoBehaviour
{
    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = gameObject.GetComponent<AudioSource>();
    }

    public void PlaySound()
    {
        _audioSource.Play();
    }

    public void StopSound()
    {
        _audioSource.Stop();
    }

    public void SoundOneShot(AudioClip _clip)
    {
        //PlayOneShot() no se puede cortar/detener, pero se le puede pasar un clip como parámetro
        _audioSource.PlayOneShot(_clip);
    }
}