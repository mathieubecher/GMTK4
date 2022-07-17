using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Beer : Interact
{
    [SerializeField] private TextMeshPro m_tuto;
    [SerializeField] private List<SpriteRenderer> m_liquid;
    [SerializeField] private int m_nbUse = 4; 
    
    public delegate void TakeBeer(Beer _beer);
    public static event TakeBeer OnTakeBeer;
    
    public delegate void BeerRelease(Beer _beer, bool _success);
    public static event BeerRelease OnBeerRelease;
    
    public delegate void BeerEmpty(Beer _beer);
    public static event BeerEmpty OnBeerEmpty;
    
    public override void EnterZone(InteractSystem _interactSystem)
    {
        m_tuto.enabled = true;
    }
    public override void ExitZone(InteractSystem _interactSystem)
    {
        m_tuto.enabled = false;
    }

    public override void InteractAction(InteractSystem _interactSystem)
    {
        if (m_nbUse <= 0) return;
        OnTakeBeer?.Invoke(this);
        base.InteractAction(_interactSystem);
    }

    public override void Success()
    {
        OnBeerRelease?.Invoke(this, true);
        
        --m_nbUse;
        if (m_nbUse <= 0)
        {
            m_nbUse = 0;
            OnBeerEmpty?.Invoke(this);
        }
        for (int i = m_liquid.Count - 1; i >= m_nbUse; --i)
        {
            m_liquid[i].enabled = false;
        }
    }

    public override void Fail()
    {
        OnBeerRelease?.Invoke(this, false);
    }
}
