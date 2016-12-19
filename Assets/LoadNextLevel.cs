using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class LoadNextLevel : MonoBehaviour {
	public string nextLevel;
	public static LoadNextLevel main;

	void Start() {
		main = this;
	}

	public void loadNextLevel() {
		SceneManager.LoadScene(nextLevel);
	}
}
