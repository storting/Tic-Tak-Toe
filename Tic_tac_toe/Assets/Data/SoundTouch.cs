using UnityEngine;
using UnityEngine.Audio;

public class SoundTouch : MonoBehaviour
{
    private AudioSource m_AudioSource;

    private void Start()
    {
        m_AudioSource = gameObject.GetComponent<AudioSource>();
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            SoundPlay();
        }
    }

    public void SoundPlay()
    {
        m_AudioSource.Play();
        m_AudioSource.pitch = Random.Range(0.9f, 1.1f);
        m_AudioSource.volume = Random.Range(0.8f, 1.0f);
    }
}
