using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unselect : MonoBehaviour
{ 

    // Update is called once per frame
    void Update()
    {
        UnSelectButton();
    }

    private void UnSelectButton()
    {
        // This is hacky
        GameObject myEventSystem = GameObject.Find("EventSystem");
        myEventSystem.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(null);
    }
}
