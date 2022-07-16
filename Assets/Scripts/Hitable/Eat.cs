using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eat : MonoBehaviour
{
    private Character m_character;
    private bool m_eat;


    private void Update()
    {
        if (m_character)
        {
            m_character.Eat(this);
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
