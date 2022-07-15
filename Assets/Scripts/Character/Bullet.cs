using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Bullet : MonoBehaviour
{
    private Rigidbody2D m_rigidbody;
    [FormerlySerializedAs("speed")] [SerializeField] private float m_speed = 10.0f;

    private void Awake()
    {
        m_rigidbody = GetComponent<Rigidbody2D>();
    }

    public void SetDirection(Vector2 direction)
    {
        m_rigidbody.velocity = direction * m_speed;
    }
}
