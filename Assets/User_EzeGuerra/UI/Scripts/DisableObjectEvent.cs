using UnityEngine;

public class DisableObjectEvent : MonoBehaviour
{
    public void Disable()
    {
        Invoke("DelayDisable", 1f);
    }
    
    void DelayDisable()
    {
        gameObject.SetActive(false);
    }
}
