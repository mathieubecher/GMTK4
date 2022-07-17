using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimDoor : MonoBehaviour
{
    public Sprite m_doorOpen;
    
    public delegate void DoorOpen(AnimDoor _animDoor);
    public static event DoorOpen OnDoorOpen;
    public void Open()
    {
        OnDoorOpen?.Invoke(this);
        GetComponent<SpriteRenderer>().sprite = m_doorOpen;
    }
}
