using UnityEngine;

public class Item : MonoBehaviour, IInteractable
{
    public ItemType itemType;

    public virtual void Interact()
    {
        
    }
}

public enum ItemType
{
    Throwable,Important
}
