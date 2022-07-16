using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Door m_door;
    private int m_totalBeer;
    
    void OnEnable()
    {
        Beer.OnBeerEmpty += BeerEmpty;
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
    private void Exit()
    {
        Debug.Log("It's a lonely boooooy");
    }
}
