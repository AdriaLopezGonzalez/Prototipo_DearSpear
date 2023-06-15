using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public AudioMixer musicMixer, sfxMixer;

    [SerializeField] private AudioSource enemyHit;
    [SerializeField] private AudioSource enemyHurt1;
    [SerializeField] private AudioSource enemyHurt2;
    [SerializeField] private AudioSource enemyHurt3;
    [SerializeField] private AudioSource enemyCloseKill;
    [SerializeField] private AudioSource dogHurt;

    private AudioSource[] enemyHurts = new AudioSource[3];

    [SerializeField] private AudioSource throwSpear;
    [SerializeField] private AudioSource land;

    public static AudioManager instance;

    [Range(-40, 10)]
    public float musicVolume, sfxVolume;
    public Slider musicSlider, sfxSlider;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        MakeAudioValues();

        enemyHurts[0] = enemyHurt1;
        enemyHurts[1] = enemyHurt2;
        enemyHurts[2] = enemyHurt3;
    }

    // Update is called once per frame
    void Update()
    {
        MusicVolume();
        SfxVolume();
    }

    private void MakeAudioValues()
    {
        musicSlider.value = musicVolume;
        sfxSlider.value = sfxVolume;

        musicSlider.minValue = -40;
        musicSlider.maxValue = 10;

        sfxSlider.minValue = -40;
        sfxSlider.maxValue = 10;
    }

    public void MusicVolume()
    {
        musicMixer.SetFloat("musicMasterVolume", musicSlider.value);
    }

    public void SfxVolume()
    {
        sfxMixer.SetFloat("sfxMasterVolume", sfxSlider.value);
    }

    public void EnemyHurt()
    {
        enemyHit.Play();
        enemyHurts[Random.Range(0,3)].Play();
    }

    public void DogHurt()
    {
        enemyHit.Play();
        dogHurt.Play();
    }

    public void EnemyCloseKill()
    {
        enemyCloseKill.pitch = Random.Range(0.85f, 1.15f);
        enemyCloseKill.Play();
        enemyHit.Play();
    }

    public void ThrowSpear()
    {
        throwSpear.Play();
    }

    public void Land()
    {
        land.Play();
    }
}
