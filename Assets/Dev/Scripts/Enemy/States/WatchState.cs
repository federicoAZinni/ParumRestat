using System.Collections;
using Unity.Cinemachine;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class WatchState : MonoBehaviour, IState
{
    [SerializeField] EnemyAIPolice enemyAI;
    [SerializeField] Animator animator;
    [SerializeField] EnemyDetectionBar _enemyDetectionBar;
    [SerializeField] float _currentDetectionValue, _maxDetectionValue;
    [SerializeField] public bool _detecting;


    Coroutine tempCoroutine;

    public void OnFinish()
    {
        StopCoroutine(tempCoroutine);
        StartCoroutine(DownDetection());
    }


    public void OnStart()
    {
        animator.Play("Looking");
        if (tempCoroutine != null) StopCoroutine(tempCoroutine);
        tempCoroutine = StartCoroutine(UpDetection());
    }


    public void OnUpdate(){}


    private IEnumerator UpDetection()
    {
        while (_currentDetectionValue < 1)
        {
            _currentDetectionValue += Time.deltaTime/ _maxDetectionValue;
            _enemyDetectionBar.DecreaseBar(_currentDetectionValue);
            yield return null;
        }

      Catched();
    }

    private IEnumerator DownDetection()
    {
        while (0 <= _currentDetectionValue)
        {
            _currentDetectionValue -= Time.deltaTime/ _maxDetectionValue;
            _enemyDetectionBar.DecreaseBar(_currentDetectionValue);
            yield return null;
        }
    }

    private void Catched()
    {
        animator.SetTrigger("Pointing");
        enemyAI.enemiesManager.OnEnemyCatchPlayer(enemyAI);
    }
}
