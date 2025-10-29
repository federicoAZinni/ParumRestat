using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonReloadScene : MonoBehaviour
{
    [SerializeField] private float _delay;

    public void ReloadScene()
    {
        Invoke("ReloadSceneDelay", _delay);
    }

    private void ReloadSceneDelay()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
