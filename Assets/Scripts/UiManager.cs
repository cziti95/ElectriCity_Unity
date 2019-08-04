using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    public static UiManager instance;

    public GameObject infoPanel;
    public GameObject gameOverPanel;
    public GameObject tutorialPanel;

    public GameObject smallPoleActive;
    public GameObject bigPoleActive;

    public GameObject plusGenerator;

    public GameObject overloadedPowerplan;

    public GameObject smallSwitcher;
    public GameObject bigSwitcher;

    public GameObject smallPole;
    public GameObject bigPole;
    public GameObject overloadedSmallPole;

    public UiNumber maxEnergy;
    public UiNumber necEnergy;
    public UiNumber smallPoles;
    public UiNumber bigPoles;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    public void EnableSmallPoleSwitcher()
    {
        smallSwitcher.SetActive(true);
        bigSwitcher.SetActive(false);
    }

    public void EnableBigPoleSwitcher()
    {
        smallSwitcher.SetActive(false);
        bigSwitcher.SetActive(true);
    }

    public void DisableBothSwitcher()
    {
        smallSwitcher.SetActive(false);
        bigSwitcher.SetActive(false);
    }

    public void InitializeNewGenerator()
    {
        var generatorPlaceholder = GameObject.Find("PlusGeneratorPlaceholder");
        GameObject newGenerator = Instantiate(plusGenerator, generatorPlaceholder.transform.position, Quaternion.identity) as GameObject;

        generatorPlaceholder.transform.position = new Vector3(generatorPlaceholder.transform.position.x-0.7f,generatorPlaceholder.transform.position.y, generatorPlaceholder.transform.position.z); 
    }

    public void InitializOverloadedPowerPlan()
    {
        var powerplan = GameObject.Find("Powerplan");
        GameObject overloadedObject = Instantiate(overloadedPowerplan, powerplan.transform.position, Quaternion.identity) as GameObject;
        Destroy(powerplan);
    }

    public void ShowInfo()
    {
        infoPanel.SetActive(true);
    }

    public void GameOver()
    {
        gameOverPanel.SetActive(true);
    }

    public void EnableTutorial()
    {
        tutorialPanel.SetActive(true);
    }

    public void DisableTutorial()
    {
        tutorialPanel.SetActive(false);
    }

    public void SmallPolesOverloaded()
    {
        var allSmallPole = Resources.FindObjectsOfTypeAll<GameObject>().Where(obj => obj.name == "SmallElectricPole(Clone)");

        foreach (var pole in allSmallPole)
        {
            GameObject overloadedPole = Instantiate(overloadedSmallPole, pole.transform.position, Quaternion.identity) as GameObject;
            Destroy(pole);
        }
    }

    public void InitializeSmallPole(Vector3 position)
    {
        GameObject newGenerator = Instantiate(smallPole, position, Quaternion.identity) as GameObject;
    }

    public void InitializeBigPole(Vector3 position)
    {
        GameObject newGenerator = Instantiate(bigPole, position, Quaternion.identity) as GameObject;
    }
}
