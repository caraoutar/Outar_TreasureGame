using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallBehaviour : MonoBehaviour {

public SpriteRenderer myRenderer;
public SpriteRenderer cloud1Renderer;
public SpriteRenderer cloud1reflectRenderer;
public SpriteRenderer platform1Renderer;
public SpriteRenderer platform2Renderer;
public SpriteRenderer platform3Renderer;
public SpriteRenderer platform4Renderer;
public SpriteRenderer cloud2Renderer;
public SpriteRenderer cloud2reflectRenderer;
public SpriteRenderer cloud3Renderer;
public SpriteRenderer cloud3reflectRenderer;
public Color floorColour;
public Color FireColour;
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
public GameObject greykeytransparent; 
public GameObject redkeytransparent;
public GameObject redkey;

public GameObject cloudlvl1;
public GameObject cloudlvl1reflect; 
public GameObject secondcloudlvl1;
public GameObject secondcloudlvl1reflect; 

//platform game objects

public GameObject cloud1;
public GameObject cloud1reflection;
public GameObject platform1;
public GameObject platform2;
public GameObject platform3;
public GameObject platform4;
public GameObject cloud2;
public GameObject cloud2reflection;
public GameObject cloud3;
public GameObject cloud3reflection;

public GameObject arrow; 
public GameObject arrow2;

//private bool reachedPos = true;
//private bool stopMovement = false;
//private Vector3 dir; 
private Vector3 nextPos; 
public float sightDist; 
public LayerMask SignLayer; 
public LayerMask SignLayer2;

public GameManager gameManager; 

public bool cloud1check = false;
public bool cloud2check = false;
public bool cloud3check = false;

public bool keyactive = false;



public float power;


    // Start is called before the first frame update
    void Start()
    {
        myRenderer = gameObject.GetComponent<SpriteRenderer>();
        myBody = gameObject.GetComponent<Rigidbody2D>();

        cloud1Renderer = cloud1.GetComponent<SpriteRenderer>();
        cloud1reflectRenderer = cloud1reflection.GetComponent<SpriteRenderer>();
        cloud2Renderer = cloud2.GetComponent<SpriteRenderer>();
        cloud2reflectRenderer = cloud2reflection.GetComponent<SpriteRenderer>();
        cloud3Renderer = cloud3.GetComponent<SpriteRenderer>();
        cloud3reflectRenderer = cloud3reflection.GetComponent<SpriteRenderer>();
        platform1Renderer = platform1.GetComponent<SpriteRenderer>();
        platform2Renderer = platform2.GetComponent<SpriteRenderer>();
        platform3Renderer = platform3.GetComponent<SpriteRenderer>();
        platform4Renderer = platform4.GetComponent<SpriteRenderer>();


        
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

        }

        CheckCloudColours();
       
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
         //if (collisionInfo.gameObject.name == "picketsign (2)") {

           // Debug.Log("hit sign 2");
           // gameManager.ShowPopText("Traverse the clouds and turn them all gold to acquire the key for the next floor. Beware of touching fire, or the clouds will turn back to normal.");
        //}

        if(collisionInfo.gameObject.tag == "Floor") {

            onFloor = true;
        }

        if(collisionInfo.gameObject.tag == "Floor") {

            myRenderer.color = floorColour;
            cloud1.SetActive(true);
            platform1.SetActive(true);
            platform2.SetActive(true);
            platform3.SetActive(true);
            platform4.SetActive(true);
            cloud2.SetActive(true);
            cloud3.SetActive(true);
        }

       

        if (collisionInfo.gameObject.name == "floor (4)") {

            Debug.Log("collided w first platform");

            //cloud1.SetActive(false); 


    } 

    if (collisionInfo.gameObject.name == "platform (12)") {
            cloud1Renderer.color = yellowColour;
            cloud1reflectRenderer.color = yellowColour;
            cloud1check = true; 
    }


    if (collisionInfo.gameObject.name == "bigcloudopaque") {
            cloud3Renderer.color = yellowColour;
            cloud3reflectRenderer.color = yellowColour;
            cloud3check = true; 
    }

    if (collisionInfo.gameObject.name == "floor (26)") {

            //platform1.SetActive(false); 

    } 

    if (collisionInfo.gameObject.name == "floor (27)") {

            //platform2.SetActive(false); 

    } 

    if (collisionInfo.gameObject.name == "floor (28)") {

            //platform3.SetActive(false); 

    } 

    if (collisionInfo.gameObject.name == "smallcloudopaque") {

            cloud2Renderer.color = yellowColour;
            cloud2reflectRenderer.color = yellowColour;
            cloud2check = true; 

    } 

    

        
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.name == "firstkey") {
            //myRenderer.color = gateColour; 
            Debug.Log("picked up first level key");
            Destroy(other.gameObject);
            Destroy(gate1);
            Destroy(greykeytransparent);
            Destroy(cloudlvl1);
            Destroy(cloudlvl1reflect);
            Destroy(secondcloudlvl1);
            Destroy(secondcloudlvl1reflect); 

            arrow.SetActive(true);
            arrow2.SetActive(true);

        }

        if (other.gameObject.name == "secondkey") {
            //myRenderer.color = gateColour; 
            Debug.Log("picked up second level key");
            Destroy(other.gameObject);
            Destroy(gate2);
            Destroy(redkeytransparent);
            
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

        if(other.gameObject.tag == "Fire") {
            Debug.Log("fire");
            myRenderer.color = FireColour;
            cloud1.SetActive(true);
            platform1.SetActive(true);
            platform2.SetActive(true);
            platform3.SetActive(true);
            platform4.SetActive(true);
            cloud2.SetActive(true);
            cloud3.SetActive(true);

            cloud1Renderer.color = floorColour;
            cloud1reflectRenderer.color = floorColour;
            cloud1check = false; 

            cloud2Renderer.color = floorColour;
            cloud2reflectRenderer.color = floorColour;
            cloud2check = false; 

            cloud3Renderer.color = floorColour;
            cloud3reflectRenderer.color = floorColour;
            cloud3check = false; 

        }

        if(other.gameObject.name == "picketsign (2)") {

        Debug.Log("hit sign 2");
        gameManager.ShowPopText("Traverse the clouds and turn them all gold to acquire the key for the next floor. Beware of touching fire, or the clouds will turn back to normal.");
        }

        if (other.gameObject.name == "ocean") {

        myRenderer.color = blueColour; 
    }
        

    }

    void CheckCloudColours() {

        redkey.SetActive(false);

        if (cloud1check == true && cloud2check == true && cloud3check == true) {

            Debug.Log("all clouds have been collided with");
            redkey.SetActive(true);
        }
    }
}
    


