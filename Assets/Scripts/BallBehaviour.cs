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

public GameObject gate1;
public GameObject gate2;
public GameObject gate3;
public GameObject minigate1;
public GameObject minigate2;

//private bool reachedPos = true;
//private bool stopMovement = false;
//private Vector3 dir; 
private Vector3 nextPos; 
public float sightDist; 
public LayerMask SignLayer; 

public GameManager gameManager; 






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

        //struct; creates a ray in particular direction
        //created a layer for the sign object
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.TransformDirection(Vector2.right), sightDist, SignLayer);
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector2.right), Color.green);
        //if we hit something
        if (hit.collider != null) {
            
            Debug.Log("hit sign");

            gameManager.ShowPopText("In a dream I see a world reflected, as if cast on water's surface. Where up is down, and down is up: heaven at the ground and hell in the sky, all else ripples between.");
            





            //if (hit.collider.tag == "Sign") {
          
                
                
           // }

        }
        else {

             
        }
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

            MoveDirection = -1;
        }

        else if (Input.GetKey(KeyCode.A)) {

            MoveDirection = 1;
        }

        else {

            MoveDirection = 0;
        }

        if (Input.GetKey(KeyCode.S) && onFloor) {

            //we want to keep the x velocity as is
            //but change y value thru jump height
            myBody.velocity = new Vector3(myBody.velocity.x, jumpHeight);
            //if we are not pressing W or on the floor 
        } else if(!Input.GetKey(KeyCode.S) && myBody.velocity.y > 0) {
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
        if (other.gameObject.name == "firstkey") {
            //myRenderer.color = gateColour; 
            Debug.Log("picked up first level key");
            Destroy(other.gameObject);
            Destroy(gate1);
        }

        if (other.gameObject.name == "secondkey") {
            //myRenderer.color = gateColour; 
            Debug.Log("picked up second level key");
            Destroy(other.gameObject);
            Destroy(gate2);
            
        }

         if (other.gameObject.name == "bluekey") {
            //myRenderer.color = gateColour; 
            Debug.Log("picked up key for mini gate");
            Destroy(other.gameObject);
            Destroy(minigate1);
        }

        if (other.gameObject.name == "thirdkey") {
            //myRenderer.color = gateColour; 
            Debug.Log("picked up third level key");
            Destroy(other.gameObject);
            Destroy(minigate2);
            Destroy(gate3);
        }

        
    }
    }
    


