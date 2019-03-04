using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour {
    
    public void LoadTestfiveAScene()
    {
        Debug.Log("SwitchScene");
        SceneManager.LoadScene("test5a");
    }

}
