using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Serializable]
    private struct SpawnerData
    {
        public int nbDrink;
        public GameObject spawner;
        public Transform position;
    }
    [Serializable]
    private struct OpenDoorData
    {
        public int nbDrink;
        public AnimDoor door;
    }

    [SerializeField] private List<SpawnerData> m_spawners;
    [SerializeField] private List<OpenDoorData> m_doors;
    [SerializeField] private Door m_door;
    public int m_totalBeer;
    public int m_totalDrink = 0;
    
    void OnEnable()
    {
        Beer.OnBeerEmpty += BeerEmpty;
        Beer.OnBeerRelease += BeerRelease;
        m_totalBeer = FindObjectsOfType<Beer>().Length;

        Door.OnDoorExit += Exit;
    }

    void OnDisable()
    {
        Beer.OnBeerEmpty -= BeerEmpty;
        
    }

    private void BeerEmpty(Beer _beer)
    {
        --m_totalBeer;
        if (m_totalBeer <= 0)
            m_door.active = true;
    }
    private void BeerRelease(Beer _beer, bool _success)
    {
        if (!_success) return;
        ++m_totalDrink;
        foreach (SpawnerData spawner in m_spawners)
        {
            if (m_totalDrink == spawner.nbDrink && spawner.spawner && spawner.position )
            {
                Instantiate(spawner.spawner, spawner.position.position, Quaternion.identity);
            }
        }
        foreach (OpenDoorData door in m_doors)
        {
            if (m_totalDrink == door.nbDrink && door.door)
            {
                door.door.Open();
            }
        }
    }
    private void Exit()
    {
        Debug.Log("It's a lonely boooooy");
    }
}
