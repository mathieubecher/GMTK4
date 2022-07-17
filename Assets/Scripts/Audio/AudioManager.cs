using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [SerializeField] private AudioEvent m_mainMusic;
    [SerializeField] private AudioEvent m_bulletWoodImpact;

    private bool m_isMusicPlaying;

    private void OnEnable()
    {
        Bullet.OnHitObject += Bullet_OnHitObject;
        SceneManager.sceneLoaded += SceneManager_sceneLoaded;
    }

    private void SceneManager_sceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        if (SceneManager.GetActiveScene().name == "SampleScene")
        {
            FMODUnity.RuntimeManager.StudioSystem.setParameterByName("MusicStates", 0);
            float value;
            FMODUnity.RuntimeManager.StudioSystem.getParameterByName("MusicStates", out value);
            Debug.Log($"Music state = {value}");
        }
        else if (SceneManager.GetActiveScene().name == "TitleScreen")
        {
            FMODUnity.RuntimeManager.StudioSystem.setParameterByName("MusicStates", 6);
            float value;
            FMODUnity.RuntimeManager.StudioSystem.getParameterByName("MusicStates", out value);
            Debug.Log($"Music state = {value}");
        }
        if (!m_isMusicPlaying)
        {
            Debug.Log("Playing music");
            m_isMusicPlaying = true;
            m_mainMusic.Play(gameObject);
        }
    }

    private void OnDisable()
    {
        Bullet.OnHitObject -= Bullet_OnHitObject;
        SceneManager.sceneLoaded -= SceneManager_sceneLoaded;
    }

    private void Bullet_OnHitObject(Bullet _bullet, GameObject _hit, Bullet.ContactType _contactType)
    {
        if (_contactType == Bullet.ContactType.Wall)
        {
            GameObject emitter = new GameObject();
            emitter.transform.position = _bullet.transform.position;
            m_bulletWoodImpact.PlayOneShot(emitter);
            StartCoroutine(DestroyCooldown(emitter));
        }
    }

    private void Awake()
    {
        m_isMusicPlaying = false;

        if (instance != null)
            Destroy(gameObject);
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void OnLevelWasLoaded(int level)
    {
        /*
        Debug.Log("ALLO");
        if (SceneManager.GetActiveScene().name == "SampleScene")
        {
            FMODUnity.RuntimeManager.StudioSystem.setParameterByName("MusicStates", 0);
        }
        else if (SceneManager.GetActiveScene().name == "TitleScreen")
        {
            FMODUnity.RuntimeManager.StudioSystem.setParameterByName("MusicStates", 6);
        }
        if (!m_isMusicPlaying)
        {
            Debug.Log("Playing music");
            m_isMusicPlaying = true;
            m_mainMusic.Play(gameObject);
        }
        */
    }

    private IEnumerator DestroyCooldown(GameObject _emitter)
    {
        yield return new WaitForSeconds(2f);
        Destroy(_emitter);
    }
}
