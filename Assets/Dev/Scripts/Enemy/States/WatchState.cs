using Unity.Cinemachine;
using UnityEngine;

public class WatchState : MonoBehaviour, IState
{
    [SerializeField] CinemachineCamera cam;
    [SerializeField] Animator animCinemachine;
    [SerializeField] Animator animator;
    public void OnFinish()
    {
       
    }

    public void OnStart()
    {
        cam.Target = new CameraTarget { TrackingTarget = transform, LookAtTarget = transform };
        animCinemachine.Play("EnemyCameraFinishEvent");
        animator.SetTrigger("Pointing");
    }

    public void OnUpdate()
    {
       
    }
}
