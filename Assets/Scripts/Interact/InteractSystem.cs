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
        if (m_character.life <= 0) return;
        if (m_hasInteract && m_canInteract)
        {
            m_interact.InteractAction(this);
        }
    }

    public IEnumerator OnInteract(Interact _interact, float _duration, Vector2 _velocity, bool _stopCharacter)
    {
        m_canInteract = false;
        if(_stopCharacter) m_character.StopActor( _velocity);
        
        yield return new WaitForSeconds(_duration);

        if (m_character.stop)
        {
            m_canInteract = true;
            if(_stopCharacter) m_character.RestartActor();
            _interact.Success();
        }
        
    }
}
