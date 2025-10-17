using UnityEngine;

public class Telefono : Item, ISound
{
    [SerializeField] EnemyAIPolice enemyToAnswerPhone;
    [SerializeField] AudioSource _audioSource;

    public override void Interact()
    {
        if (enemyToAnswerPhone == null) enemyToAnswerPhone = FindAnyObjectByType<EnemyAIPolice>();
        PlaySound();

        enemyToAnswerPhone.OnPhoneCalling(transform);
    }

    public void PlaySound()
    {
        _audioSource.Play();
    }
}
