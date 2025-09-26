using Unity.Cinemachine;
using UnityEngine;

public class WatchState : MonoBehaviour, IState
{
    [SerializeField] CinemachineCamera cam;
    [SerializeField] Animator animCinemachine;
    [SerializeField] Animator animator;

    [SerializeField] EnemyDetectionBar _enemyDetectionBar;
    [SerializeField] float _currentDetectionValue, _maxDetectionValue;
    [SerializeField] public bool _detecting;
    bool _lockAnimation;

    public void OnFinish()
    {
        _currentDetectionValue = 0; //resetear valor de detección al pasar a otro estado
    }

    public void OnStart()
    {
        InvokeRepeating("UpdateDetection", 0, 1f);

        /*cam.Target = new CameraTarget { TrackingTarget = transform, LookAtTarget = transform };
        animCinemachine.Play("EnemyCameraFinishEvent");
        animator.SetTrigger("Pointing");*/
    }

    public void OnUpdate()
    {
        if (!_lockAnimation && _currentDetectionValue == _maxDetectionValue) //si se acabó el valor máximo de detección, poner animación
            DetectionAnimation();

        if (!_detecting) //PENDIENTE: resolver retorno al estado de patrol, capaz todo esto deberia estar en OnFinish()
        {
            _enemyDetectionBar.ResetBar();
            CancelInvoke("UpdateDetection");
        }
    }

    private void UpdateDetection()
    {
        _currentDetectionValue += 1;
        _enemyDetectionBar.DecreaseBar(_currentDetectionValue);
        //Debug.Log(_currentDetectionValue);
    }

    private void DetectionAnimation()
    {
        cam.Target = new CameraTarget { TrackingTarget = transform, LookAtTarget = transform };
        animCinemachine.Play("EnemyCameraFinishEvent");
        animator.SetTrigger("Pointing");

        _lockAnimation = true; //llamar animación una sola vez
    }
}
