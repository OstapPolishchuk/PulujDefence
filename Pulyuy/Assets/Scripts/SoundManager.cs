using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private static SoundManager instance;

    [SerializeField] private AudioSource music;
    [SerializeField] private AudioSource shoot;
    [SerializeField] private AudioSource generatorWorking;
    [SerializeField] private AudioSource generatorOverheat;
    [SerializeField] private AudioSource xrayOn;
    [SerializeField] private AudioSource xrayOff;
    [SerializeField] private AudioSource doorOpen;
    [SerializeField] private AudioSource doorClose;
    [SerializeField] private AudioSource win;
    [SerializeField] private AudioSource fail;

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Instance freak up");
        }
        instance = this;
    }

    private void Start()
    {
        music.Play();
    }

    public static void PlayOverHeat() { instance.generatorOverheat.Play(); }

    public static void PlayShoot() { instance.shoot.Play(); }

    public static void SetGeneratorPlaying(bool value)
    {
        if (value) instance.generatorWorking.Play();
        else instance.generatorWorking.Stop();
    }

    public static void SetXray(bool value)
    {
        if (value) instance.xrayOn.Play();
        else instance.xrayOff.Play();
    }

    public static void SetDoor(bool value)
    {
        if (value) instance.doorOpen.Play();
        else instance.doorClose.Play();
    }

    public static void Finish(bool succesfuly)
    {
        if (succesfuly) instance.win.Play();
        else instance.fail.Play();
    }
}