using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Restart : MonoBehaviour {

	public void restart()
    {
        SceneManager.LoadScene(GameObject.Find("SceneHandler").GetComponent<SceneHandlerScript>().GetSceneToPlay());
    }
}
