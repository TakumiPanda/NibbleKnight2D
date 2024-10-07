using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewPlayerMovement : MonoBehaviour
{
    [Header("Menu options")]
    [SerializeField]private GameObject m_Options;
    [SerializeField]private GameObject m_PauseMenuUI;

    [Header("Movement Parameters")]
    [SerializeField]private float speed;
    [SerializeField]private float jumpPower;

    [Header("Jump")]
    [SerializeField]private float coyoteTime;
    [SerializeField]private float coyoteCounter;
    [SerializeField]private float wallJumpx;
    [SerializeField]private float wallJumpy;
    private bool isGrounded = false;

    [Header("Layers")]
    [SerializeField]private LayerMask groundLayer;
    [SerializeField]private LayerMask wallLayer;
    private Rigidbody2D body;
    private CapsuleCollider2D capsuleCollider;
    private float wallJumpCooldown;
    private float horizontalInput;

    [Header("Grapple Hook")]
    public GameObject player;
    public GameObject Hook; // refrenced hook
    private GameObject _Hook, currentOb, newOb; //instiantiated hook to destroy
    public Camera mainCam;
    public LineRenderer _lineRender;
    public DistanceJoint2D joint; // player to an object
    private DistanceJoint2D moveingJoint; // object to a player
    public float RopeMaxLength = 10f;
    public float ropeSpeed = 5f;
    //public float hookSpeed; //when we animate the line, this will be used

    public LayerMask _grappableEnviorment;
    private Vector2 mousePos, currentAnchor;
    private string obTag;
    private GameObject GrabbedObject;

    [SerializeField] private Transform[] hooks;
    private int hookCount = 0;
    private bool isHooked;    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        
        //Flip Player when moving left or right
        if (horizontalInput > 0.01f )
        {
            transform.localScale = Vector3.one;
        }
        else if (horizontalInput < -0.01f )
        {
            transform.localScale = new Vector3 (-1, 1, 1);
        }

        //if _hook, flip compared to player pos

        // ###############
        // # Jump Update #
        // ###############

        //jump
        if (Input.GetKeyDown(KeyCode.Space)) {
            if(isGrounded && (coyoteCounter != 0)) 
                jump();   
        } 

        //Coyote time
        if(!isGrounded) {
            if(coyoteCounter > 0) 
            coyoteCounter -= Time.deltaTime;
        }

        //ground colision
        //if(groundhitbox) { Grounded }

        //wall jump
        //if(wall) {wall slide, wall slide animation
        //  if jump, jump away
        //} 

        // ##################
        // # Grapple Update #
        // ##################

        //Grab
        if (Input.GetKeyDown(KeyCode.Mouse0)) {
            mousePos = (Vector2)mainCam.ScreenToWorldPoint(Input.mousePosition);
            Collider2D col = Physics2D.OverlapCircle(mousePos, 0.3f, _grappableEnviorment);
            newOb = col.gameObject;
            if(newOb.layer == _grappableEnviorment && newOb != currentOb) {
                if(_Hook) {
                    stopGrapple();
                }
                obTag = newOb.tag;
                currentOb = newOb;
                currentAnchor = newOb.transform.position;
                _Hook = Instantiate(Hook, player.position, player.rotation);
                startGrapple();
            }
        }

        // releace grab
        if ((Input.GetKeyDown(KeyCode.Mouse1) || (Input.GetButtonDown("Jump") && (obTag != "Object"))) && hooked) {
            // releace grapple if player right clicks. or jumps on a non grapple object
            stopGrapple();
        }

        // grabbing an object
        if(obTag == "Object") {
            moveingJoint.connectedAnchor = (Vector2)transform.position;
            _Hook.transform.position = currentOb.transform.position;
        }

        // draws a rope between you and ob. adjusts length of rope
        if (hooked) {
            DrawRope();
            if(Input.GetKey(KeyCode.W)) {
                if(joint.distance > 1f)
                    if(obTag == "Object")
                        moveingJoint.distance = moveingJoint.distance - (Time.deltaTime * ropeSpeed);
                    else
                        joint.distance = joint.distance - (Time.deltaTime * ropeSpeed);
            }
            if(Input.GetKey(KeyCode.S)) {                
                if(joint.distance < RopeMaxLength)
                    if(obTag == "Object")
                        moveingJoint.distance = moveingJoint.distance + (Time.deltaTime * ropeSpeed);
                    else
                        joint.distance = joint.distance + (Time.deltaTime * ropeSpeed);
            }
        }
        else {
            Vector2 mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
            //RotateGun(mousePos, true);
        }
        
        if(joint.enabled) {
            _lineRender.SetPosition(1, transform.position);
        }
    }

    void LateUpdate() {
        DrawRope();
    }

    // ########
    // # jump #
    // ########

    private void jump()
    {
        body.velocity = new Vector2(body.velocity.x, jumpPower);
    }

    // ###########
    // # grapple #
    // ###########

    void DrawRope() {
        if(!joint) return;

        Debug.Log("Line");

        _lineRender.SetPosition(0, player.position); //player
        _lineRender.SetPosition(1, _grabPoint); // object
    }

    void startGrapple() {
        RaycastHit hit;
        Ray directHit = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.Log("pew");
        //UnityEngine.Physics.Raycast(directHit, out hit, maxDistance, _grappable);
        //Debug.Log(hit.point);

        //add in additional logic if the tag is either layer
        //envior will link player to ob. the other will link the ob to player
        if (UnityEngine.Physics.Raycast(directHit, out hit, maxDistance, _grappableEnviorment)) {
            Debug.Log("catch");
            _grabPoint = hit.point;
            _grabPoint.z = 0f;
            joint = player.gameObject.AddComponent<SpringJoint>();
            joint.autoConfigureConnectedAnchor = false;
            joint.connectedAnchor = _grabPoint;
            joint.connectedBody = grappleSource;

            float toGrabPoint = Vector3.Distance(player.position, _grabPoint);
            joint.maxDistance = toGrabPoint * 0.4f;
            joint.minDistance = toGrabPoint * 0.3f;

            /**/
            joint.spring = 10f;
            joint.damper = 3f;
            joint.massScale = 4f;
            /**/

            _lineRender.positionCount = 2;
        }

        //function to stop the player streatching the rope to far
    }
    /**/
    void stopGrapple() {
        _lineRender.positionCount = 0;
        Destroy(joint);
    }

}

