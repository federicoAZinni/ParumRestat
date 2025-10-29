using UnityEngine;
using UnityEngine.AI;

public class PhoneCallState : MonoBehaviour, IState
{
    [SerializeField] NavMeshAgent agent;
    [SerializeField] Animator anim;
    [SerializeField] float timeCall;
    [HideInInspector]public Transform targetPhone;
    [SerializeField] EnemyAIPolice enemyAI;

    private void Awake()
    {
        enemyAI = GetComponent<EnemyAIPolice>();
    }

    public void OnFinish()
    {
        agent.isStopped = true;
        anim.SetFloat("Speed", 0);
    }

    public void OnStart()
    {
        agent.isStopped = false;
        agent.SetDestination(targetPhone.position);
    }

    public void OnUpdate()
    {
        if(Vector3.Distance(transform.position, targetPhone.position) < 1) { return; }
        Animations();
        if (enemyAI.OnVisionCone()) enemyAI.ChangeState(TypeState.watchState);
        
    }

    void Animations()
    {
        anim.SetFloat("Speed", agent.velocity.magnitude);
    }
}
