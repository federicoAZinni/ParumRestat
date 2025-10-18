using System;
using System.Collections;
using System.Data;
using UnityEngine;
using UnityEngine.Splines;

public class EnemyAISecurityCamera : Enemy
{
    [SerializeField] Transform refToMove;
    [SerializeField] Transform refCameraToMove;
    [SerializeField] float radius;
    [SerializeField] SplineAnimate splineAnimate;
    [SerializeField] float _currentDetectionValue, _maxDetectionValue;
    [SerializeField] EnemyDetectionBar _enemyDetectionBar;
    bool turnOff;

    enum StateCamera { onVision,outVision}
    StateCamera currentState = StateCamera.outVision;

    Coroutine currentCoroutine;

    private void Update()
    {
        refCameraToMove.LookAt(refToMove.position);
    }

    private void FixedUpdate()
    {
        if (turnOff) return;

        Collider[] objectsOnRange = Physics.OverlapSphere(refToMove.transform.position, radius);

        foreach (Collider collider in objectsOnRange)
        {
            if (collider.CompareTag("Player") )
            {
                Physics.Raycast(refCameraToMove.transform.position, (collider.transform.position- refCameraToMove.transform.position).normalized, out RaycastHit hit, 50);
                if(hit.transform.CompareTag("Player")) ChangeState(StateCamera.onVision);
            } 
            else
                ChangeState(StateCamera.outVision);
        }
    }


    void ChangeState(StateCamera state)
    {
        if (state == currentState) return;
        if(currentCoroutine!=null) StopCoroutine(currentCoroutine);
        currentState = state;

        switch (currentState)
        {
            case StateCamera.onVision:
                splineAnimate.Pause();
                currentCoroutine = StartCoroutine(UpDetection());
                break;
            case StateCamera.outVision:
                splineAnimate.Play();
                currentCoroutine = StartCoroutine(DownDetection());
                break;
            default:
                break;
        }
    }


    private IEnumerator UpDetection()
    {
        while (_currentDetectionValue < 1)
        {
            _currentDetectionValue += Time.deltaTime / _maxDetectionValue;
            _enemyDetectionBar.DecreaseBar(_currentDetectionValue);
            yield return null;
        }

        Catched();
    }

    private IEnumerator DownDetection()
    {
        while (0 <= _currentDetectionValue)
        {
            _currentDetectionValue -= Time.deltaTime / _maxDetectionValue;
            _enemyDetectionBar.DecreaseBar(_currentDetectionValue);
            yield return null;
        }
        _currentDetectionValue = 0;
    }

    void Catched() => enemiesManager.OnEnemyCatchPlayer(this);

    public void TurnOffCamera()
    {
        turnOff = true;
        refToMove.gameObject.SetActive(false);
        splineAnimate.Pause();
    }
    

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(refToMove.transform.position, radius);

    }
}
