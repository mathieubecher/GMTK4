using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombi : Hitable
{
    public float m_hitSpeed = 1f;
    public float m_hitDuration = 1f;
    
    private Rigidbody2D m_rigidbody;
    [HideInInspector] public bool stop;

    private void Awake()
    {
        m_rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (stop)
        {
            return;
        }
        
        m_rigidbody.velocity = Vector2.zero;
    }

    public override void Hit(Bullet _bullet)
    {
        if (stop) return;
        
        Vector3 direction = transform.position - _bullet.transform.position;
        direction.Normalize();
        IEnumerator coroutine = Stun(direction);
        StartCoroutine(coroutine);
    }

    private IEnumerator Stun(Vector3 _direction)
    {
        stop = true;
        m_rigidbody.velocity = _direction * m_hitSpeed;
        yield return new WaitForSeconds(m_hitDuration);
        stop = false;
    }
}
