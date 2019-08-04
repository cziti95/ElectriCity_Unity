using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CitySpawner : MonoBehaviour
{
    public GameObject smallCityPrefab;
    public GameObject bigCityPrefab;

    public float maxX;
    public float maxY;
    public float minY;

    void Awake()
    {
        SpawnCitys();
    }

    void SpawnCitys()
    {
        int numberOfCitys = Random.Range(8, 16);
        for (int i = 0; i < numberOfCitys; i++)
        {
            float randomX = Random.Range(-maxX, maxX);
            float randomY = Random.Range(minY, maxY);

            int cityType = Random.Range(0, 3);

            Vector3 position = new Vector3(randomX, randomY, 0);

            GameObject newCity = Instantiate(cityType == 0 ? bigCityPrefab : smallCityPrefab, position, Quaternion.identity) as GameObject;
        }
    }

}
