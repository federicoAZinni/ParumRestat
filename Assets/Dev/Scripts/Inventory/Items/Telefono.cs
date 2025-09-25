using UnityEngine;

public class Telefono : Item, ISound
{
    [SerializeField] EnemyAI enemyToAnswerPhone;
    [SerializeField] AudioSource _audioSource;

    public override void Interact()
    {
        if (enemyToAnswerPhone == null) enemyToAnswerPhone = FindAnyObjectByType<EnemyAI>();
        PlaySound();

        enemyToAnswerPhone.OnPhoneCalling(transform);
    }

    public void PlaySound()
    {
        _audioSource.Play();
    }
}
