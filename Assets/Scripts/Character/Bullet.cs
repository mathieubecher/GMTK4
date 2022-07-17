using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Bullet : MonoBehaviour
{
    public enum ContactType
    {
        Character,
        Zombi,
        Wall,
        Breakable
    }
    
    private Rigidbody2D m_rigidbody;
    [FormerlySerializedAs("speed")] [SerializeField] private float m_speed = 10.0f;
    public float speed { get; set; }
    [HideInInspector] public ShooterZombi origin;
    
    public delegate void HitObjectDelegate(Bullet _bullet, GameObject _hit, ContactType _contactType);
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
        if (_other.TryGetComponent(out Character character))
        {
            if (origin)
            {
                character.Eat();
                OnHitObject?.Invoke(this, _other.gameObject, ContactType.Character);
            }
            else return;
        }
        else if (_other.TryGetComponent(out Bullet bullet))
        {
            if (bullet.origin == origin) return;
        }
        else if (_other.TryGetComponent(out Hitable hit))
        {
            if (hit == origin) return;
                
            OnHitObject?.Invoke(this,  _other.gameObject, hit is Zombi || hit is ShooterZombi ? ContactType.Zombi : ContactType.Breakable);
            hit.Hit(this);
        }
        else
        {
            OnHitObject?.Invoke(this, _other.gameObject,  ContactType.Wall);
        }
        Destroy(gameObject);
    }
}
