using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUi;
    private Transform _player;

    // Update is called once per frame
    private void OnEnable()
    {
        PlayerInputs.ActivateMenu += ActivateMenu;
    }
    
    private void OnDisable()
    {
        PlayerInputs.ActivateMenu -= ActivateMenu;
    }

    private void Start()
    {
       _player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Escape))
        //{
        //    if (GameIsPaused)
        //    {
        //        Resume();
        //    }
        //    else
        //    {
        //        Pause();
        //    }
        //} 
    }
    public void Resume()
    {
        pauseMenuUi.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;

        MovingPlayer();
    }
    void Pause()
    {
        pauseMenuUi.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
        
        FreezePlayer();
    }

    public void ExitButton()
    {
        //Application.Quit();
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    private void FreezePlayer()
    {
        _player.GetComponent<PlayerInput>().enabled = false;
        _player.GetComponent<PlayerInputs>().FreezePlayer();
        _player.GetComponent<PlayerInputs>().enabled = false;
    }

    private void MovingPlayer()
    {
        _player.GetComponent<PlayerInput>().enabled = true;
        _player.GetComponent<PlayerInputs>().enabled = true;
    }

    public void ActivateMenu()
    {
        if (GameIsPaused)
        {
            Resume();
        }
        else
        {
            Pause();
        }
    }
}
    

