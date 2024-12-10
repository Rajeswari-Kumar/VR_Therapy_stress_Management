using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
public class Scene_manager : MonoBehaviour
{
    public InputActionProperty Go_back_scene;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(Go_back_scene.action.IsPressed())
        {
            SceneSwitch("Start scene");
        }
    }

    public void SceneSwitch(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}