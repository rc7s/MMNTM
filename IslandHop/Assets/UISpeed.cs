using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISpeed : MonoBehaviour {

    public Text speed;
    public Rigidbody rb;

    // Use this for initialization
    void Start () {
        speed = GetComponent<Text>();
        rb = GetComponentInParent<Rigidbody>();
    }
	
	// Update is called once per frame
	void Update () {
        speed.text = rb.velocity.magnitude.ToString("F2") + " km/h";
    }
}
