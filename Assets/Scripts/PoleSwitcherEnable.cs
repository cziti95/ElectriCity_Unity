using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoleSwitcherEnable : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameManager.instance.EnablePoleSwitcher();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        UiManager.instance.DisableBothSwitcher();
    }
}
