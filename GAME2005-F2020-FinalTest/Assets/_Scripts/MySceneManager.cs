using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MySceneManager : MonoBehaviour
{

    public void loadStartScene()
    {
        SceneManager.LoadScene(0);
    }

    public void loadPlayScene()
    {
        SceneManager.LoadScene(1);
    }

}
