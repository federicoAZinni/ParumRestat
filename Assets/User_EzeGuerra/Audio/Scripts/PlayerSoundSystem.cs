using UnityEngine;
using System;

public enum SoundType //tipos de sonidos que puede emitir el player
{
    FOOTSTEPS,
    ATTACK,
    DAMAGE,
    DEFEAT
}

public class PlayerSoundSystem : MonoBehaviour
{
    static PlayerSoundSystem _systemInstance;
    private AudioSource _audioSource;
    [SerializeField] private SoundsList[] _soundsList;

    void Awake()
    {
        _systemInstance = this;
        _audioSource = _systemInstance.GetComponent<AudioSource>();
    }

    public static void PlaySound(SoundType sound, float volume = 1) //método llamable desde cualquier script
    {
        AudioClip[] clips = _systemInstance._soundsList[(int)sound].Sounds;
        AudioClip randomClip = clips[UnityEngine.Random.Range(0, clips.Length)]; //elegir sonido random de la lista

        _systemInstance._audioSource.PlayOneShot(randomClip, volume);
    }

#if UNITY_EDITOR
    private void OnEnable()
    {
        string[] names = Enum.GetNames(typeof(SoundType)); //buscar cada elemento del enum
        Array.Resize(ref _soundsList, names.Length); //sumar cambios hechos en el enum al array del inspector

        for (int i = 0; i < _soundsList.Length; i++)
            _soundsList[i].name = names[i]; //setear nombres
    }
#endif

    [Serializable]
    public struct SoundsList //sonidos individuales de cada categoría
    {
        public AudioClip[] Sounds { get => sounds; } //el array original es privado
        [HideInInspector] public string name;
        [SerializeField] AudioClip[] sounds;
    }
}
