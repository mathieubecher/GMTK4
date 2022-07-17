using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Bullet : MonoBehaviour
{
    private Rigidbody2D m_rigidbody;
    [FormerlySerializedAs("speed")] [SerializeField] private float m_speed = 10.0f;
    public float speed { get; set; }
    [HideInInspector] public ShooterZombi origin;
    
    public delegate void HitObjectDelegate(Bullet _bullet, Hitable _hit);
    public static event HitObjectDelegate OnHitObject;
    private void Awake()
    {
        m_rigidbody = GetComponent<Rigidbody2D>();
    }

    public void SetDirection(Vector2 direction)
    {
        m_rigidbody.velocity = direction * m_speed;
    }

    public void OnTriggerEnter2D(Collider2D _other)
    {
        if (_other.TryGetComponent(out Bullet bullet))
        {
            if (bullet.origin == origin) return;
        }
        
        if (_other.TryGetComponent(out Hitable hit))
        {
            if (hit == origin) return;
                
            OnHitObject?.Invoke(this, hit);
            hit.Hit(this);
        }
        Destroy(gameObject);
    }
}
