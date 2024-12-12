using UnityEngine;
using UnityEngine.InputSystem;

public class QuitApplication : MonoBehaviour
{
    public InputActionProperty quitAction;

    void Update()
    {
        if (quitAction.action.WasPressedThisFrame())
        {
            QuitApp();
        }
    }

    public void QuitApp()
    {
        Debug.Log("Application quitting...");
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // Ensures quitting also works in the editor
#endif
    }
}
