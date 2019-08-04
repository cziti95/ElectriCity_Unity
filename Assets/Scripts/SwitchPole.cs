using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchPole : MonoBehaviour
{
    private void OnMouseDown()
    {
        GameManager.instance.SwitchPoleType();
    }
}
