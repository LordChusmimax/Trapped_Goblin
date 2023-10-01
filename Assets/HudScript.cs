using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class HudScript : MonoBehaviour
{
    public static bool dead;
    public static HudScript current;
    [SerializeField]GameObject hud, fade, win;
    PlayerInput inputAction;
    // Start is called before the first frame update
    void Start()
    {
        dead = false;
        current = this;
        inputAction = new PlayerInput();
        inputAction.Hud.Retry.performed+=Restart;
        Reset();
        
    }

    void Restart(InputAction.CallbackContext action)
    {
        inputAction.Hud.Disable();
        SceneManager.LoadScene(0);
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public void Reset()
    {
        Time.timeScale = 1;
        hud.SetActive(true);
        fade.SetActive(false);
    }

    public void Kill()
    {

        inputAction.Hud.Enable();
        Time.timeScale = 0;
        dead = true;
        hud.SetActive(false);
        fade.SetActive(true);
    }

    public void Win()
    {

        inputAction.Hud.Enable();
        Time.timeScale = 0;
        dead = true;
        hud.SetActive(false);
        win.SetActive(true);
    }

    private void OnDestroy()
    {
        inputAction.Hud.Disable();
        inputAction.Hud.Retry.performed -= Restart;
    }
}
