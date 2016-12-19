using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerControl : MonoBehaviour {
	public float horizontalSpeed;
	public float jumpForce;
	public LayerMask groundColliders;
	public Transform groundedCheck;
	public GameObject tetherMark;

	public bool grounded;
	private Rigidbody2D rb;

	public List<TempTetherMark> tetherMarks;

	private bool jumping;


	// Use this for initialization
	void Start () {
		rb = gameObject.GetComponent<Rigidbody2D>();
		tetherMarks = new List<TempTetherMark>();

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
				TempTetherMark mark = ((GameObject) Instantiate(tetherMark, hit.point, Quaternion.identity)).GetComponent<TempTetherMark>();
				mark.clickedObject = hit.collider.gameObject;
				mark.localClickedPos = mark.clickedObject.transform.InverseTransformPoint(hit.point);
				mark.transform.parent = mark.clickedObject.transform;
				tetherMarks.Add(mark);
			}

			if(tetherMarks.Count > 1) {
				TetheredObjects.main.addObject(tetherMarks[0].clickedObject, tetherMarks[0].localClickedPos, tetherMarks[1].clickedObject, tetherMarks[1].localClickedPos);
				foreach (TempTetherMark t in tetherMarks) {
					Destroy(t.gameObject);
				}
				tetherMarks.Clear();
			}
		}

		if (Input.GetKeyDown(KeyCode.Space)) {
			TetheredObjects.main.removeAll();
			foreach (TempTetherMark t in tetherMarks) {
				Destroy(t.gameObject);
			}
			tetherMarks.Clear();
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
