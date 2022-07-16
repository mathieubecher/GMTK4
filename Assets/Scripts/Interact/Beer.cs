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
        
        base.InteractAction(_interactSystem);
        --m_nbUse;
        m_nbUse = Math.Max(0, m_nbUse);
        for (int i = m_liquid.Count - 1; i >= m_nbUse; --i)
        {
            m_liquid[i].enabled = false;
        }

    }
}
