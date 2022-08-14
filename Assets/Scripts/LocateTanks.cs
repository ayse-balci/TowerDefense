using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class LocateTanks : MonoBehaviour
{
    public GameObject tankPrefab;
    public List<Location> tankLocations;
    private int locationCount;
    public int leftTankCount;
    private GameManager _gameManager;
    
    void Awake()
    {
        _gameManager = FindObjectOfType<GameManager>();
        tankLocations = new List<Location>();
        locationCount = transform.childCount;
        for (int i = 0; i < locationCount; i++)
        {
            tankLocations.Add( new Location(transform.GetChild(i), false));
        }
    }

    public void CreateTankAtRandomPosition()
    {
        if (leftTankCount <= 0)
        {
            Debug.Log("You don't have tanks");
            return;
        }
        else
        {
            int x = Random.Range(0, locationCount);

            if (tankLocations[x].GetIsFull())
            {
                CreateTankAtRandomPosition();
            }

            Instantiate(tankPrefab, new Vector3(tankLocations[x].GetTransform().position.x, tankLocations[x].GetTransform().position.y, -1f), Quaternion.identity);
            tankLocations[x].SetIsFull(true);
            leftTankCount--;
            _gameManager.UpdateLeftTankCountText();
        }
    }

    public void CreateTanksAtContinue(int[] arr)
    {
        foreach (var x in arr)
        {
            if (x != 0)
            {
                Instantiate(tankPrefab, new Vector3(tankLocations[x].GetTransform().position.x, tankLocations[x].GetTransform().position.y, -1f), Quaternion.identity);
                tankLocations[x].SetIsFull(true);
                leftTankCount--;
                _gameManager.UpdateLeftTankCountText();
            }
        }
    }

    public void IncreaseLeftTankCount()
    {
        leftTankCount++;
        _gameManager.UpdateLeftTankCountText();
    }
    
    public void SetLeftTankCount(int count)
    {
        leftTankCount = count;
        _gameManager.UpdateLeftTankCountText();
    }
    
    public int  GetLeftTankCount()
    {
        return leftTankCount;
    }
}


