using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public GameObject weaponMenu;
    public static bool isPaused;

    private void Start()
    {
        weaponMenu.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();    
            }
        }
    }

    public void PauseGame()
    {
        weaponMenu.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void ResumeGame()
    {
        weaponMenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }
}
