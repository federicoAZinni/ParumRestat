using System;
using NUnit.Framework.Constraints;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAIPolice : Enemy
{
    IState currentState;
    
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


    private void Start()
    {
        idleState = GetComponent<IdleState>();
        patrolState = GetComponent<PatrolState>();
        watchState = GetComponent<WatchState>();
        phoneCallState = GetComponent<PhoneCallState>();

        currentState = idleState;
        ChangeState(TypeState.patrolState);
    }

    private void Update()
    {
        currentState.OnUpdate();

        Vector3 dir = (target.transform.position - transform.position);
        Debug.DrawRay(transform.position, dir * distanceVision);
    }

    public void ChangeState(TypeState newState)
    {
        IState tempState = GetStateBySteteType(newState);
        if (currentState == tempState) return;

        currentState.OnFinish();
        currentState = tempState;
        currentState.OnStart();
    }

    IState GetStateBySteteType(TypeState newState)
    {
        switch (newState)
        {
            case TypeState.idleState:
                return idleState;
            case TypeState.patrolState:
                return patrolState;
            case TypeState.watchState:
                return watchState;
            case TypeState.phoneCallState:
                return phoneCallState;
            default:
                return idleState;
        }
    }

    public bool OnVisionCone()
    {
        if (target == null) return false;
        if (Vector3.Distance(target.transform.position, transform.position) > distanceVision) return false;
        Vector3 dir = (target.transform.position - transform.position).normalized;
        if (Vector3.Dot(dir, transform.forward) < amplitudeVision) return false;
        if (Physics.Raycast(transform.position, dir, out RaycastHit hit))
            if (!hit.transform.CompareTag("Player")) return false;
        
        return true;
    }

    public void OnPhoneCalling(Transform targetPhone)
    {
        phoneCallState.targetPhone = targetPhone;
        ChangeState(TypeState.phoneCallState);    
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

public enum TypeState { idleState, patrolState, watchState, phoneCallState }

public class Enemy: MonoBehaviour
{
    public EnemiesManager enemiesManager;
    public GameObject target;
}

