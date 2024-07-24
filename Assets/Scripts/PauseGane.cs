using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseGane : MonoBehaviour
{

    [SerializeField] public GameObject PausePanel;
    public bool isPaused = false;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            
            if (isPaused)
            {
                PausePanel.SetActive(false);
                Time.timeScale = 1f;
                
            }
            else
            {
                PausePanel.SetActive(true);
                Time.timeScale = 0f;
            }
            isPaused = !isPaused;
            
        }
    }
}
