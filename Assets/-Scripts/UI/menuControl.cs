using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menuControl : MonoBehaviour
{
    public GameObject _escapeMenu;
    public bool escapeActive = false;

    public PlayerLook _playerLookScript;
    public ShotgunMechanic _shotgunScript;
    public ShotgunAnim _shotgunAnimScript;


    // Start is called before the first frame update
    private void Awake()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            escapeActive = !escapeActive;
            if (escapeActive)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                _escapeMenu.SetActive(true);
                Time.timeScale = 0;
                _playerLookScript.enabled = false;
                _shotgunScript.enabled = false;
                _shotgunAnimScript.enabled = false;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                _escapeMenu.SetActive(false);
                Time.timeScale = 1;
                _playerLookScript.enabled = true;
                _shotgunScript.enabled = true;
                _shotgunAnimScript.enabled = true;

            }
        }
    }


    public void Continue()
    {
        _escapeMenu.SetActive(false);
        escapeActive = false;
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        _playerLookScript.enabled = true;
        _shotgunScript.enabled = true;
        _shotgunAnimScript.enabled = true;
    }

    public void GoToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
        Application.Quit();
    }


}
