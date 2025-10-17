using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Dependecies")]
    public PlayerMovement playerMovement;
    public CameraController cameraController;
    public EnemiesManager enemiesManager;


    private void Awake()
    {
        if(instance == null) instance=this;
        else Destroy(gameObject);

        playerMovement = FindAnyObjectByType<PlayerMovement>();
        cameraController = FindAnyObjectByType<CameraController>();
        enemiesManager = FindAnyObjectByType<EnemiesManager>();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
