using UnityEngine;
using UnityEngine.UI;

public class EnemyDetectionBar : MonoBehaviour
{
    [SerializeField] private Image _detectionBar;
    [SerializeField] private float _maxFill, _currentFill;

    public void DecreaseBar(float passedFill) //actualizar fill de la barra según tiempo pasado dentro del cono de visión del enemy
    {
        _currentFill = passedFill;
        _detectionBar.fillAmount = _currentFill / _maxFill; 
    }

    public void ResetBar() //resetear al salir del cono
    {
        _detectionBar.fillAmount = _maxFill;
    }
}
