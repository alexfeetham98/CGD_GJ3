using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFollow : MonoBehaviour
{
    public Transform player;
    public float heading = 0;
    public float tilt = 30;
    public float camDistance = 10;
    float rotationSpeed = 180;
    public float playerHalfHeight = 1.3f;
    Vector3 camPos;
    Vector3 playerPos;
    public Movement playerScript;

    //90 DEGREE CAMERA ROTATION
    int direction = 0;
    int south = 0;
    int west = 1;
    int north = 2;
    int east = 3;
    float absoluteEuler;
    float specialCase;

    //CAMERA RESET
    float resetTimer = 0;
    float slowTimer = 0;
    bool autoRotate = false;

    bool leftInput = false;
    bool rightInput = false;
    float timer;
    public bool FixedCam = false;

    private void OnEnable()
    {
        heading = transform.rotation.eulerAngles.y;
    }

    void LateUpdate()
    {
        if(!FixedCam)
        {
            if (!leftInput && !rightInput && !autoRotate)
            {
                heading += Input.GetAxis("Mouse X") * Time.deltaTime * rotationSpeed;
                heading += Input.GetAxis("RightStickHorizontal") * Time.deltaTime * rotationSpeed;
            }

            tilt += Input.GetAxis("RightStickVertical") * Time.deltaTime * rotationSpeed;
            tilt += Input.GetAxis("Mouse Y") * Time.deltaTime * rotationSpeed;
            tilt = Mathf.Clamp(tilt, 0, 89);
        }
      

        transform.rotation = Quaternion.Euler(tilt, heading, 0);

        transform.position = player.position - transform.forward * camDistance + Vector3.up * playerHalfHeight;

        if (heading > 360)
        {
            heading = 0;
        }
        else if (heading < 0)
        {
            heading = 360;
        }


        horizontalRotation();

        compensateForWalls();

        resetToBehind();
    }

    void compensateForWalls()
    {

        playerPos = player.position - transform.forward * 0.3f + Vector3.up * 3.5f * playerHalfHeight;
        camPos = this.transform.position;
        Debug.DrawLine(playerPos, camPos, Color.cyan);

        RaycastHit wallHit = new RaycastHit();
        if (Physics.Linecast(playerPos, camPos, out wallHit))
        {
            Debug.DrawRay(wallHit.point, Vector3.left, Color.red);

            camPos = new Vector3(wallHit.point.x, wallHit.point.y, wallHit.point.z);
            transform.position = camPos;
            transform.LookAt(player.position + Vector3.up * playerHalfHeight);
        }

    }

    void horizontalRotation()
    {

        absoluteEuler = transform.eulerAngles.y % 90;
        specialCase = absoluteEuler;
        if (absoluteEuler < 0.1)
        {
            specialCase = 90;
        }



        if (timer < 1.2f)
        {
            timer += 2 * Time.deltaTime;
        }
        else
        {
            timer = 1.2f;
        }


        if ((Input.GetButtonDown("Right90") || Input.GetAxis("DPadHorizontal") > 0) && timer >= 1.2)
        {
            timer = 0f;

            direction++;
            if (direction == 4)
            {
                direction = 0;
            }
            leftInput = true;
            rightInput = false;

        }

        if ((Input.GetButtonDown("Left90") || Input.GetAxis("DPadHorizontal") < 0) && timer >= 1.2)
        {
            timer = 0f;

            direction--;
            if (direction == -1)
            {
                direction = 3;
            }
            leftInput = false;
            rightInput = true;
        }

        if (direction == south && leftInput)
        {
            transform.RotateAround(player.position + Vector3.up * playerHalfHeight, Vector3.up, Mathf.SmoothStep(0, (90 - absoluteEuler), timer));


            if (timer >= 1)
            {
                heading = transform.rotation.eulerAngles.y;
                leftInput = false;

            }

        }
        else if (direction == west && leftInput)
        {
            transform.RotateAround(player.position + Vector3.up * playerHalfHeight, Vector3.up, Mathf.SmoothStep(0, (90 - absoluteEuler), timer));

            if (timer >= 1)
            {
                heading = transform.rotation.eulerAngles.y;
                leftInput = false;
            }

        }
        else if (direction == east && leftInput)
        {
            transform.RotateAround(player.position + Vector3.up * playerHalfHeight, Vector3.up, Mathf.SmoothStep(0, (90 - absoluteEuler), timer));

            if (timer >= 1)
            {
                heading = transform.rotation.eulerAngles.y;
                leftInput = false;
            }
        }
        else if (direction == north && leftInput)
        {
            transform.RotateAround(player.position + Vector3.up * playerHalfHeight, Vector3.up, Mathf.SmoothStep(0, (90 - absoluteEuler), timer));

            if (timer >= 1)
            {
                heading = transform.rotation.eulerAngles.y;
                leftInput = false;
            };
        }
        else if (direction == south && rightInput)
        {
            transform.RotateAround(player.position + Vector3.up * playerHalfHeight, Vector3.up, Mathf.SmoothStep(0, -specialCase, timer));

            if (timer >= 1)
            {
                heading = transform.rotation.eulerAngles.y;
                rightInput = false;
            };
        }
        else if (direction == west && rightInput)
        {
            transform.RotateAround(player.position + Vector3.up * playerHalfHeight, Vector3.up, Mathf.SmoothStep(0, -specialCase, timer));

            if (timer >= 1)
            {
                heading = transform.rotation.eulerAngles.y;
                rightInput = false;
            };
        }
        else if (direction == east && rightInput)
        {
            transform.RotateAround(player.position + Vector3.up * playerHalfHeight, Vector3.up, Mathf.SmoothStep(0, -specialCase, timer));

            if (timer >= 1)
            {
                heading = transform.rotation.eulerAngles.y;
                rightInput = false;
            };
        }
        else if (direction == north && rightInput)
        {
            transform.RotateAround(player.position + Vector3.up * playerHalfHeight, Vector3.up, Mathf.SmoothStep(0, -specialCase, timer));

            if (timer >= 1)
            {
                heading = transform.rotation.eulerAngles.y;
                rightInput = false;
            };
        }

    }

    void resetToBehind()
    {
        if (playerScript.input.magnitude == 0 && !Input.anyKey && Input.GetAxis("DPadHorizontal") == 0 &&
            Input.GetAxis("RightStickVertical") == 0 && Input.GetAxis("Mouse Y") == 0
            && Input.GetAxis("Mouse X") == 0 && Input.GetAxis("RightStickHorizontal") == 0)
        {
            if ((absoluteEuler < 89.9f || absoluteEuler > 0.1f) && resetTimer < 4.0f)
            {
                resetTimer += Time.deltaTime;
            }

        }
        else
        {
            resetTimer = 0;
            autoRotate = false;
        }

        if (resetTimer >= 4.0f && !autoRotate)
        {
            slowTimer = 0;
            autoRotate = true;


        }

        if (slowTimer >= 1)
        {
            slowTimer = 1;
        }



        if (autoRotate)
        {
            slowTimer += Time.deltaTime;

            if (absoluteEuler > 44.9 && absoluteEuler < 90.1)
            {
                transform.RotateAround(player.position + Vector3.up * playerHalfHeight, Vector3.up, Mathf.SmoothStep(0, (90 - absoluteEuler), slowTimer));

                if (slowTimer >= 1)
                {
                    heading = transform.rotation.eulerAngles.y;

                    autoRotate = false;
                    resetTimer = 0;

                }
            }
            else if (absoluteEuler < 44.9 && absoluteEuler > 0.1)
            {
                transform.RotateAround(player.position + Vector3.up * playerHalfHeight, Vector3.up, Mathf.SmoothStep(0, -specialCase, slowTimer));

                if (slowTimer >= 1)
                {
                    heading = transform.rotation.eulerAngles.y;

                    autoRotate = false;
                    resetTimer = 0;
                }
            }
        }

    }

}