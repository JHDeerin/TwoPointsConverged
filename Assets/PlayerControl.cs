using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerControl : MonoBehaviour {
	public float horizontalSpeed;
	public float jumpForce;
	public LayerMask groundColliders;
	public Transform groundedCheck;

	public bool grounded;
	private Rigidbody2D rb;

	public List<GameObject> clickedObjects;
	public List<Vector3> localClickPos;

	private bool jumping;


	// Use this for initialization
	void Start () {
		rb = gameObject.GetComponent<Rigidbody2D>();
		clickedObjects = new List<GameObject>();
		localClickPos = new List<Vector3>();

		grounded = false;
		jumping = false;
	}

	void Update() { //handles input
		if (Input.GetKeyDown(KeyCode.W)) {
			jumping = true;
		}

		//raycasts for testing tether
		if (Input.GetMouseButtonDown(0)) {
			RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
			if (hit.collider != null) {
				clickedObjects.Add(hit.collider.gameObject);
				localClickPos.Add(hit.collider.gameObject.transform.InverseTransformPoint(hit.point));
			}

			if(clickedObjects.Count > 1) {
				TetheredObjects.main.addObject(clickedObjects[0], localClickPos[0], clickedObjects[1], localClickPos[1]);
				clickedObjects.Clear();
				localClickPos.Clear();
			}
		}

		if (Input.GetKeyDown(KeyCode.Space)) {
			TetheredObjects.main.removeAll();
		}
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		grounded = Physics2D.OverlapCircle(groundedCheck.position, 0.2f, groundColliders);
			
		//Horizontal movement
		if (Input.GetKey(KeyCode.D)) {
			rb.velocity = new Vector2(horizontalSpeed, rb.velocity.y);
		} else if (Input.GetKey(KeyCode.A)) {
			rb.velocity = new Vector2(-1f * horizontalSpeed, rb.velocity.y);
		}

		//jumping
		if (grounded) {
			if (jumping) {
				rb.AddForce(Vector2.up * jumpForce);
				jumping = false;
			}
		}
	}
}
