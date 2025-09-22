using UnityEngine;

public class ButtonTester : MonoBehaviour
{
    public void ClickTester()
    {
        Debug.Log("button " + gameObject + " was pressed");
    }

    public void SelTester()
    {
        Debug.Log("button " + gameObject + " was selected");
    }
}
