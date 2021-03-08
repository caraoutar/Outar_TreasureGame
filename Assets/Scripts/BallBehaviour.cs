using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallBehaviour : MonoBehaviour {

public SpriteRenderer myRenderer;
public Color floorColour;
public Color gateColour;
public Color yellowColour;
public Color blueColour;
public Color hinge2Colour;
public Color gateColour2;
public Color greenColour;
public Color purpleColour;
public Rigidbody2D myBody;

public GameObject firstCam;
float MoveDirection = 1;
public float speed; 
public float jumpHeight;
public float GravityMultiplier;
public float jumpMultiplier;
//boolean to check if player is on floor/platform or not
bool onFloor = true;

public GameObject orangegate;
public GameObject greengate;
public GameObject redgate;
public GameObject reddoor1;
public GameObject reddoor2;





public float power;


    // Start is called before the first frame update
    void Start()
    {
        myRenderer = gameObject.GetComponent<SpriteRenderer>();
        myBody = gameObject.GetComponent<Rigidbody2D>();
        
    }

    // Update is called once per frame
    void Update()
    {
        //returns true once
        
    }

    void FixedUpdate() {
        //physics code runs in fixed update instead of update!

        //make on floor false if our velocity is at a certain rate
        if(onFloor && myBody.velocity.y > 0) {
            onFloor = false; 
        }
        CheckKeys();
        HandleMovement();
        JumpPhysics();


    }

    void CheckKeys() {

        if (Input.GetKey(KeyCode.D)) {

            MoveDirection = 1;
        }

        else if (Input.GetKey(KeyCode.A)) {

            MoveDirection = -1;
        }

        else {

            MoveDirection = 0;
        }

        if (Input.GetKey(KeyCode.W) && onFloor) {

            //we want to keep the x velocity as is
            //but change y value thru jump height
            myBody.velocity = new Vector3(myBody.velocity.x, jumpHeight);
            //if we are not pressing W or on the floor 
        } else if(!Input.GetKey(KeyCode.W) && myBody.velocity.y > 0) {
            //little boost, then slow down 
            myBody.velocity += Vector2.up * Physics.gravity.y * (jumpMultiplier - 1f) * Time.deltaTime; 
        }

    }

    void JumpPhysics() {
        //if we are in descent
        if(myBody.velocity.y < 0) {
            myBody.velocity += Vector2.up * Physics.gravity.y * (GravityMultiplier - 1f) * Time.deltaTime;
        }
    }

    void HandleMovement() {

        //only messing w/ x values
        myBody.velocity = new Vector3(MoveDirection * speed, myBody.velocity.y);


    }

    void OnCollisionEnter2D(Collision2D collisionInfo) {

        //tag for group of objects
        if(collisionInfo.gameObject.tag == "Floor") {

            onFloor = true;
        }
        
        

        
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.name == "orangemushroom") {
            //myRenderer.color = gateColour; 
            Debug.Log("picked up orange mushroom");
            Destroy(other.gameObject);
            Destroy(orangegate);
        }

        if (other.gameObject.name == "redmushroom") {
            //myRenderer.color = gateColour; 
            Debug.Log("picked up red mushroom");
            Destroy(other.gameObject);
            Destroy(reddoor2);
            Destroy(redgate);
        }

         if (other.gameObject.name == "greenmushroom") {
            //myRenderer.color = gateColour; 
            Debug.Log("picked up green mushroom");
            Destroy(other.gameObject);
            Destroy(greengate);
        }

        if (other.gameObject.name == "redkey") {
            //myRenderer.color = gateColour; 
            Debug.Log("picked up green mushroom");
            Destroy(other.gameObject);
            Destroy(reddoor1);
        }

        
    }
    }
    


