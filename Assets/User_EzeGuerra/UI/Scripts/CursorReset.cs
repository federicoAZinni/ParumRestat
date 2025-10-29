using UnityEngine;

public class CursorReset : MonoBehaviour
{
    private void Start() //desactivar cursor original
    {
    	Cursor.visible = false;
    }

    private void Update() //ligar cursor nuevo al mouse
    {
    	transform.position = Input.mousePosition;
    }
}
