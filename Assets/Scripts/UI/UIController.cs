using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] private List<Image> m_bullet; 
    [SerializeField] private List<Image> m_life; 
    [SerializeField] private float m_reloadDuration; 
    // Start is called before the first frame update
    private Character m_character;
    
    void OnEnable()
    {
        m_character = FindObjectOfType<Character>();
        m_character.OnShoot += Shoot;
        m_character.OnReload += Reload;
        Character.OnEat += Eat;
        Character.OnDead += Dead;
        
    }

    void OnDisable()
    {
        m_character.OnShoot -= Shoot;
        m_character.OnReload -= Reload;
        Character.OnEat -= Eat;
        Character.OnDead -= Dead;
    }

    private void Shoot(int _bullet)
    {
        for (int i = m_bullet.Count-1; i >= _bullet; --i)
        {
            m_bullet[i].enabled = false;
        }
    }
    
    private void Reload()
    {
        for (int i = 0; i < m_bullet.Count; ++i)
        {
            m_bullet[i].enabled = false;
        }

        StartCoroutine("ReloadAnim");
    }

    private IEnumerator ReloadAnim()
    {
        for (int i = 0; i < m_bullet.Count; ++i)
        {
            yield return new WaitForSeconds(m_reloadDuration/m_bullet.Count);
            m_bullet[i].enabled = true;
        }
    }
    private void Eat(int _life)
    {
        for (int i = m_life.Count-1; i >= _life; --i)
        {
            m_life[i].enabled = false;
        }
    }
    private void Dead()
    {
        for (int i = 0; i < m_life.Count; ++i)
        {
            m_life[i].enabled = false;
        }
    }
}
