using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private const int defaultGeneratorEnergy = 700;
    private const int plusGeneratorEnergy = 300;
    private const int smallCityEnergyLevel = 100;
    private const int bigCityEnergyLevel = 230;
    private const int poleMultiplierForEachCity = 10;

    private int currentMaxEnergy;
    private int necessaryEnergy;
    private int currentlyUsedEnergy;

    private int numberOfSmallPoles;
    private int numberOfBigPoles;

    private int maximumPlusGenerator;

    private bool isSmallPole;

    public int activeBigCitys { get; set; }
    public int activeSmallCitys { get; set; }

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    private void Start()
    {
        isSmallPole = true;
        maximumPlusGenerator = 5;
        currentMaxEnergy = defaultGeneratorEnergy;
        necessaryEnergy = 0;
        currentlyUsedEnergy = 0;
        activeBigCitys = 0;
        activeSmallCitys = 0;

        FirstRunTutorial();
    }

    void Update()
    {
        if (necessaryEnergy == 0)
        {
            SetNecessaryEnergy();
            SetPolesNumber();
        }
        
        currentlyUsedEnergy = activeBigCitys * bigCityEnergyLevel + activeSmallCitys * smallCityEnergyLevel;

        if (currentlyUsedEnergy > currentMaxEnergy)
        {
            UiManager.instance.InitializOverloadedPowerPlan();
            UiManager.instance.GameOver();
        }

        if ((numberOfBigPoles == 0 && activeBigCitys != GetTheAmountOfDifferentCityTypes()[1]) ||
                (numberOfSmallPoles == 0 && activeSmallCitys != GetTheAmountOfDifferentCityTypes()[0]))
        {
            UiManager.instance.GameOver();
        }

        if (GetTheAmountOfDifferentCityTypes()[1] == 0 && GetTheAmountOfDifferentCityTypes()[0] == 0)
        {
            SceneManager.LoadScene("GameScene");
        }

        UiManager.instance.maxEnergy.number = currentMaxEnergy;
        UiManager.instance.smallPoles.number = numberOfSmallPoles;
        UiManager.instance.bigPoles.number = numberOfBigPoles;

    }

    private int[] GetTheAmountOfDifferentCityTypes()
    {
        var smallObjects = Resources.FindObjectsOfTypeAll<GameObject>().Where(obj => obj.name == "SmallCity(Clone)");
        var bigObjects = Resources.FindObjectsOfTypeAll<GameObject>().Where(obj => obj.name == "BigCity(Clone)");

        return new int[] { smallObjects.Count(), bigObjects.Count() };
    }

    private void SetNecessaryEnergy()
    {
        necessaryEnergy += (GetTheAmountOfDifferentCityTypes()[0] * smallCityEnergyLevel);
        necessaryEnergy += (GetTheAmountOfDifferentCityTypes()[1] * bigCityEnergyLevel);

        UiManager.instance.necEnergy.number = necessaryEnergy;
    }

    private void SetPolesNumber()
    {
        numberOfSmallPoles = (GetTheAmountOfDifferentCityTypes()[0] * poleMultiplierForEachCity);
        numberOfBigPoles = (GetTheAmountOfDifferentCityTypes()[1] * poleMultiplierForEachCity);
    }

    public void IncrementMaxEnergy()
    {
        if (maximumPlusGenerator > 0)
        {
            maximumPlusGenerator--;
            currentMaxEnergy += plusGeneratorEnergy;
            UiManager.instance.InitializeNewGenerator();
        }
    }

    public void SwitchPoleType()
    {
        if (isSmallPole)
        {
            if (numberOfBigPoles > 0)
            {
                UiManager.instance.EnableBigPoleSwitcher();
                isSmallPole = !isSmallPole;
            }
        }
        else
        {
            if (numberOfSmallPoles > 0)
            {
                UiManager.instance.EnableSmallPoleSwitcher();
                isSmallPole = !isSmallPole;
            }
        }
    }

    public void EnablePoleSwitcher()
    {
        if (isSmallPole)
            UiManager.instance.EnableSmallPoleSwitcher();
        else
            UiManager.instance.EnableBigPoleSwitcher();
    }

    public bool isSmallPoleActive()
    {
        return isSmallPole;
    }

    public void NextPole(string direction)
    {
        var polePlaceholder = GameObject.Find("PolePlaceholder");

        if (validMove(direction))
        {
            switch (direction)
            {
                case "Right":
                    polePlaceholder.transform.position = new Vector3(isSmallPole ? polePlaceholder.transform.position.x + 0.45f : polePlaceholder.transform.position.x + 0.65f, polePlaceholder.transform.position.y, polePlaceholder.transform.position.z);
                    break;
                case "Left":
                    polePlaceholder.transform.position = new Vector3(isSmallPole ? polePlaceholder.transform.position.x - 0.45f : polePlaceholder.transform.position.x - 0.65f, polePlaceholder.transform.position.y, polePlaceholder.transform.position.z);
                    break;
                case "Up":
                    polePlaceholder.transform.position = new Vector3(polePlaceholder.transform.position.x, isSmallPole ? polePlaceholder.transform.position.y + 0.65f : polePlaceholder.transform.position.y + 0.8f, polePlaceholder.transform.position.z);
                    break;
                case "Down":
                    polePlaceholder.transform.position = new Vector3(polePlaceholder.transform.position.x, isSmallPole ? polePlaceholder.transform.position.y - 0.65f : polePlaceholder.transform.position.y - 0.8f, polePlaceholder.transform.position.z);
                    break;
            }

            if (isSmallPole)
            {
                UiManager.instance.InitializeSmallPole(polePlaceholder.transform.position);
                numberOfSmallPoles--;
            }
            else
            {
                UiManager.instance.InitializeBigPole(polePlaceholder.transform.position);
                numberOfBigPoles--;
            }
        }
    }

    private bool validMove(string direction)
    {
        var polePlaceholder = GameObject.Find("PolePlaceholder");

        switch (direction)
        {
            case "Right":
                if (polePlaceholder.transform.position.x + 0.65 < 2.5)
                    return true;
                else return false;
            case "Left":
                if (polePlaceholder.transform.position.x - 0.65 > -2.5)
                    return true;
                else return false;
            case "Up":
                if (polePlaceholder.transform.position.y + 0.8 < 3.5)
                    return true;
                else return false;
            case "Down":
                if (polePlaceholder.transform.position.y - 0.8 > -4.6)
                    return true;
                else return false;
        }

        return true;
    }

    private void FirstRunTutorial()
    {
        if (!PlayerPrefs.HasKey("FirstRun"))
        {
            UiManager.instance.EnableTutorial();
            PlayerPrefs.SetInt("FirstRun", 0);
        }
    }
}
