using UnityEngine;

public class ButtonCloseGame : MonoBehaviour
{
    [SerializeField] private float _delay;
    
    public void CloseGame()
    {
        Invoke("DelayCloseGame", _delay);
    }

    private void DelayCloseGame()
    {
        Application.Quit();
    }
}
