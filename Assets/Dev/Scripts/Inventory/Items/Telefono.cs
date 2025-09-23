using UnityEngine;

public class Telefono : Item,IInteractable
{
    [SerializeField] EnemyAI enemyToAnswerPhone;

    public override void Interact()
    {
        if(enemyToAnswerPhone == null) enemyToAnswerPhone = FindAnyObjectByType<EnemyAI>();

        enemyToAnswerPhone.OnPhoneCalling(transform);
    }
}
