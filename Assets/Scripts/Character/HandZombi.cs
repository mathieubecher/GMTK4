using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandZombi : MonoBehaviour
{
    [SerializeField] private ShooterZombi m_zombi;
    [SerializeField] private SpriteRenderer m_sprite;
    [SerializeField] private float m_distance = 0.6f;

    private Animator m_animator;
    
    void OnEnable()
    {
        m_zombi.OnShoot += Shoot;
        m_animator = m_sprite.transform.parent.GetComponent<Animator>();
    }

    void OnDisable()
    {
        m_zombi.OnShoot -= Shoot;
    }

    void Dead()
    {
        gameObject.SetActive(false);
    }

    void Shoot()
    {
        m_animator.SetTrigger("Shoot");
    }

    void Reload()
    {
        m_animator.SetTrigger("Reload");
        
    }
    // Update is called once per frame
    void Update()
    {
        transform.localPosition = (m_zombi.targetDir * m_distance); 
        
        m_sprite.transform.parent.transform.eulerAngles = new Vector3(0f,0f, Vector2.SignedAngle(Vector2.right, m_zombi.targetDir));
        m_sprite.flipY = m_zombi.targetDir.x < 0;
    }
}
