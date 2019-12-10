using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class J_CharacterController : MonoBehaviour
{
    // Control Mode enum type
    enum ControlMode
    {
        NONE = 0,
        POSITION,
        VELOCITY,
        ACCELERATION,
        TARGETVELOCITY_LERP
    }

    // Control Mode
    [SerializeField]
    private ControlMode currentControlMode = ControlMode.NONE;

    // Input Manager
    Managers.s_InputManager IM;

    // Rigidbody
    Rigidbody rb = null;

    // Movement Varaibles
    [SerializeField]
    private float positionSpeed = 0.0f;
    [SerializeField]
    private float velocitySpeed = 0.0f;
    [SerializeField]
    private float accelerationSpeed = 0.0f;
    [SerializeField]
    private float targetVelocitySpeed = 0.0f;

    // DeadZones
    private float joystickDeadzone = 0.0f;

    void Awake()
    {
        // Set Rigidbody
        rb = this.GetComponent<Rigidbody>();
        // Init Input Manager
        IM = Managers.s_InputManager.Instance;
    }

    void Update()
    {
        UpdateMovement();
    }

    // Movement Updates
    //-------------------------------------------------------------------//

    void UpdateMovement()
    {
        switch (currentControlMode)
        {
            case ControlMode.NONE:
                break;
            case ControlMode.POSITION:
                UpdateMovement_Position();
                break;
            case ControlMode.VELOCITY:
                UpdateMovement_Velocity();
                break;
            case ControlMode.ACCELERATION:
                UpdateMovement_Acceleration();
                break;
            case ControlMode.TARGETVELOCITY_LERP:
                UpdateMovement_TargetVelocity_Lerp();
                break;
            default:
                Debug.LogError("currentControlMode not set in J_CharacterController");
                break;
        }
    }

    void UpdateMovement_Position()
    {
        // Horizontal
        if (IM.GetKey(KeyCode.D) || IM.GetKey(KeyCode.RightArrow)) // Right Input
            rb.position = new Vector3(rb.position.x + positionSpeed, rb.position.y, rb.position.z);
        else if (IM.GetKey(KeyCode.A) || IM.GetKey(KeyCode.LeftArrow)) // Left Input
            rb.position = new Vector3(rb.position.x - positionSpeed, rb.position.y, rb.position.z);

        // Vertical
        if (IM.GetKey(KeyCode.W) || IM.GetKey(KeyCode.UpArrow)) // Forward Input
            rb.position = new Vector3(rb.position.x, rb.position.y, rb.position.z + positionSpeed);
        else if(IM.GetKey(KeyCode.S) || IM.GetKey(KeyCode.DownArrow))
            rb.position = new Vector3(rb.position.x, rb.position.y, rb.position.z - positionSpeed);

    }

    void UpdateMovement_Velocity()
    {
        // Horizontal
        if (IM.GetKey(KeyCode.D) || IM.GetKey(KeyCode.RightArrow)) // Right Input
            rb.velocity = new Vector3(positionSpeed, rb.velocity.y, rb.velocity.z);
        else if (IM.GetKey(KeyCode.A) || IM.GetKey(KeyCode.LeftArrow)) // Left Input
            rb.velocity = new Vector3(-velocitySpeed, rb.velocity.y, rb.velocity.z);

        // Vertical
        if (IM.GetKey(KeyCode.W) || IM.GetKey(KeyCode.UpArrow)) // Forward Input
            rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, velocitySpeed);
        else if(IM.GetKey(KeyCode.S) || IM.GetKey(KeyCode.DownArrow))
            rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, -velocitySpeed);
    }

    void UpdateMovement_Acceleration()
    {
        // Horizontal
        if (IM.GetKey(KeyCode.D) || IM.GetKey(KeyCode.RightArrow)) // Right Input
            rb.AddForce(Vector3.right * accelerationSpeed, ForceMode.Acceleration);
        else if (IM.GetKey(KeyCode.A) || IM.GetKey(KeyCode.LeftArrow)) // Left Input
            rb.AddForce(Vector3.left * accelerationSpeed, ForceMode.Acceleration);

        // Vertical
        if (IM.GetKey(KeyCode.W) || IM.GetKey(KeyCode.UpArrow)) // Forward Input
            rb.AddForce(Vector3.forward * accelerationSpeed, ForceMode.Acceleration);
        else if(IM.GetKey(KeyCode.S) || IM.GetKey(KeyCode.DownArrow))
            rb.AddForce(Vector3.back * accelerationSpeed, ForceMode.Acceleration);
    }

    void UpdateMovement_TargetVelocity_Lerp()
    {

    }

    // Input
    //-------------------------------------------------------------------//

    float GetHorizontalInput()
    {
        if (Mathf.Abs(Input.GetAxis("Horizontal")) < joystickDeadzone)
            return 0;
        return Input.GetAxis("Horizontal");
    }

    float GetVerticalInput()
    {
        if (Mathf.Abs(Input.GetAxis("Vertical")) < joystickDeadzone)
            return 0;
        return Input.GetAxis("Vertical");
    }

    // Getter & Setters
    //-------------------------------------------------------------------//

    void SetCurrentControlMode(ControlMode controlMode)
    {
        currentControlMode = controlMode;
    }

    ControlMode GetCurrentControlMode()
    {
        return currentControlMode;
    }
}
