using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour {

    public Transform vrCamera;
    public float toggleAngle = 30.0f;
    public float speed = 10.0f;

    //private CharacterController cc;
    private Rigidbody rbody;

    void Start(){
    	//cc = GetComponent<CharacterController> ();
    	rbody = GetComponent<Rigidbody> ();
    }

    void Update(){

    	if(Input.GetButton("Fire1") || Input.GetButtonDown ("Fire2")){
    		//rbody.MovePosition(transform.parent.position + Camera.main.transform.forward * speed * Time.deltaTime);

        	Vector3 tempVect = new Vector3(0, 0, 1);
        	tempVect = Camera.main.transform.forward * speed * Time.deltaTime;
        	rbody.MovePosition(transform.position + tempVect);
    	}
    }
}
