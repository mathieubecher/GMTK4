using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableAudio : Hitable
{
    [SerializeField] private AudioEvent m_breakEvent;
    public override void Hit(Bullet _bullet)
    {
        
    }
}
