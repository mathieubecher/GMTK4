using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Target : MonoBehaviour
{
    [SerializeField] private Transform m_character;
    [SerializeField] private float m_distance = 10;
    private Vector2 m_direction = Vector2.down;
    public Vector2 direction
    {
        get { return m_direction; }
        set { m_direction = value; }
    }
    void Start()
    {
        
    }

    void Update()
    {
        transform.position = m_character.position + (Vector3)m_direction.normalized * m_distance;
    }
}
