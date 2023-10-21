using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitApp : MonoBehaviour
{ 
    void Update()
    {
        CloseApp();
    }

    // ReSharper disable Unity.PerformanceAnalysis
    void CloseApp()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
            Debug.Log("App CLosed");
        }
    }
}
