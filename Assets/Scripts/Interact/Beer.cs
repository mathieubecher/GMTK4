using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Beer : Interact
{
    [SerializeField] private TextMeshPro m_tuto;
    
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
        Debug.Log("Interact");
    }
}
