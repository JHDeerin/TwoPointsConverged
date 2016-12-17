using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TetheredObjects : MonoBehaviour {
	public static TetheredObjects main;
	public List<DragableObject> tethered;
	public float force;

	public float lineRenderWidth;

	void Start () {
		main = this;
		tethered = new List<DragableObject>();
	}

	void FixedUpdate () {
		foreach (DragableObject obj in tethered) {
			obj.tetherForce(force);
			obj.pairedObject.tetherForce(force);

			if(obj.lineRend != null) {
				Vector3[] positions = {obj.attachedObject.transform.TransformPoint(obj.localForcePos), obj.pairedObject.attachedObject.transform.TransformPoint(obj.pairedObject.localForcePos)};
				obj.lineRend.SetPositions(positions);
			}
		}
	}

	public void addObject(GameObject obj1, Vector3 localPos1, GameObject obj2, Vector3 localPos2) {
		DragableObject newObj = new DragableObject();
		newObj.attachedObject = obj1;
		newObj.localForcePos = localPos1;

		newObj.pairedObject = new DragableObject();
		newObj.pairedObject.pairedObject = newObj;
		newObj.pairedObject.attachedObject = obj2;
		newObj.pairedObject.localForcePos = localPos2;

		GameObject rendObj = new GameObject(); //just holds the lineRenderer component
		rendObj.transform.parent = obj1.transform;
		newObj.lineRend = rendObj.AddComponent<LineRenderer>();
		newObj.lineRend.SetWidth(lineRenderWidth, lineRenderWidth);

		tethered.Add(newObj);
	}

	public void removeAll() {
		foreach (DragableObject obj in tethered) {
			if(obj.lineRend != null) {
				Destroy(obj.lineRend.gameObject);
			}
		}
		tethered.Clear();
	}
}
