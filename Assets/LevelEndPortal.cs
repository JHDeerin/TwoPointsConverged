using UnityEngine;
using System.Collections;

public class LevelEndPortal : MonoBehaviour {
	void OnTriggerEnter2D(Collider2D col) {
		if (col.gameObject.CompareTag("Player")) {
			LoadNextLevel.main.loadNextLevel();
		}
	}
}
