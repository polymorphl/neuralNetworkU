using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

    public float speed;
    public BoxCollider boundingBox;


	// Use this for initialization
	void Start () {

    }

	// Update is called once per frame
	void Update () {
        Vector3 newPos = this.transform.position;
        newPos += new Vector3(0, Input.GetAxis("Vertical"), 0) * speed;
        newPos += new Vector3(Input.GetAxis("Horizontal"), 0, 0) * speed;

        if(boundingBox.bounds.Contains(newPos)) {
            transform.position = newPos;
        }
    }
}
