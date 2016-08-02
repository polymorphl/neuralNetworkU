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
        Vector3 movey = new Vector3(0, Input.GetAxis("Vertical"), 0) * speed;
        Vector3 movex = new Vector3(Input.GetAxis("Horizontal"), 0, 0) * speed;

        if (boundingBox.bounds.Contains(transform.position + movex))
            transform.position += movex;

        if (boundingBox.bounds.Contains(transform.position + movey))
            transform.position += movey;
    }
}
