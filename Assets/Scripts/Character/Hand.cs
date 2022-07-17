using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    [SerializeField] private Character m_character;
    [SerializeField] private SpriteRenderer m_sprite;
    [SerializeField] private float m_distance = 0.6f;
    // Start is called before the first frame update
    
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
    // Update is called once per frame
    void Update()
    {
        transform.position = (Vector2) m_character.transform.position + m_character.targetDir * m_distance; 
        
        m_sprite.transform.eulerAngles = new Vector3(0f,0f, Vector2.SignedAngle(Vector2.right, m_character.targetDir));
    }
}
