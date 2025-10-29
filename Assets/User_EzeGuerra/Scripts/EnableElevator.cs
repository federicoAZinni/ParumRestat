using UnityEngine;

public class EnableElevator : MonoBehaviour, IInteractable //Eze: hice que este componente utilice IInteractable en vez de heredar de Item
{
    [SerializeField] BoxCollider _elevatorCollider;
    [SerializeField] GameObject _readyUI;
    public void Interact()
    {
        _elevatorCollider.isTrigger = true;
        _readyUI.SetActive(true);
    }
}
