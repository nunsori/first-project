using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Game_Play_Manager : MonoBehaviour
{
    public GameObject submenu;
    public GameObject clearmenu;
    public GameObject player_object;
    public GameObject defaultmenu;

    public int clear_time_second;

    public bool game_clear = false;

    public TextMeshProUGUI clear_text;

    public TextMeshProUGUI coin_text;

    public bool[] coin_asset = { true, true, true, true, true };
    public GameObject [] coin_asset_object= new GameObject[5];

    public AudioSource button_audio;
    public AudioSource bgm_audio;

    public Slider bgm_volume;

    public AudioSource clear_audio;

    // Start is called before the first frame update
    void Start()
    {
        Screen.SetResolution(1280, 720, true); //16:9 screen ratio
        player_object.transform.position = new Vector3(0, 0, 0);
        submenu.SetActive(false);
        clearmenu.SetActive(false);
        defaultmenu.SetActive(true);
        Load_Game();
        StartCoroutine("clear_time");
        Application.targetFrameRate = 60;
        bgm_audio.loop = true;
        bgm_audio.Play();
    }

    // Update is called once per frame
    void Update()
    {
        bgm_audio.volume = bgm_volume.value;
        clear_audio.volume = bgm_volume.value;

        if (Input.GetKeyDown(KeyCode.Escape)&&game_clear == false)
        {
            //submenu.SetActive(true);
            submenu_active();
        }

        if(player_object.transform.position.y < -10)
        {
            player_object.transform.position = new Vector3(0, 0, 0);
        }

        if(game_clear == true)
        {
            clearmenu.SetActive(true);
            StopCoroutine("clear_time");
            Debug.Log("game_clear");
            clear_time_print();
            
        }

        if(submenu.activeSelf == false)
        {
            Time.timeScale = 1;
        }
    }

    public void Game_Exit()
    {
        Application.Quit();
    }


    public void Save_Game()
    {
        int secondsave = 0;
        if(PlayerPrefs.HasKey("clear_time"))
        {
            secondsave = PlayerPrefs.GetInt("clear_time");
        }

        PlayerPrefs.SetFloat("PlayerX", player_object.transform.position.x);
        PlayerPrefs.SetFloat("PlayerY", player_object.transform.position.y);
        PlayerPrefs.SetInt("clear_time", clear_time_second+secondsave);
        PlayerPrefs.Save();

        submenu.SetActive(false);
    }

    public void Load_Game()
    {
        if(!PlayerPrefs.HasKey("PlayerX"))
        {
            return;
        }

        player_object.transform.position = new Vector3(PlayerPrefs.GetFloat("PlayerX"), PlayerPrefs.GetFloat("PlayerY"), 0);
    }

    public void Reset_Data()
    {
        PlayerPrefs.DeleteAll();
    }

    IEnumerator clear_time()
    {
        clear_time_second = 0;

        while(true)
        {
            yield return new WaitForSeconds(1);
            clear_time_second += 1;

        }
    }

    public void title_load()
    {
        SceneManager.LoadScene("menu");
    }

    public void clear_time_print()
    {
        bgm_audio.Stop();
        clear_audio.Play();

        if (PlayerPrefs.HasKey("clear_time"))
        {
            clear_time_second += PlayerPrefs.GetInt("clear_time");
        }

        //clear_time_second += PlayerPrefs.GetInt("clear_time");
        clear_text.text = clear_time_second.ToString();

        game_clear = false;
    }

    public void submenu_active()
    {
        Time.timeScale = 0;
        submenu.SetActive(true);
        defaultmenu.SetActive(false);
    }

    public void button_sound()
    {
        button_audio.Play();
    }
}
