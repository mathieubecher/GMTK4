using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact : MonoBehaviour
{
    [SerializeField] private float m_duration;
    [SerializeField] private bool m_stopCharacter;
    public virtual void EnterZone(InteractSystem _interactSystem)
    {
        
    }
    public virtual void ExitZone(InteractSystem _interactSystem)
    {
        
    }
    
    public virtual void InteractAction(InteractSystem _interactSystem)
    {
        Debug.Log("Interact");
        IEnumerator coroutine = _interactSystem.OnInteract(m_duration, m_stopCharacter);
        StartCoroutine(coroutine);
    }
}
