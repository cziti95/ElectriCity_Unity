using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnOnCity : MonoBehaviour
{
    public GameObject smallTurnedOnCity;
    public GameObject bigTurnedOnCity;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (gameObject.tag == "bigCity")
        {
            if (collision.gameObject.tag == "polePlaceholder" && !GameManager.instance.isSmallPoleActive())
            {
                GameObject turnedOnCity = Instantiate(bigTurnedOnCity, transform.position, Quaternion.identity) as GameObject;
                GameManager.instance.activeBigCitys++;
            }
            else if (collision.gameObject.tag == "polePlaceholder" && GameManager.instance.isSmallPoleActive())
            {
                UiManager.instance.GameOver();
                UiManager.instance.SmallPolesOverloaded();
            }
        }

        if (gameObject.tag == "smallCity")
        {
            if (collision.gameObject.tag == "polePlaceholder")
            {
                GameObject turnedOnCity = Instantiate(smallTurnedOnCity, transform.position, Quaternion.identity) as GameObject;
                GameManager.instance.activeSmallCitys++;
            }
        }
    }
}
