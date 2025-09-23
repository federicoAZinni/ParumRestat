using System;
using System.Net.Sockets;
using Unity.IO.LowLevel.Unsafe;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.Rendering.HableCurve;

public class EnemyAI : MonoBehaviour
{
    IState currentState;
    [SerializeField] GameObject target;

    [Space(5)]
    [Header("States")]
    IdleState idleState;
    PatrolState patrolState;
    WatchState watchState;
    PhoneCallState phoneCallState;

    [Space(5)]
    [Header("Vision Parameters")]
    [SerializeField] float distanceVision;
    [SerializeField] float amplitudeVision;

    bool onVision;
    
    private void Start()
    {
        idleState = GetComponent<IdleState>();
        patrolState = GetComponent<PatrolState>();
        watchState = GetComponent<WatchState>();
        phoneCallState = GetComponent<PhoneCallState>();

        currentState = idleState;
        ChangeState(patrolState);
    }

    private void Update()
    {
        currentState.OnUpdate();

        if (onVision != OnVisionCone())
        {
            ChangeState(watchState);
        }
        onVision = OnVisionCone();

        Vector3 dir = (target.transform.position - transform.position);
        Debug.DrawRay(transform.position, dir * distanceVision);

    }

    void ChangeState(IState newState)
    {
        if (currentState == newState) return;

        currentState.OnFinish();
        currentState = newState;
        currentState.OnStart();
    }

    bool OnVisionCone()
    {
        if (target == null) return false;
        if (Vector3.Distance(target.transform.position, transform.position) > distanceVision) return false;
        Vector3 dir = (target.transform.position - transform.position).normalized;
        if (Vector3.Dot(dir, transform.forward) < amplitudeVision) return false;
        if (Physics.Raycast(transform.position, dir, out RaycastHit hit))
            if (hit.transform != target.transform) return false;
        
        return true;

    }

    public void OnPhoneCalling(Transform targetPhone)
    {
        phoneCallState.targetPhone = targetPhone;
        ChangeState(phoneCallState);    
    }

    private void OnDrawGizmos()
    {
        //Cone
        float angulo = Mathf.Acos(amplitudeVision) * Mathf.Rad2Deg;

        Quaternion rotIzq = Quaternion.AngleAxis(-angulo, Vector3.up);
        Quaternion rotDer = Quaternion.AngleAxis(angulo, Vector3.up);

        Vector3 bordeIzq = rotIzq * transform.forward;
        Vector3 bordeDer = rotDer * transform.forward;

        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, transform.position + bordeIzq * distanceVision);
        Gizmos.DrawLine(transform.position, transform.position + bordeDer * distanceVision);
        //Cone

    }

}

