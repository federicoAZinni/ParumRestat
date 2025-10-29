using UnityEngine;

public class ButtonEnableDisable : MonoBehaviour
{
    [SerializeField] private GameObject[] _enableObjects, _disableObjects;
    [SerializeField] private float _enableDelay, _disableDelay;

    public void EnableObjects()
    {
        Invoke("DelayEnableObjects", _enableDelay);
    }

    public void DisableObjects()
    {
        Invoke("DelayDisableObjects", _disableDelay);
    }
    private void DelayEnableObjects()
    {
        for (int i = 0; i < _enableObjects.Length; i++)
        {
            _enableObjects[i].gameObject.SetActive(true);
        }
    }

    private void DelayDisableObjects()
    {
        for(int i = 0;  i < _disableObjects.Length; i++)
        {
            _disableObjects[i].gameObject.SetActive(false);
        }
    }
}
