using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameElements : MonoBehaviour
{
    public Transform player;
    private bool reloadScene = false;
    public AudioSource music;
    public GameObject panel;

    // Update is called once per frame
    void Update()
    {
        if(player.transform.position.y <= -25f && reloadScene == false)
        {
            Scene scene = SceneManager.GetActiveScene(); SceneManager.LoadScene(scene.name);

            reloadScene = true;
        }
        if(Input.GetKeyDown(KeyCode.M))
        {
            music.volume = (music.volume == 0.05f) ? 0 : 0.05f;
        }

        if (Input.GetKeyDown(KeyCode.J))
        {
            if(panel.activeSelf == true)
            {
                panel.SetActive(false);
                Cursor.lockState = CursorLockMode.Locked;
                Time.timeScale = 1;
            }
            else
            {
                panel.SetActive(true);
                Cursor.lockState = CursorLockMode.None;
                Time.timeScale = 0;
            }
        }

    }
}
