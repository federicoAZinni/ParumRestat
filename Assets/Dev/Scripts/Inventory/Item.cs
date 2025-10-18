using UnityEngine;

public class Item : MonoBehaviour, IInteractable
{
    public ItemType itemType;
    public bool isOnHand;
    public virtual void Interact()
    {
        
    }
}

public enum ItemType
{
    Throwable,Important
}
