using UnityEngine;

public class ElevatorTrigger : MonoBehaviour
{
    [SerializeField] GameObject _readyUI;
    void OnTriggerEnter(Collider c)
    {
        if (c.gameObject.CompareTag("Player"))
        {
            _readyUI.SetActive(false);
            GameManager.instance.EnableFade();
            GameManager.instance.Invoke("OnWinGame", 3);
        }
    }
}
