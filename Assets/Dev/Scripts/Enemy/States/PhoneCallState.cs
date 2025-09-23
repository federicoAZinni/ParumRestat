using UnityEngine;
using UnityEngine.AI;

public class PhoneCallState : MonoBehaviour, IState
{
    [SerializeField] NavMeshAgent agent;
    [SerializeField] Animator anim;
    [SerializeField] float timeCall;
    [HideInInspector]public Transform targetPhone;

    public void OnFinish()
    {

    }

    public void OnStart()
    {
        agent.isStopped = false;
        agent.SetDestination(targetPhone.position);
    }

    public void OnUpdate()
    {
        Animations();
    }

    void Animations()
    {
        anim.SetFloat("Speed", agent.velocity.magnitude);
    }
}
