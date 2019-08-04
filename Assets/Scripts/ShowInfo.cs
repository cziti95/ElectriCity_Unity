using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowInfo : MonoBehaviour
{ 
    private void OnMouseDown()
    {
        UiManager.instance.ShowInfo();
    }
}
