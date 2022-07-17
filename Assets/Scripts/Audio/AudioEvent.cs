using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AudioEvent
{
    public FMODUnity.EventReference m_eventReference;
    private FMOD.Studio.EventInstance m_eventInstance;
    bool m_isInit = false;

    public void Instantiate(GameObject _emitter)
    {
        m_eventInstance = FMODUnity.RuntimeManager.CreateInstance(m_eventReference);
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(m_eventInstance, _emitter.transform);
        m_isInit = true;
        //Debug.Log($"{m_eventReference.Path} is initialized.");
    }

    public void Play(GameObject _emitter)
    {
        if (!m_isInit)
            Instantiate(_emitter);
        m_eventInstance.start();
        //Debug.Log($"{m_eventReference.Path} is playing.");
    }

    public void PlayOneShot(GameObject _emitter) => FMODUnity.RuntimeManager.PlayOneShotAttached(m_eventReference, _emitter);

    public void Stop()
    {
        m_eventInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        m_eventInstance.release();
        m_isInit = false;
        //Debug.Log($"{m_eventReference.Path} was stopped and cleared.");
    }

    public void SetParameter(string _parameterName, float _value) => m_eventInstance.setParameterByName(_parameterName, _value);

    public void SetParameter(string _parameterName, int _value) => m_eventInstance.setParameterByName(_parameterName, _value);
}
