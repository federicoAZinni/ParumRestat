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
            if(collider.TryGetComponent<Item>(out Item itemPicked)) 
            {
                if (itemPicked.isOnHand) return;
                if(itemPicked.itemType == ItemType.Throwable)
                { 
                    AnimPickingITemThrowable(collider.transform);
                    itemPicked.Interact(); //Sumé esta linea para el sonido de las tazas -eze
                    return;
                }else if(itemPicked.itemType == ItemType.Important)
                {
                    AnimTouchItem(collider.transform);
                    itemPicked.Interact();
                    return;
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
