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

    [SerializeField] private List<SpawnerData> m_spawners;
    [SerializeField] private Door m_door;
    private int m_totalBeer;
    private int m_totalDrink = 0;
    
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
            if (m_totalDrink == spawner.nbDrink)
            {
                Instantiate(spawner.spawner, spawner.position.position, Quaternion.identity);
            }
        }
    }
    private void Exit()
    {
        Debug.Log("It's a lonely boooooy");
    }
}
