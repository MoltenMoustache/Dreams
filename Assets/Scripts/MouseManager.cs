using UnityEngine;

#pragma warning disable 0618

public class MouseManager : MonoBehaviour {

	void Start ()
    {
        Cursor.visible = false;
        Screen.lockCursor = true;
	}
    
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Q))
            EnableMouse();

        if (Input.GetMouseButton(0))
            DisableMouse();
    }

    public void EnableMouse()
    {
        Cursor.visible = true;
        Screen.lockCursor = false;
    }

    public void DisableMouse()
    {
        Cursor.visible = false;
        Screen.lockCursor = true;
    }
}