using System.Net.Sockets;
using UnityEngine;
using UnityEngine.UI;

public class EnemyDetectionBar : MonoBehaviour
{
    [SerializeField] private Image _detectionBar;
    [SerializeField] private float _currentFill;
    Camera cameraMain;

    private void Awake()
    {
        cameraMain = Camera.main;
    }

    public void DecreaseBar(float passedFill) //actualizar fill de la barra según tiempo pasado dentro del cono de visión del enemy
    {
        _currentFill = passedFill;
        _detectionBar.fillAmount = _currentFill;
    }

    private void Update()
    {
        transform.parent.LookAt(cameraMain.transform.position);
    }

    public void ResetBar() //resetear al salir del cono
    {
        _detectionBar.fillAmount = 0;
    }
}
