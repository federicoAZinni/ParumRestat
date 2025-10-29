using UnityEngine;

public class ComputerManager : MonoBehaviour
{
    [SerializeField] GameObject _UIbar;
    [SerializeField] ComputerScreenRandomizer[] _computerScreens; //pendiente: automatizar con FindObjectsByType dsp de debug
    ComputerScreenRandomizer _correctComputer;

    void Start()
    {
        _computerScreens = FindObjectsByType<ComputerScreenRandomizer>(FindObjectsSortMode.None);
        
        //seleccionar una computadora random de las que hay en la escena
        int randomScreen = Random.Range(0, _computerScreens.Length);

        //señalar la computadora como la indicada
        _correctComputer = _computerScreens[randomScreen];
        _correctComputer._selected = true;
        Debug.Log(_correctComputer); //para testeo

        foreach (ComputerScreenRandomizer computer in _computerScreens)
        {
            computer.StoreMaterial();
            computer._barUSB = _UIbar;
        }
    }

    void Update() //para testeo
    {
        /*if(Input.GetKeyDown(KeyCode.L))
        {
            foreach (ComputerScreenRandomizer computer in _computerScreens)
            {
                computer.Interact();
            }
        }*/
    }
}
