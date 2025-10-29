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
        navMeshSurface.useGeometry = NavMeshCollectGeometry.PhysicsColliders;
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


    public void OnEnemyCatchPlayer(Enemy enemy, float _delay)
    {
        GameManager.instance.cameraController.OnCatchPlayer(enemy.transform);
        GameManager.instance.EnableFade(); //Eze: Activar el fade fuera del delay queda bien para la cámara, más no para el enemy

        LeanTween.delayedCall(gameObject, _delay, () => //Eze: Convertí este delay en parámetro para probar valores
        {
            GameManager.instance.OnLoseGame();
        });
    }
}
