using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class PatrolState : MonoBehaviour, IState
{
    [Header("Dependecies")]
    [SerializeField] NavMeshAgent agent;
    [SerializeField] Transform wayPointParent;
    [SerializeField] Animator anim;

    [Space(5)]
    [Header("Variables")]
    [SerializeField] float distanceDetectionWayPoint;
    [SerializeField] float timeToWaitOnWayPoint;


    Vector3[] wayPointPos;
    int currentWayPoint;
    Vector3 currentWayPointPos;

    Coroutine patrol;
    private void Awake()
    {
        int wayPointsCount = wayPointParent.childCount;
        wayPointPos = new Vector3[wayPointsCount];

        int i = 0;
        foreach (Transform t in wayPointParent)
        {
            wayPointPos[i] = t.position;
            i++;
        }

        wayPointParent.gameObject.SetActive(false);
    }


    public void OnFinish()
    {
        agent.isStopped = true;
        anim.SetFloat("Speed", 0);
        StopCoroutine(patrol);
    }

    public void OnStart()
    {
        agent.isStopped = false;
        ChangeToNextWayPoint();
        patrol = StartCoroutine(Patrol());
    }

    public void OnUpdate()
    {
        Animations();
    }

    IEnumerator Patrol()
    {
        while (true)
        {
            if (Vector3.Distance(transform.position, currentWayPointPos) < distanceDetectionWayPoint)
            {
                yield return new WaitForSeconds(timeToWaitOnWayPoint);
                
                ChangeToNextWayPoint();
            }
            yield return null;
        }
    }

    void ChangeToNextWayPoint()
    {
        currentWayPointPos = wayPointPos[currentWayPoint];
        agent.SetDestination(wayPointPos[currentWayPoint]);
        currentWayPoint++;
        if (currentWayPoint >= wayPointPos.Length) currentWayPoint = 0;
    }

    void Animations()
    {
        anim.SetFloat("Speed", agent.velocity.magnitude);
    }
}
