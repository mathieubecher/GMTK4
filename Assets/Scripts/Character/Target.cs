using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Target : MonoBehaviour
{
    [SerializeField] private Character m_character;
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
    void OnEnable()
    {
        m_character.OnDead += Dead;
    }

    void OnDisable()
    {
        m_character.OnDead -= Dead;
    }

    void Dead()
    {
        gameObject.SetActive(false);
    }
    void Update()
    {
        transform.position = m_character.transform.position + (Vector3)m_direction.normalized * m_distance;
    }
}
