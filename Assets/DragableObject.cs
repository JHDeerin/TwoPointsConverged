using UnityEngine;
using System.Collections;

public class DragableObject {
	public DragableObject pairedObject; //the object on the other side of the rope 

	public GameObject attachedObject;
	public Vector3 localForcePos;
	public LineRenderer lineRend;

	public void tetherForce(float forceAmt) {
		Vector2 forceDir = to2D(Vector3.Normalize(pairedObject.attachedObject.transform.TransformPoint(pairedObject.localForcePos) 
			- attachedObject.transform.TransformPoint(localForcePos)));
		if (attachedObject.GetComponent<Rigidbody2D>() != null) {
			attachedObject.GetComponent<Rigidbody2D>().AddForceAtPosition(
				forceDir * forceAmt, attachedObject.transform.TransformPoint(localForcePos));
		}
	}

	private Vector2 to2D (Vector3 orig) {
		return new Vector2(orig.x, orig.y);
	}
}
