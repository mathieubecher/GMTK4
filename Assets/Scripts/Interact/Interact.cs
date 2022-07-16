using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact : MonoBehaviour
{
    [SerializeField] protected float m_duration;
    [SerializeField] protected bool m_stopCharacter;
    [SerializeField] protected Vector2 m_velocity;
    public virtual void EnterZone(InteractSystem _interactSystem)
    {
        
    }
    public virtual void ExitZone(InteractSystem _interactSystem)
    {
        
    }
    
    public virtual void InteractAction(InteractSystem _interactSystem)
    {
        Debug.Log("Interact");
        IEnumerator coroutine = _interactSystem.OnInteract(m_duration, m_velocity, m_stopCharacter);
        StartCoroutine(coroutine);
    }
}
