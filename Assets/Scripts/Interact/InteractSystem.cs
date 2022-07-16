using System;
using System.Collections;
using System.Collections.Generic;
using InputSystem;
using UnityEngine;

public class InteractSystem : MonoBehaviour
{
    private Interact m_interact;
    private bool m_hasInteract;
    private Controller m_controller;

    void OnEnable()
    {
        m_controller = GetComponent<Controller>();
        m_controller.OnInteract += Interact;
    }

    void OnDisable()
    {
        m_controller.OnInteract -= Interact;
    }

    public void OnTriggerEnter2D(Collider2D _other)
    {
        if (_other.TryGetComponent(out Interact interact))
        {
            if (m_hasInteract && m_interact == interact) return;
            
            if(m_hasInteract) m_interact.ExitZone(this);
            
            m_hasInteract = true;
            m_interact = interact;
            m_interact.EnterZone(this);
        }
    }
    public void OnTriggerExit2D(Collider2D _other)
    {
        if (_other.TryGetComponent(out Interact interact))
        {
            if (m_hasInteract && m_interact == interact)
            {
                m_hasInteract = false;
                m_interact.ExitZone(this);
                m_interact = null;
            }
        }
    }

    private void Interact()
    {
        if (m_hasInteract)
        {
            m_interact.InteractAction(this);
        }
    }
}
