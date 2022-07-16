using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : Interact
{
    [SerializeField] private bool m_active = false;

    public bool active
    {
        set
        {
            m_active = value;
            if(m_active) OnDoorOpen?.Invoke();
        }
        get => m_active;
    }

    public delegate void DoorOpen();
    public static event DoorOpen OnDoorOpen;
    
    public delegate void DoorExit();
    public static event DoorExit OnDoorExit;
    public override void EnterZone(InteractSystem _interactSystem)
    {
        if (active)
        {
            OnDoorExit?.Invoke();
            IEnumerator coroutine = _interactSystem.OnInteract(m_duration, m_velocity, m_stopCharacter);
            StartCoroutine(coroutine);
        }
    }
    public override void InteractAction(InteractSystem _interactSystem)
    {
    }
}
