using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMover : MonoBehaviour
{
    [SerializeField]
    private float timer = 3f;
    // Start is called before the first frame update
    void Start()
    {
        SceneChangeTimer();
    }

    //A timer for switching scenes from the intro scene
    IEnumerable SceneChangeTimer()
    {
        yield return new WaitForSeconds(timer);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
