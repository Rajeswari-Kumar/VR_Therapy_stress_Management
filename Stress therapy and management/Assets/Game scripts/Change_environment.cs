using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Change_environment : MonoBehaviour
{
    public GameObject[] environments;
    public InputActionProperty environmentChange;
    public InputActionProperty skyboxChange;
    public Material[] skyboxes;
    public AudioSource[] backgroundAudio;

    private int currentEnvironmentIndex = 0;
    private int currentSkyboxIndex = 0;
    private int currentAudio = 0;

    void Start()
    {
        for (int i = 0; i < environments.Length; i++)
        {
            environments[i].SetActive(i == currentEnvironmentIndex);
        }

        if (skyboxes.Length > 0)
        {
            RenderSettings.skybox = skyboxes[currentSkyboxIndex];
        }
    }

    void Update()
    {
        if (environmentChange.action.WasPressedThisFrame())
        {
            ChangeEnvironment();
        }

        if (skyboxChange.action.WasPressedThisFrame())
        {
            ChangeSkybox();
        }
    }

    public void ChangeEnvironment()
    {
        StopCurrentBackground(currentEnvironmentIndex);
        environments[currentEnvironmentIndex].SetActive(false);
        currentEnvironmentIndex = (currentEnvironmentIndex + 1) % environments.Length;
        environments[currentEnvironmentIndex].SetActive(true);
        ChangebackgourdAudio(currentEnvironmentIndex);
    }

    public void ChangeSkybox()
    {
        if (skyboxes.Length == 0) return;
        currentSkyboxIndex = (currentSkyboxIndex + 1) % skyboxes.Length;
        RenderSettings.skybox = skyboxes[currentSkyboxIndex];
        DynamicGI.UpdateEnvironment();
    }

    public void ChangebackgourdAudio(int audio)
    {
        if (backgroundAudio.Length == 0) return;
        currentAudio = (currentAudio + 1) % backgroundAudio.Length;
        backgroundAudio[currentAudio].Play();
    }
    public void StopCurrentBackground(int currentAudio)
    {
        backgroundAudio[currentAudio].Stop();
    }
}
