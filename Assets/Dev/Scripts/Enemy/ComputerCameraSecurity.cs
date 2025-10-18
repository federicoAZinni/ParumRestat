using UnityEngine;

public class ComputerCameraSecurity : Item
{
    EnemyAISecurityCamera[] securityCameras;
    private void Awake()
    {
        securityCameras = FindObjectsByType<EnemyAISecurityCamera>(FindObjectsSortMode.None);
    }

    public override void Interact()
    {
        if(securityCameras.Length <= 0) return; 

        foreach (EnemyAISecurityCamera camera in securityCameras)
        {
            camera.TurnOffCamera();
        }
    }
}
