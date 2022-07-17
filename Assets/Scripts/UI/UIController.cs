using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    [SerializeField] private List<Image> m_bullet; 
    [SerializeField] private List<Image> m_life; 
    [SerializeField] private float m_reloadDuration;
    [SerializeField] private int beerMax;
    [SerializeField] private int drink;
    private GameManager m_manager;
    // Start is called before the first frame update
    private Character m_character;

    public TMPro.TMP_Text Text_Counter;
    public TMPro.TMP_Text Text_BigText;
    public TMPro.TMP_Text Text_Smalltext;
    public TMPro.TMP_Text Text_BottomText;

    public GameObject EndScreen;

    public string Defeat_Bigtext;
    public string Defeat_Smalltext;
    public string Defeat_Bottomtext;

    public string Victory_Bigtext;
    public string Victory_Smalltext;
    public string Victory_Bottomtext;




    void OnEnable()
    {
        m_character = FindObjectOfType<Character>();
        m_manager = FindObjectOfType<GameManager>();
        m_character.OnShoot += Shoot;
        m_character.OnReload += Reload;
        m_character.OnDamaged += Damaged;
        m_character.OnDead += Dead;
        Door.OnDoorExit += Win;

        
    }

    private void Start()
    {
        
        beerMax = (m_manager.m_totalBeer * 4);
    }

    private void Update()
    {
        drink = m_manager.m_totalDrink;
        if (drink == beerMax)
        {

            Text_Counter.text = "GO TO THE EXIT!";
        }
        else
        {
            Text_Counter.text = drink + " / " + beerMax;
        }
        
        
    }


    void OnDisable()
    {
        m_character.OnShoot -= Shoot;
        m_character.OnReload -= Reload;
        m_character.OnDamaged -= Damaged;
        m_character.OnDead -= Dead;
        Door.OnDoorExit -= Win;
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
    private void Damaged(int _life)
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

        Text_BigText.text = Defeat_Bigtext;
        Text_Smalltext.text = Defeat_Smalltext;
        Text_BottomText.text = Defeat_Bottomtext;
        EndScreen.SetActive(true);
    }

    public void Win()
    {
        Text_BigText.text = Victory_Bigtext;
        Text_Smalltext.text = Victory_Smalltext;
        Text_BottomText.text = Victory_Bottomtext;
        EndScreen.SetActive(true);
    }

    public void Restart()
    {
        SceneManager.LoadScene("SampleScene", LoadSceneMode.Single);
    }

    public void TitleScreen()
    {
        SceneManager.LoadScene("TitleScreen", LoadSceneMode.Single);
    }



}
