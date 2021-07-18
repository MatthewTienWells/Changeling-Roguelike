using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMover : MonoBehaviour
{
    [SerializeField]
    private float timer = 3f;

    // Start is called before the first frame update
    private void Start()
    {
        StartCoroutine(SceneChangeTimer());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetButtonDown("Attack")) 
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    //A timer for switching scenes from the intro scene
    IEnumerator SceneChangeTimer()
    {
        yield return new WaitForSeconds(timer);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
