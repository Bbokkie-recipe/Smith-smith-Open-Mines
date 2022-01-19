using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

//타 스크립트에서 사용원할 시 아래와 같이 호출
/*public AudioClip[] clips;
if (Input.GetKeyDown(KeyCode.Space)) SoundManager.instance.SFXPlay("Jump", clips[0]);*/
public class SoundManager : Singleton<SoundManager>
{
    public float musicValue;

    public AudioMixer mixer;
    public AudioSource bgmSound;
    public AudioClip[] bglist; //bglist 배열의 요소로 배경음을 넣어주어야 함
    public override void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else Destroy(gameObject);
    }

    private void Start()
    {
        musicValue = 1;
    }
    private void Update()
    {
        float musicVolume = musicValue;
        bgmSound.volume = musicVolume;
    }
    private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        for (int i = 0; i < bglist.Length; i++)
        {
            if (arg0.name == bglist[i].name)
                BgmSoundPlay(bglist[i]);
        }
    }
    //다 스크립트에서 SoundManager.instance.SFXPlay("아무이름",clips[0]); 식으로 호출하면 됨
    public void SFXPlay(string stxName, AudioClip clip)
    {
        GameObject go = new GameObject(stxName + "_SFXsound");
        AudioSource audioSource = go.AddComponent<AudioSource>();
        audioSource.outputAudioMixerGroup = mixer.FindMatchingGroups("SFX")[0];
        audioSource.clip = clip;
        audioSource.Play();

        Destroy(go, clip.length);//클립 길이 만큼 재생이 끝나면 자동 파괴됨
    }

    public void BgmSoundPlay(AudioClip clip)
    {
        bgmSound.outputAudioMixerGroup = mixer.FindMatchingGroups("Bgm")[0];
        bgmSound.clip = clip;
        bgmSound.loop = true;
        bgmSound.volume = 1f;
        bgmSound.Play();
    }
    public void BgmSoundVolume(float value)
    {
        mixer.SetFloat("BgmVolume", value);
     }
    public void EffectSoundVolume(float value)
    {
        mixer.SetFloat("EffectVolume", value);
    }

}
