using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;



public class AudioManager : SingletonMono<AudioManager>
{
    public AudioType[] audioTypes;

    protected override void Awake()
    {
        base.Awake();
        foreach (var type in audioTypes)
        {
            type.source = gameObject.AddComponent<AudioSource>();

            type.source.clip = type.clip;
            type.source.name = type.name.ToString();
            type.source.volume = type.volume;
            type.source.pitch = type.pitch;
            type.source.loop = type.loop;

            if (type.group != null)
            {
                type.source.outputAudioMixerGroup = type.group;
            }
        }
    }
    private void Start()
    {

    }

    /// <summary>
    /// ������Ƶ
    /// </summary>
    /// <param name="name">��Ƶ������</param>
    public void Play(AudioName name)
    {
        foreach (var type in audioTypes)
        {
            if (type.name == name)
            {
                type.source.Play();
                return;
            }
        }
        Debug.LogWarning("��" + name + "��Ƶ");
    }
    /// <summary>
    /// ������Ƶ�����¸��������ߣ�
    /// </summary>
    /// <param name="name"></param>
    public void PlayWithRandomPitch(AudioName name, float minPitch, float maxPitch)
    {
        foreach (var type in audioTypes)
        {
            if (type.name == name)
            {
                type.source.pitch = Random.Range(minPitch, maxPitch);
                type.source.Play();
                return;
            }
        }
    }
    /// <summary>
    /// ��ͣ��Ƶ
    /// </summary>
    /// <param name="name">��Ƶ������</param>
    public void Pause(AudioName name)
    {
        foreach (var type in audioTypes)
        {
            if (type.name == name)
            {
                type.source.Pause();
                return;
            }
        }
        Debug.LogWarning("��" + name + "��Ƶ");
    }

    /// <summary>
    /// ֹͣ��Ƶ
    /// </summary>
    /// <param name="name">��Ƶ������</param>
    public void Stop(AudioName name)
    {
        foreach (var type in audioTypes)
        {
            if (type.name == name)
            {
                type.source.Stop();
                return;
            }
        }
        Debug.LogWarning("��" + name + "��Ƶ");
    }
}

[System.Serializable]
public class AudioType
{
    [HideInInspector]
    public AudioSource source;
    public AudioClip clip;
    public AudioMixerGroup group;


    public AudioName name;
    [Range(0f, 1f)]
    public float volume;
    [Range(0.1f, 5f)]
    public float pitch;
    public bool loop;
}