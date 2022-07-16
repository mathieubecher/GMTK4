using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAudio : MonoBehaviour
{
    [Header("Character Voices")]
    [SerializeField] private AudioEvent m_drinkVoice;
    [SerializeField] private AudioEvent m_beerFinishedVoice;
    [SerializeField] private AudioEvent m_burpVoice;
    [SerializeField] private AudioEvent m_painVoice;
    [SerializeField] private AudioEvent m_deathVoice;

    [Header("Character Feedbacks")]
    [SerializeField] private AudioEvent m_beerFinishedFB;
    [SerializeField] private AudioEvent m_hitFB;

    [Header("Gun Foleys")]
    [SerializeField] private AudioEvent m_gunShotAudio;
    [SerializeField] private AudioEvent m_gunReloadAudio;

    [Header("Door Foleys")]
    [SerializeField] private AudioEvent m_doorOpenAudio;

    private Character m_character;

    private void OnEnable()
    {

    }

    private void OnDisable()
    {

    }

    private void Start()
    {
        gameObject.TryGetComponent(out m_character);
    }
}
