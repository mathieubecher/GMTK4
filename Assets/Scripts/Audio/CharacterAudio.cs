using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAudio : MonoBehaviour
{
    [Header("Character Foleys")]
    [SerializeField] private AudioEvent m_moveFoley;

    [Header("Character Voices")]
    [SerializeField] private AudioEvent m_drinkVoice;
    [SerializeField] private AudioEvent m_beerFinishedVoice;
    [SerializeField] private AudioEvent m_burpVoice;
    [SerializeField] private AudioEvent m_painVoice;
    [SerializeField] private AudioEvent m_deathVoice;
    [SerializeField] private AudioEvent m_victoryVoice;

    [Header("Character Feedbacks")]
    [SerializeField] private AudioEvent m_beerFinishedFB;
    [SerializeField] private AudioEvent m_hitFB;

    [Header("Gun Foleys")]
    [SerializeField] private AudioEvent m_gunShotAudio;
    [SerializeField] private AudioEvent m_gunReloadAudio;

    [Header("Door Foleys")]
    [SerializeField] private AudioEvent m_doorExitAudio;

    [Header("Beer foleys")]
    [SerializeField] private AudioEvent m_beerTake;
    [SerializeField] private AudioEvent m_beerSet;

    private Character m_character;
    private int m_drinkedBeer;

    private int m_beerComboCounter;
    private float m_beerComboTimer;
    private float m_beerComboCurrentTime;

    private void OnDisable()
    {
        if (m_character)
        {
            m_character.OnShoot -= OnShoot;
            m_character.OnReload -= OnReload;
            m_character.OnDamaged -= OnDamaged;
            m_character.OnDead -= OnDead;
            Beer.OnTakeBeer -= OnTakeBeer;
            Beer.OnBeerRelease -= OnBeerRelease;
            Beer.OnBeerEmpty -= OnBeerEmpty;
            Door.OnDoorExit -= OnDoorExit;
        }
    }

    private void Start()
    {
        gameObject.TryGetComponent(out m_character);
        if (m_character)
        {
            m_character.OnShoot += OnShoot;
            m_character.OnReload += OnReload;
            m_character.OnDamaged += OnDamaged;
            m_character.OnDead += OnDead;
            Beer.OnTakeBeer += OnTakeBeer;
            Beer.OnBeerRelease += OnBeerRelease;
            Beer.OnBeerEmpty += OnBeerEmpty;
            Door.OnDoorExit += OnDoorExit;
        }
        m_beerComboTimer = 2f;
        m_beerComboCurrentTime = 0f;
        m_moveFoley.Play(gameObject);
        m_drinkedBeer = 0;
    }

    private void OnDoorExit()
    {
        m_doorExitAudio.PlayOneShot(gameObject);
        m_beerFinishedFB.PlayOneShot(gameObject);
        m_victoryVoice.PlayOneShot(gameObject);
    }

    private void Update()
    {
        if (m_beerComboCounter > 0)
        {
            m_beerComboCurrentTime += Time.deltaTime;
            if (m_beerComboCurrentTime >= m_beerComboTimer)
            {
                m_beerComboCounter--;
                m_beerComboCurrentTime = 0;
            }
        }
        else if (m_beerComboCurrentTime != 0)
            m_beerComboCurrentTime = 0;
        m_moveFoley.SetParameter("EntitySpeed", m_character.currentSpeed / 4f);
    }

    private void OnBeerEmpty(Beer _beer)
    {
        m_drinkedBeer++;
        m_beerFinishedFB.PlayOneShot(gameObject);
        m_beerFinishedVoice.PlayOneShot(gameObject);
        FMODUnity.RuntimeManager.StudioSystem.setParameterByName("MusicStates", m_drinkedBeer);
    }

    private void OnBeerRelease(Beer _beer, bool _success)
    {
        m_drinkVoice.Stop();
        m_beerSet.PlayOneShot(gameObject);
        m_beerComboCounter++;
        if (m_beerComboCounter > 1)
        {
            float proba = (float)(m_beerComboCounter - 1) / 2;
            float result = (float)Random.Range(0, 100)/100;
            if (result < proba)
            {
                m_burpVoice.PlayOneShot(gameObject);
                m_beerComboCounter = 0;
            }
        }
    }

    private void OnTakeBeer(Beer _beer)
    {
        m_beerTake.PlayOneShot(gameObject);
        m_drinkVoice.Play(gameObject);
    }

    private void OnDead()
    {
        m_hitFB.PlayOneShot(gameObject);
        m_deathVoice.PlayOneShot(gameObject);
    }

    private void OnDamaged(int _life)
    {
        m_hitFB.PlayOneShot(gameObject);
        m_painVoice.PlayOneShot(gameObject);
    }

    private void OnReload()
    {
        StartCoroutine(ReloadLoop());
    }

    private IEnumerator ReloadLoop()
    {
        for (int i = 0; i < 6; ++i)
        {
            yield return new WaitForSeconds(0.3f);
            FMODUnity.RuntimeManager.StudioSystem.setParameterByName("ReloadingSteps", i);
            m_gunReloadAudio.PlayOneShot(gameObject);
            //FMODUnity.RuntimeManager.StudioSystem.setParameterByName("RemainingBullets", _bullet);
        }
    }

    private void OnShoot(int _bullet)
    {
        m_gunShotAudio.PlayOneShot(gameObject);
        FMODUnity.RuntimeManager.StudioSystem.setParameterByName("RemainingBullets", _bullet);
    }
}
