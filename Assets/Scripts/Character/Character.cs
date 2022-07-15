using System.Collections;
using System.Collections.Generic;
using InputSystem;
using UnityEngine;

public class Character : MonoBehaviour
{
    private Controller m_controller;
    private Rigidbody2D m_rigidbody;
    [SerializeField] private Target m_target;
    [Header("Navigation")]
    [SerializeField] private float m_moveSpeed = 3f;

    void Awake()
    {
        m_controller = GetComponent<Controller>();
        m_rigidbody = GetComponent<Rigidbody2D>();
    }
    
    void Update()
    {
        m_target.direction = m_controller.targetDirection;
        m_rigidbody.velocity = m_controller.moveDirection * m_moveSpeed;
    }
}
