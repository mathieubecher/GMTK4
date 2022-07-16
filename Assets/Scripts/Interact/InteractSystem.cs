using System;
using System.Collections;
using System.Collections.Generic;
using InputSystem;
using UnityEngine;

public class InteractSystem : MonoBehaviour
{
    private Character m_character;
    
    private Interact m_interact;
    private bool m_hasInteract;

    private bool m_canInteract = true;
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

    private void Awake()
    {
        m_character = GetComponent<Character>();
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
        if (m_hasInteract && m_canInteract)
        {
            m_interact.InteractAction(this);
        }
    }

    public IEnumerator OnInteract(float _duration, bool _stopCharacter)
    {
        m_canInteract = false;
        
        m_character.stop |= _stopCharacter;
        
        yield return new WaitForSeconds(_duration);
        m_canInteract = true;
        m_character.stop = false;
    }
}
