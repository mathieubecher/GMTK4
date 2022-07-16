using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotionAudio : MonoBehaviour
{
    [SerializeField] private AudioEvent m_footstepFoley;

    public void OnFootstep()
    {
        m_footstepFoley.Instantiate(gameObject);
        m_footstepFoley.Play(gameObject);
    }

    public void OnFootstepWithSpeedUpdate()
    {
        m_footstepFoley.Instantiate(gameObject);
        // m_footstepFoley.SetParameter("PlayerSpeed",); //GET SPEED
        m_footstepFoley.Play(gameObject);
    }
}
