using UnityEngine;

public class ComputerCameraSecurity : MonoBehaviour, IInteractable, ISound
{
    EnemyAISecurityCamera[] securityCameras;
    AudioSource _audioSource;

    private void Awake()
    {
        securityCameras = FindObjectsByType<EnemyAISecurityCamera>(FindObjectsSortMode.None);
        _audioSource = GetComponent<AudioSource>();
    }

    public void Interact()
    {
        if (securityCameras.Length <= 0) return;

        foreach (EnemyAISecurityCamera camera in securityCameras)
        {
            camera.TurnOffCamera();
        }
        Debug.Log("a");

        PlaySound();
    }
    
    public void PlaySound() //Eze: Sonido de desactivación de las cámaras
    {
        _audioSource.Play();
    }
}
