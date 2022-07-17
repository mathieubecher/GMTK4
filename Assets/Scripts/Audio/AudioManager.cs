using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [SerializeField] private AudioEvent m_menuMusic;
    [SerializeField] private AudioEvent m_mainMusic;

    private void Awake()
    {
        if (instance)
            Destroy(this);
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void OnLevelWasLoaded(int level)
    {
        if (SceneManager.GetActiveScene().name == "SampleScene")
        {
            m_menuMusic.Stop();
            m_mainMusic.Play(gameObject);
        }
        else
        {
            m_mainMusic.Stop();
            m_menuMusic.Play(gameObject);
        }
    }

    private void Start()
    {
        Debug.Log(SceneManager.GetActiveScene().name);
        if (SceneManager.GetActiveScene().name == "SampleScene")
        {
            m_mainMusic.Play(gameObject);
        }
        else
        {
            m_menuMusic.Play(gameObject);
        }
    }
}
