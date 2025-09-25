using UnityEngine;

public class SoundEvent : MonoBehaviour
{
    AudioSource _audioSource;
    [SerializeField] AudioClip[] _clips;

    private void Awake() //automatizar referencia
    {
        _audioSource = gameObject.GetComponent<AudioSource>();
    }

    public void PlaySoundOnFrame()
    {
        int randInd = Random.Range(0, _clips.Length); //meter random dentro de la asignacion dsp
        Debug.Log(gameObject);

        _audioSource.PlayOneShot(_clips[randInd]);
    }

}
