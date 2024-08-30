using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrappleHook : MonoBehaviour
{

    public GameObject m_Options;
    public GameObject m_PauseMenuUI;
    public Transform playerPos;
    public GameObject Hook;
    private GameObject _Hook, currentOb, newOb;
    public Rigidbody2D playerRB;
    public Camera mainCam;
    public LineRenderer _lineRender;
    public DistanceJoint2D joint;
    public float RopeMaxLength = 10f;
    public float ropeSpeed = 5f;
    public float hookSpeed;

    public LayerMask _grappableEnviorment;
    private Vector2 mousePos, currentAnchor;
    private GameObject GrabbedObject;

    [SerializeField] private Transform[] hooks;
    private int hookCount = 0;

    private bool hooked = false;


    // Start is called before the first frame update
    void Start()
    {
        joint.enabled = false;
        _lineRender.enabled = false;    
    }

    // Update is called once per frame
    void Update()
    {
        //RaycastHit hit;
        //Ray directHit = Camera.main.ScreenPointToRay(Input.mousePosition);
        if((!m_Options.activeInHierarchy && !m_PauseMenuUI.activeInHierarchy ))
        {

            //if (Input.GetKeyDown(KeyCode.shift)) {
                // moved through the list
            //}

            if (Input.GetKeyDown(KeyCode.Mouse0)) {
            mousePos = (Vector2)mainCam.ScreenToWorldPoint(Input.mousePosition);
            Collider2D col = Physics2D.OverlapCircle(mousePos, 0.3f, _grappableEnviorment);
            newOb = col.gameObject;
            if(newOb != null)
                currentOb = newOb;
                currentAnchor = mousePos;
            //_Hook = Instantiate(Hook, playerPos.position, playerPos.rotation);
                if(_Hook) {
                    Destroy(_Hook);
                }
                startGrapple();
            }
            if ((Input.GetKeyDown(KeyCode.Mouse1) || Input.GetButtonDown("Jump")) && hooked){
                stopGrapple();
            }


            if (hooked) {
                DrawRope();
                if(Input.GetKey(KeyCode.W)) {
                    if(joint.distance > 1f)
                        joint.distance = joint.distance - (Time.deltaTime * ropeSpeed);
                }
                if(Input.GetKey(KeyCode.S)) {
                    if(joint.distance < RopeMaxLength)
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
    }

    /**/
    private void startGrapple() {
        //joint = playerPos.gameObject.AddComponent<DistanceJoint2D>();
        joint.autoConfigureConnectedAnchor = false;
        joint.connectedAnchor = (Vector2)currentOb.transform.position;
        _Hook = Instantiate(Hook, (Vector2)currentAnchor, Quaternion.identity);
        joint.distance = Vector2.Distance(currentAnchor, playerPos.position);
        joint.enabled = true;
        _lineRender.enabled = true;
        hooked = true;
    }

    private void stopGrapple() {
        Destroy(_Hook);
        joint.enabled = false;
        _lineRender.enabled = false;
        hooked = false;
    }

    void DrawRope() {
        _lineRender.SetPosition(0, mousePos);
        _lineRender.SetPosition(1, transform.position);
    }

    /**

    void OnTriggerEnter2D(Collider2D col) {
        if(hookCount < hook.Length) {
            hooks[hookCount] = col.transform.position;
            hookCount++;
        } else {
            for(int i = 0; i < hooks.Length; i++) {
            
            }
            hooks[hookCount - 1] = col.transform.position
        }
    }

    void OnTriggerExit2D(Collider2D col) {
        int shift the arrat
        for (int i = 0; i < hookCount; i++) {
            if(hook[i] == col.transform.postiion) {
            
            }
        }
        hooks[hookCount] = col.transform.position;
        hookCount--;
    }
    /**/

    /**
    void RotateGun(Vector3 lookPoint, bool allowRotationOverTime)
    {
        Vector3 distanceVector = lookPoint - gunPivot.position;

        float angle = Mathf.Atan2(distanceVector.y, distanceVector.x) * Mathf.Rad2Deg;
        if (rotateOverTime && allowRotationOverTime)
        {
            gunPivot.rotation = Quaternion.Lerp(gunPivot.rotation, Quaternion.AngleAxis(angle, Vector3.forward), Time.deltaTime * rotationSpeed);
        }
        else
        {
            gunPivot.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }

    private void grappleUp() {
        
    }

    private void grappleDown() {

    }
    /**/
}
