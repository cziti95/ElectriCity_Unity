using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddPlusGenerator : MonoBehaviour
{
    private void OnMouseDown()
    {
        GameManager.instance.IncrementMaxEnergy();
    }
}
