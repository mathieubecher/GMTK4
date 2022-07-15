using System.Collections;
using System.Collections.Generic;
using InputSystem;
using UnityEngine;

public class Character : MonoBehaviour
{
    private Controller m_controller;
    private Rigidbody2D m_rigidbody;
    
    [Header("Navigation")]
    [SerializeField] private float m_moveSpeed = 3f;

    [Header("Shoot")] 
    [SerializeField] private Target m_target;
    [SerializeField] private GameObject m_bullet;
    [SerializeField] private float m_offsetBulletAtSpawn = 0.6f;
    [SerializeField] private int m_magazineSize = 6;
    
    private int m_leftBullet;
    void OnEnable()
    {
        m_controller = GetComponent<Controller>();
        m_controller.OnShoot += Shoot;
    }
    void OnDisable()
    {
        m_controller.OnShoot -= Shoot;
    }
    void Awake()
    {
        m_controller = GetComponent<Controller>();
        m_rigidbody = GetComponent<Rigidbody2D>();
        m_leftBullet = m_magazineSize;
    }
    
    void Update()
    {
        m_target.direction = m_controller.targetDirection;
        m_rigidbody.velocity = m_controller.moveDirection * m_moveSpeed;
    }

    void Shoot()
    {
        if (m_leftBullet <= 0)
            return;
        
        --m_leftBullet;
        GameObject gameObjectRef = Instantiate(m_bullet);
        gameObjectRef.transform.position = transform.position + (Vector3)m_target.direction * m_offsetBulletAtSpawn;
        gameObjectRef.GetComponent<Bullet>().SetDirection(m_target.direction);
    }
}
