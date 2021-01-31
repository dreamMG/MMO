using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Loading : MonoBehaviour
{
    private float timer;

    public Slider slider;

    void Update()
    {
        timer += Time.deltaTime;

        slider.value = timer;

        if (slider.maxValue <= timer)
        {
            SceneManager.UnloadSceneAsync("LoadingScene");
        }
    }
}
