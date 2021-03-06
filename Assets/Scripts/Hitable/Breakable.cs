using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakable : Hitable
{
    [SerializeField] private SpriteRenderer m_renderer;
    [SerializeField] private Sprite m_break;
    [SerializeField] private AudioEvent m_breakSFX;

    public override void Hit(Bullet _bullet)
    {
        m_renderer.sprite = m_break;
        GetComponent<SpriteRenderer>().enabled = true;
        GetComponent<Collider2D>().enabled = false;
        m_breakSFX.PlayOneShot(gameObject);
    }
}
