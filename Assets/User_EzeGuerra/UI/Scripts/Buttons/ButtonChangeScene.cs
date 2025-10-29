using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonChangeScene : MonoBehaviour
{
    [SerializeField] private float _delay;
    [SerializeField] private string _sceneName;

    public void ChangeScene()
    {
        Invoke("ChangeSceneDelay", _delay);
    }

    private void ChangeSceneDelay()
    {
        SceneManager.LoadScene(_sceneName);
    }
}
