using UnityEngine;
using System;

public enum TipoSonido //Tipos de sonidos que podría haber en el juego
{
    PASOS,
    INTERACCION
}

//agrega un AudioSource automáticamente al objeto
[RequireComponent(typeof(AudioSource)), ExecuteInEditMode]

public class PlayerSoundSystem : MonoBehaviour
{
    static PlayerSoundSystem _instanciaManager; 
    private AudioSource _audioSource;

    [Tooltip("Para agregar tipos de sonidos, abrir el script y sumarlos al enum 'TipoSonido' ")]
    [SerializeField] private ListaSonidos[] _listaSonidos;

    //automatizar referencias a ambos componentes
    void Awake()
    {
        _instanciaManager = this;
        _audioSource = _instanciaManager.GetComponent<AudioSource>();
    }

    //método llamable desde cualquier script
    public static void PlaySonido(TipoSonido sonido, float volume = 1)
    {
        AudioClip[] clips = _instanciaManager._listaSonidos[(int)sonido].Sonidos;
        AudioClip randomClip = clips[UnityEngine.Random.Range(0, clips.Length)]; //elegir sonido random de la lista

        _instanciaManager._audioSource.PlayOneShot(randomClip, volume);
    }

#if UNITY_EDITOR
    private void OnEnable()
    {
        string[] nombres = Enum.GetNames(typeof(TipoSonido)); //buscar cada elemento del enum
        Array.Resize(ref _listaSonidos, nombres.Length); //sumar cambios hechos en el enum al array del inspector

        for (int i = 0; i < _listaSonidos.Length; i++)
            _listaSonidos[i].nombre = nombres[i]; //setear nombres del array a los del enum
    }
#endif

    [Serializable] 
    public struct ListaSonidos //sonidos individuales de cada categoría
    {
        public AudioClip[] Sonidos {get => sonidos;} //el array original es privado
        [HideInInspector] public string nombre;
        [SerializeField] private AudioClip[] sonidos;
    }
}
