using UnityEngine;
using System.Collections;


public class PlayerMove : MonoBehaviour {

	public Rigidbody rb;

	public Vector3 sprint;

	public float ms;
	public float airspd;
	public float wallspd;

	public float maxSpeed;//Replace with your max speed

	public bool isGrounded;
	public bool onWall;
    private int velcheck;

    public Vector3 shake;

	// Use this for initialization
	void Start () {

		rb = GetComponent<Rigidbody>();

		sprint = transform.forward * 5;

		ms = 10;
		airspd = 5;
		wallspd = 10;
        maxSpeed = 12f;

        velcheck = 0;

        shake.x = 0f;
        shake.y = -0.05f;
        shake.z = 0f;
    }

	// Update is called once per frame
	void Update () {

		if (isGrounded) {
			if (Input.GetKey (KeyCode.W)) {
				//transform.position += transform.forward * Time.deltaTime * ms;
				rb.AddRelativeForce(Vector3.forward * ms);
			}	
			if (Input.GetKey (KeyCode.A)) {
				//transform.position += transform.right * -1 * Time.deltaTime * ms;
				rb.AddRelativeForce(Vector3.right * -ms);
			}	
			if (Input.GetKey (KeyCode.S)) {
				//transform.position += transform.forward * -1 * Time.deltaTime * ms;
				rb.AddRelativeForce(Vector3.forward * -ms);
			}	
			if (Input.GetKey (KeyCode.D)) {
				//transform.position += transform.right * Time.deltaTime * ms;
				rb.AddRelativeForce(Vector3.right * ms);
			}

            velcheck += 1;

		}
		if (!isGrounded) {
			if (!onWall) {
				if (Input.GetKey (KeyCode.W)) {
					//transform.position += transform.forward * Time.deltaTime * ms;
					rb.AddRelativeForce (Vector3.forward * airspd);
				}	
				if (Input.GetKey (KeyCode.A)) {
					//transform.position += transform.right * -1 * Time.deltaTime * ms;
					rb.AddRelativeForce (Vector3.right * -airspd);
				}	
				if (Input.GetKey (KeyCode.S)) {
					//transform.position += transform.forward * -1 * Time.deltaTime * ms;
					rb.AddRelativeForce (Vector3.forward * -airspd);
				}	
				if (Input.GetKey (KeyCode.D)) {
					//transform.position += transform.right * Time.deltaTime * ms;
					rb.AddRelativeForce (Vector3.right * airspd);
				}

                velcheck = 0;
            }
			if (onWall) {
				if (Input.GetKey (KeyCode.A)) {
					//transform.position += transform.right * -1 * Time.deltaTime * ms;
					rb.AddRelativeForce (Vector3.right * -wallspd);
				}	
				if (Input.GetKey (KeyCode.D)) {
					//transform.position += transform.right * Time.deltaTime * ms;
					rb.AddRelativeForce (Vector3.right * wallspd);
				}
				rb.AddRelativeForce (Vector3.up * 25);
				rb.AddRelativeForce (Vector3.forward * 20);
                velcheck += 1;
            }

		}
//		if (Input.GetKey (KeyCode.LeftShift)) {
//			transform.position += transform.forward * 2 * Time.deltaTime;
//		}	
//
		if (Input.GetKeyDown(KeyCode.Space) && isGrounded ){
			rb.AddForce (Vector3.up * 250);
		}
		if (Input.GetKeyDown(KeyCode.Space) && onWall ){
			//rb.AddForce (Vector3.up * 300);
		}

	}
    
	void FixedUpdate()
	{
        if (rb.velocity.magnitude > maxSpeed && isGrounded)
        {
            if (velcheck > 50)
            {
                rb.velocity = rb.velocity.normalized * maxSpeed;
            }
		}
        if (rb.velocity.magnitude > maxSpeed && onWall)
        {
            if (velcheck > 1)
            {
                rb.velocity = rb.velocity.normalized * maxSpeed;
            }
        }
    }

	void OnCollisionStay (Collision collision)
	{
        
            if (collision.gameObject.tag == "floor")
            {
                // iTween.ShakePosition(gameObject, shake, 1); fix when colliding
                 isGrounded = true;
            }
            if (collision.gameObject.tag == "wall")
            {
                onWall = true;
            }
     
	}

	void OnCollisionExit (Collision collision)
	{
		if(collision.gameObject.tag == "floor"){
			isGrounded = false;
		}
		if(collision.gameObject.tag == "wall"){
			onWall = false;
		}
	}
}
