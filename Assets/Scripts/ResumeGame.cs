using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResumeGame : MonoBehaviour
{
    [SerializeField] private GameObject PausePanel;
   public void Resume()
    {
        PausePanel.SetActive(false);
        Time.timeScale = 1;
    }
}
