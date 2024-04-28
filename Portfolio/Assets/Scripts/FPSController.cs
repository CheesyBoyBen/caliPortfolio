using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class FPSController : MonoBehaviour
{
    public Camera playerCamera;
    public float walkSpeed = 6f;
    public float runSpeed = 12f;
    public float jumpPower = 7f;
    public float gravity = 10f;

    public float lookSpeed = 2f;
    public float lookXLimit = 45f;

    Vector3 moveDirection = Vector3.zero;
    float rotationX = 0;

    public bool canMove = true;
    GameObject heldScarecrow = null;

    public GameObject scarecrow;
    public int scarecrowCount;

    CharacterController characterController;

    public Animator anim;

    GameObject levelManager;


    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        levelManager = GameObject.FindGameObjectWithTag("GameController");

        scarecrowCount = levelManager.GetComponent<levelManagerScript>().totalScarecrows;
        levelManager.GetComponent<levelManagerScript>().updateText(scarecrowCount);

    }

    // Update is called once per frame
    void Update()
    {
        #region Handles Movement
        if (Cursor.lockState == CursorLockMode.Locked)
        {
            Vector3 forward = transform.TransformDirection(Vector3.forward);
            Vector3 right = transform.TransformDirection(Vector3.right);

            //Press Left Shift to run
            bool isRunning = Input.GetKey(KeyCode.LeftShift);
            float curSpeedX = canMove ? (isRunning ? runSpeed : walkSpeed) * Input.GetAxis("Vertical") : 0;
            float curSpeedY = canMove ? (isRunning ? runSpeed : walkSpeed) * Input.GetAxis("Horizontal") : 0;
            float movementDirectionY = moveDirection.y;
            moveDirection = (forward * curSpeedX) + (right * curSpeedY);

            #endregion

            anim.SetBool("Walking", false);
            anim.SetBool("Walking Back", false);


            if (Input.GetAxis("Vertical") > 0)
            {
                anim.SetBool("Walking", true);
            }
            else if (Input.GetAxis("Vertical") < 0)
            {
                anim.SetBool("Walking Back", true);

            }
            else
            {
                if (Input.GetAxis("Horizontal") > 0)
                {
                    anim.SetBool("Walking", true);
                }
                else if (Input.GetAxis("Horizontal") < 0)
                {
                    anim.SetBool("Walking", true);
                }
                else
                {
                    if (Random.Range(0, 100) == 100)
                    {
                        anim.SetTrigger("Random Trigger");

                    }
                }
            }
        }

        /*#region Handles Jumping
        if (Input.GetButton("Jump") && canMove && characterController.isGrounded)
        {
            moveDirection.y = jumpPower;
        }
        else
        {
            moveDirection.y = movementDirectionY;
        }

        if (!characterController.isGrounded)
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }

        #endregion*/

        #region Handles Rotation
        characterController.Move(moveDirection * Time.deltaTime);

        if ((canMove) && (Cursor.lockState == CursorLockMode.Locked))
        {
            rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
        }

        #endregion



        if ((Input.GetKeyDown(KeyCode.Mouse0)) && (heldScarecrow == null) && (Cursor.lockState == CursorLockMode.Locked))
        {
            if (scarecrowCount > 0)
            {
                GameObject sc = Instantiate(scarecrow, this.transform.GetChild(2).position, transform.rotation);
                scarecrowCount--;

                levelManager.GetComponent<levelManagerScript>().updateText(scarecrowCount);

            }
        }

        if (Cursor.lockState == CursorLockMode.Locked)
        {
            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                if (heldScarecrow == null)
                {
                    GameObject[] mirrors = GameObject.FindGameObjectsWithTag("Mirror");
                    GameObject[] scarecrows = GameObject.FindGameObjectsWithTag("Player");

                    if ((mirrors.Length > 0) || (scarecrows.Length > 1))
                    {
                        GameObject closest = null;

                        if (scarecrows.Length > 1)
                        {
                            closest = scarecrows[1];
                        }
                        if (mirrors.Length > 0)
                        {
                            closest = mirrors[0];
                        }


                        foreach (GameObject m in mirrors)
                        {
                            if ((transform.position - m.transform.position).magnitude < (transform.position - closest.transform.position).magnitude)
                            {
                                closest = m;
                            }
                        }
                        foreach (GameObject s in scarecrows)
                        {
                            if (s != this.gameObject)
                            {
                                if ((transform.position - s.transform.position).magnitude < (transform.position - closest.transform.position).magnitude)
                                {
                                    closest = s;
                                }
                            }
                        }

                        if ((transform.position - closest.transform.position).magnitude < 3)
                        {
                            if (closest.tag == "Mirror")
                            {
                                closest.GetComponent<MirrorScript>().turn();
                            }
                            if (closest.tag == "Player")
                            {

                                closest.GetComponent<ScarecrowScript>().PickUp();
                                heldScarecrow = closest;
                            }
                        }
                    }


                }
                else if (heldScarecrow != null)
                {
                    heldScarecrow.GetComponent<ScarecrowScript>().Drop();
                    heldScarecrow = null;
                }
            }

        }
    }


}
