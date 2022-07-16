using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] private List<Image> m_bullet; 
    [SerializeField] private float m_reloadDuration; 
    // Start is called before the first frame update
    private Character m_character;
    
    void OnEnable()
    {
        m_character = FindObjectOfType<Character>();
        m_character.OnShoot += Shoot;
        m_character.OnReload += Reload;
    }

    void OnDisable()
    {
        m_character.OnShoot -= Shoot;
        m_character.OnReload -= Reload;
    }

    private void Shoot(int _bullet)
    {
        for (int i = 5; i >= _bullet; --i)
        {
            m_bullet[i].enabled = false;
        }
    }
    
    private void Reload()
    {
        for (int i = 0; i < 6; ++i)
        {
            m_bullet[i].enabled = false;
        }

        StartCoroutine("ReloadAnim");
    }

    private IEnumerator ReloadAnim()
    {
        for (int i = 0; i < 6; ++i)
        {
            yield return new WaitForSeconds(m_reloadDuration/6f);
            m_bullet[i].enabled = true;
        }
    }
}
