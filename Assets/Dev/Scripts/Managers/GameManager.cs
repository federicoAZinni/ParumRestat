using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Dependencies")]
    public PlayerMovement playerMovement;
    public CameraController cameraController;
    public EnemiesManager enemiesManager;

    [Header("Objetos del canvas")]
    [SerializeField] private GameObject _fadeEffect;
    [SerializeField] private GameObject _cursor;
    [SerializeField] private GameObject _victoryScreen;
    [SerializeField] private GameObject _defeatScreen;

    private AudioSource _stockAudioSource;
    [Header("Tracks de audio")]
    [SerializeField] private AudioSource _victoryTrack;
    [SerializeField] private AudioSource _defeatTrack;
    private bool _trackSwitched;

    private void Awake()
    {
        if(instance == null) instance=this;
        else Destroy(gameObject);

        playerMovement = FindAnyObjectByType<PlayerMovement>();
        cameraController = FindAnyObjectByType<CameraController>();
        enemiesManager = FindAnyObjectByType<EnemiesManager>();

        _stockAudioSource = GetComponent<AudioSource>();
    }

    public void OnLoseGame() //Derrota - Eze: Moví el código original del SceneManager al botón de reintentar
    {
        if(!_trackSwitched)
        {
            _stockAudioSource.Stop();
            _defeatTrack.Play();

            _trackSwitched = true;
        }
        
        
        _cursor.SetActive(true);
        _defeatScreen.SetActive(true); 
    }

    public void OnWinGame() //Victoria
    {
        if(!_trackSwitched)
        {
            _stockAudioSource.Stop();
            _victoryTrack.Play();

            _trackSwitched = true;
        }

        _cursor.SetActive(true);
        _victoryScreen.SetActive(true);
    }

    public void EnableFade()
    {
        _fadeEffect.SetActive(true);
    }
}
