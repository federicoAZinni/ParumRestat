using UnityEngine;
using UnityEngine.Animations.Rigging;

public class PickItemAnimRig : MonoBehaviour
{
    [SerializeField] Rig rigArm;
    [SerializeField] Transform target;
    [SerializeField] Transform hand;
    [SerializeField] BoxCollider refCollider;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) PickItem();
    }


    void PickItem()
    {
        Collider[] objectsOnRange = Physics.OverlapBox(refCollider.transform.position, refCollider.size/2,refCollider.transform.rotation); 

        foreach (Collider collider in objectsOnRange)
        {
            if (collider.TryGetComponent<IInteractable>(out IInteractable itemPicked))
            {
                if(itemPicked.GetType() == typeof(Item))
                {
                    Item item =  (Item)itemPicked;
                    if (item.isOnHand) return;
                    if (item.itemType == ItemType.Throwable)
                    {
                        AnimPickingITemThrowable(collider.transform);
                        item.Interact();
                        //return; Eze: Returns comentados para permitir múltiples Interact() en una sola interacción
                    }
                    else if (item.itemType == ItemType.Important)
                    {
                        AnimTouchItem(collider.transform);
                        item.Interact();
                        //return;
                    }
                }else
                {
                    itemPicked.Interact();
                    AnimTouchItem(collider.transform); //Eze: animation fix?
                }
            }
        }
    }

    void AnimPickingITemThrowable(Transform posItem)
    {
        target.transform.position = posItem.position;
        LeanTween.value(gameObject, 0, 1, 0.3f).setOnUpdate((value) => { rigArm.weight = value; }).setEaseInCirc().setOnComplete(() => {

            posItem.SetParent(hand);
            posItem.localPosition = Vector3.zero;
            LeanTween.value(gameObject, 1, 0, 0.3f).setOnUpdate((value) => { rigArm.weight = value; }).setEaseInCirc();
        });

    }

    void AnimTouchItem(Transform posItem)
    {
        target.transform.position = posItem.position;
        LeanTween.value(gameObject, 0, 1, 0.3f).setOnUpdate((value) => { rigArm.weight = value; }).setEaseInCirc().setOnComplete(() => {

            LeanTween.value(gameObject, 1, 0, 0.3f).setOnUpdate((value) => { rigArm.weight = value; }).setEaseInCirc();
        });

    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(refCollider.transform.position, refCollider.size);
        Gizmos.matrix = refCollider.transform.localToWorldMatrix;
      
    }

}
