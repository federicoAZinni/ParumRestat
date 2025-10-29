using UnityEngine;

public class ComputerScreenRandomizer : MonoBehaviour, IInteractable, ISound
{
    [SerializeField] Material[] _materials;
    [SerializeField] AudioClip[] _wrongClips;
    [SerializeField] AudioClip _rightClip;
    Renderer _screenRenderer;
    Material _storedMaterial;
    AudioSource _audioSource;
    [HideInInspector] public GameObject _barUSB;
    public bool _selected;
    bool _soundPlayed;

    void Awake()
    {
        _screenRenderer = GetComponent<Renderer>();
        _audioSource = GetComponent<AudioSource>();
    }

    public void StoreMaterial()
    {
        if (!_selected) //si la computadora no es la correcta, setear un fondo random
            _storedMaterial = _materials[Random.Range(1, _materials.Length)];
        else           //si la computadora es la correcta, setear el fondo correcto
            _storedMaterial = _materials[0]; 
    }
    
    public void Interact()
    {
        //cambiar material stock al randomizado + suena sonido según si es la compu correcta o no
        _screenRenderer.material = _storedMaterial;
        if(!_soundPlayed) PlaySound();

        if(_selected) _barUSB.SetActive(true);
    }

    public void PlaySound()
    {
        if (!_selected) _audioSource.PlayOneShot(_wrongClips[Random.Range(0, _wrongClips.Length)]);
        else _audioSource.PlayOneShot(_rightClip);
        _soundPlayed = true;
    }
}
