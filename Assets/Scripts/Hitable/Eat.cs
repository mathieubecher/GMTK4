using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eat : MonoBehaviour
{
    [SerializeField] private Zombi m_zombi;
    private Character m_character;
    private bool m_eat;

    public delegate void EatDelegate(Zombi _zombi);
    public static event EatDelegate OnEat;


    private void Update()
    {
        if (m_character && !m_zombi.stop) 
        {
            if (m_character.Eat(this))
            {
                m_zombi.animator.SetTrigger("Attack");
                Vector2 dir = m_character.transform.position - m_zombi.transform.position;
                m_zombi.animator.SetFloat("hitDirX", dir.x);
                m_zombi.animator.SetFloat("hitDirY",  dir.y);
                OnEat?.Invoke(m_zombi);
            }
        }
    }

    public void OnTriggerEnter2D(Collider2D _other)
    {
        if (_other.TryGetComponent(out Character character))
        {
            m_character = character;
        }
    }
    public void OnTriggerExit2D(Collider2D _other)
    {
        if (_other.TryGetComponent(out Character character))
        {
            m_character = null;
        }
    }
}
