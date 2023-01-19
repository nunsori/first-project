using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class title_manager : MonoBehaviour
{
    // Start is called before the first frame update

    public AudioSource button_audio;
    public GameObject control_menu;
    public GameObject main_menu_object2;
    public AudioSource bgm;

    public Slider bgm_slider;

    void Start()
    {
        Screen.SetResolution(1280, 720, true);//16:9 screen ratio
        Application.targetFrameRate = 60;
        control_menu.SetActive(false);
        bgm.loop = true;
        bgm.Play();
    }

    // Update is called once per frame
    void Update()
    {
        bgm.volume = bgm_slider.value;
    }

    public void reset_data_t()
    {
        PlayerPrefs.DeleteAll();
    }

    public void quit_game_t()
    {
        Application.Quit();
    }

    public void sound_button()
    {
        button_audio.Play();
    }

    public void controll_menu_on()
    {
        control_menu.SetActive(true);
    }

    public void controll_menu_off()
    {
        control_menu.SetActive(false);
    }

    public void stop_bgm()
    {
        bgm.Stop();
    }
}
