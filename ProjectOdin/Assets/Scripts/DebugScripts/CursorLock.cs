using UnityEngine;
using System.Collections;

public class CursorLock : MonoBehaviour {

	
	
	// Update is called once per frame
	void Update () {
	    if(Input.GetButtonDown("Lock"))
        {
            if (Cursor.lockState == CursorLockMode.Locked)
            {
                Cursor.lockState = CursorLockMode.None;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
            }
        }
	}
}
