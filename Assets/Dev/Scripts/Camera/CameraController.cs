using Unity.Cinemachine;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] CinemachineCamera playerCamera;
    [SerializeField] CinemachineCamera enemyCamera;
    [SerializeField] Animator animator;

    private void Awake()
    {
        if(animator==null) animator = GetComponent<Animator>();
    }
    private void Start()
    {
        playerCamera.Target = new CameraTarget { 
            TrackingTarget = GameManager.instance.playerMovement.transform, 
            LookAtTarget = GameManager.instance.playerMovement.transform };
    }

    public void OnCatchPlayer(Transform enemy)
    {
        enemyCamera.Target = new CameraTarget
        {
            TrackingTarget = enemy,
            LookAtTarget = enemy
        };

        animator.Play("EnemyCameraFinishEvent");
    }
}
