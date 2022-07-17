using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnviroAudio : Hitable
{
    [SerializeField] private AudioEvent m_hitEvent;
    public override void Hit(Bullet _bullet)
    {
        GameObject emitter = new GameObject();
        emitter.transform.position = _bullet.transform.position;
        m_hitEvent.PlayOneShot(emitter);
    }
}
