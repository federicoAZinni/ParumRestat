using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

public class EnemiesManager : MonoBehaviour
{
    [SerializeField] List<Enemy> currentEnemies;

    private void Awake()
    {
        InitNavMeshSurface();
    }

    private void InitNavMeshSurface()
    {
        NavMeshSurface navMeshSurface = new GameObject().AddComponent<NavMeshSurface>();
        navMeshSurface.layerMask = LayerMask.GetMask("Scene");
        navMeshSurface.BuildNavMesh();
    }

    private void Start()
    {
        Enemy[] temp = FindObjectsByType<Enemy>(FindObjectsInactive.Include, FindObjectsSortMode.None);

        foreach (Enemy enemy in temp)
        {
            enemy.enemiesManager = this;
            enemy.target = GameManager.instance.playerMovement.cameraRefToEnemy.gameObject;
            enemy.gameObject.SetActive(true);
            currentEnemies.Add(enemy);
        }    
    }


    public void OnEnemyCatchPlayer(Enemy enemy)
    {
        GameManager.instance.cameraController.OnCatchPlayer(enemy.transform);

        LeanTween.delayedCall(gameObject, 5, () => {
            GameManager.instance.RestartGame();
        });
    }

}
