using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public struct ArrowHandler
{
    public Transform arrow;
    public float aimingSensitivity;
    public float maxYRotationClampAngle;
    public float maxXRotationClampAngle;
    [HideInInspector] public float currentArrowAngleYOffset;
    [HideInInspector] public float currentArrowAngleXOffset;
}

public enum BallState
{
    Aiming, Shoot, None
}
public class PlayerController : MonoBehaviour
{
    private Vector3 ballStartPosition;
    private Vector3 arrowStartAngle;

    [SerializeField] private BallState currentBallState = BallState.Aiming;
    [SerializeField] ArrowHandler arrowHandler;

    private void Start()
    {
        ballStartPosition = this.transform.position;
        arrowStartAngle = new Vector3(arrowHandler.arrow.eulerAngles.x - 360, arrowHandler.arrow.eulerAngles.y, arrowHandler.arrow.eulerAngles.z);

        arrowHandler.currentArrowAngleYOffset = arrowHandler.arrow.eulerAngles.y;
        arrowHandler.currentArrowAngleXOffset = arrowHandler.arrow.eulerAngles.x + 360;

    }
    private void Update()
    {
        ExecuteStates();
    }
    void ExecuteStates()
    {
        switch (currentBallState)
        {
            case BallState.Aiming:
                SetAim();
                break;
            case BallState.Shoot:
                Shoot();
                break;
        }
    }
    void SetAim()
    {
        //Set the arrow direction according to input
        if (Input.GetKey(KeyCode.LeftArrow) && arrowHandler.arrow.eulerAngles.y > arrowHandler.currentArrowAngleYOffset - arrowHandler.maxYRotationClampAngle)
            RotateToAim(Vector3.down * arrowHandler.aimingSensitivity * Time.deltaTime);
        else if (Input.GetKey(KeyCode.RightArrow) && arrowHandler.arrow.eulerAngles.y < arrowHandler.currentArrowAngleYOffset + arrowHandler.maxYRotationClampAngle)
            RotateToAim(Vector3.up * arrowHandler.aimingSensitivity * Time.deltaTime);

        else if (Input.GetKey(KeyCode.UpArrow) && arrowHandler.arrow.eulerAngles.x > 335)
            RotateToAim(Vector3.left * arrowHandler.aimingSensitivity * Time.deltaTime);

        else if (Input.GetKey(KeyCode.DownArrow) && arrowHandler.arrow.eulerAngles.x < 355)
            RotateToAim(Vector3.right * arrowHandler.aimingSensitivity * Time.deltaTime);

        //if aim set, then next shot it
        if (Input.GetKeyDown(KeyCode.Space))
            currentBallState = BallState.Shoot;
    }
    void RotateToAim(Vector3 toRotate)
    {
        arrowHandler.arrow.transform.Rotate(toRotate);
    }
    void Shoot()
    {
        this.GetComponent<Rigidbody>().velocity = arrowHandler.arrow.transform.forward * 50f;
        currentBallState = BallState.None;
        Invoke("ResetPlayerState", 2f);
    }
    void ResetPlayerState()
    {
        this.GetComponent<Rigidbody>().velocity = Vector3.zero;
        this.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        arrowHandler.arrow.eulerAngles = arrowStartAngle;
        this.transform.position = ballStartPosition;
        currentBallState = BallState.Aiming;
    }

}
