using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class LocateTanks : MonoBehaviour
{
    public GameObject tankPrefab;
    public List<Location> tankLocations;
    private int locationCount;
    void Awake()
    {
        tankLocations = new List<Location>();
        locationCount = transform.childCount;
        for (int i = 0; i < locationCount; i++)
        {
            tankLocations.Add( new Location(transform.GetChild(i), false));
        }
    }

    public void CreateTankAtRandomPosition()
    {
        int x = Random.Range(0, locationCount);

        if (!tankLocations[x].GetIsFull())
        {
            Instantiate(tankPrefab, new Vector3(tankLocations[x].GetTransform().position.x, tankLocations[x].GetTransform().position.y, -1f), Quaternion.identity);
            tankLocations[x].SetIsFull(true);
        }
    }
}


