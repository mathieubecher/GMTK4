using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eat : MonoBehaviour
{
    private Character m_character;
    private bool m_eat;

    public delegate void EatDelegate(Zombi _zombi);
    public static event EatDelegate OnEat;

    private void Update()
    {
        if (m_character)
        {
            if (m_character.Eat(this))
            {
                OnEat?.Invoke(GetComponent<Zombi>());
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
