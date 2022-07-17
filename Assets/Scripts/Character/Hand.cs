using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    [SerializeField] private Character m_character;
    [SerializeField] private SpriteRenderer m_sprite;
    [SerializeField] private float m_distance = 0.6f;

    private Animator m_animator;
    void OnEnable()
    {
        m_character.OnShoot += Shoot;
        m_character.OnReload += Reload;
        m_character.OnDead += Dead;
        Door.OnDoorExit += Dead;
        m_animator = m_sprite.transform.parent.GetComponent<Animator>();
    }

    void OnDisable()
    {
        m_character.OnShoot -= Shoot;
        m_character.OnReload -= Reload;
        m_character.OnDead -= Dead;
        Door.OnDoorExit -= Dead;
    }

    void Dead()
    {
        gameObject.SetActive(false);
    }

    void Shoot(int _bullet)
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
        transform.position = (Vector2) m_character.transform.position + m_character.targetDir * m_distance; 
        
        m_sprite.transform.parent.transform.eulerAngles = new Vector3(0f,0f, Vector2.SignedAngle(Vector2.right, m_character.targetDir));
        m_sprite.flipY = m_character.targetDir.x < 0;
    }
}
