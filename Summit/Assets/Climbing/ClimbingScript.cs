using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ClimbingScript : MonoBehaviour
{
    private bool isClimbing = false;
    private Vector3 initialGrabPosition;
    private XRBaseInteractor leftInteractor;
    private XRBaseInteractor rightInteractor;
    private CharacterController characterController;
    public float climbingSpeed = 5f;

    private void Start()
    {
        // Find the left and right hand interactor objects
        leftInteractor = GameObject.Find("LeftHand").GetComponent<XRBaseInteractor>();
        rightInteractor = GameObject.Find("RightHand").GetComponent<XRBaseInteractor>();

        // Get the CharacterController component attached to the climbing object
        characterController = GetComponent<CharacterController>();

        // Subscribe to the selectEntered and selectExited events of the interactors
        leftInteractor.selectEntered.AddListener(StartClimbing);
        leftInteractor.selectExited.AddListener(StopClimbing);
        rightInteractor.selectEntered.AddListener(StartClimbing);
        rightInteractor.selectExited.AddListener(StopClimbing);
    }

    private void OnDestroy()
    {
        // Unsubscribe from the selectEntered and selectExited events of the interactors
        if (leftInteractor != null)
        {
            if (leftInteractor.selectEntered != null)
                leftInteractor.selectEntered.RemoveListener(StartClimbing);

            if (leftInteractor.selectExited != null)
                leftInteractor.selectExited.RemoveListener(StopClimbing);
        }

        if (rightInteractor != null)
        {
            if (rightInteractor.selectEntered != null)
                rightInteractor.selectEntered.RemoveListener(StartClimbing);

            if (rightInteractor.selectExited != null)
                rightInteractor.selectExited.RemoveListener(StopClimbing);
        }
    }

    private void StartClimbing(SelectEnterEventArgs args)
    {
        if (!isClimbing)
        {
            // Set isClimbing to true and store the initial grab position
            Debug.Log("Climbing");
            isClimbing = true;
            XRBaseInteractor interactor = (XRBaseInteractor)args.interactorObject;

            // Determine which interactor is grabbing and nullify the other one
            if (interactor == leftInteractor)
                rightInteractor = null;
            else if (interactor == rightInteractor)
                leftInteractor = null;

            initialGrabPosition = interactor.transform.position;
        }
    }

    private void StopClimbing(SelectExitEventArgs args)
    {
        if (isClimbing)
        {
            // Set the respective interactor to null and check if both interactors are null to stop climbing
            XRBaseInteractor interactor = (XRBaseInteractor)args.interactorObject;
            Debug.Log("Stop Climbing");
            if (interactor == leftInteractor)
                leftInteractor = null;
            else if (interactor == rightInteractor)
                rightInteractor = null;

            if (leftInteractor == null && rightInteractor == null)
                isClimbing = false;
        }
    }

    private void FixedUpdate()
    {
        if (isClimbing)
        {
            // Calculate the offset based on the movement of the interactors
            Vector3 offset = Vector3.zero;

            if (leftInteractor != null)
                offset += leftInteractor.transform.position - initialGrabPosition;

            if (rightInteractor != null)
                offset += rightInteractor.transform.position - initialGrabPosition;

            // Invert the offset to move in the opposite direction
            offset *= -1f;

            // Move the climbing object and update the initial grab position
            transform.position += offset;
            initialGrabPosition += offset;

            // Climb up or down based on the vertical movement of the hands
            float verticalMovement = GetVerticalMovement();
            if (verticalMovement != 0f)
                Climb(verticalMovement);
        }
    }

    private float GetVerticalMovement()
    {
        float verticalMovement = 0f;

        if (leftInteractor != null)
            verticalMovement += GetHandVerticalMovement(leftInteractor);

        if (rightInteractor != null)
            verticalMovement += GetHandVerticalMovement(rightInteractor);

        return verticalMovement;
    }

    private float GetHandVerticalMovement(XRBaseInteractor interactor)
    {
        Vector3 handPosition = interactor.transform.position;
        Vector3 initialPosition = initialGrabPosition;
        float verticalMovement = handPosition.y - initialPosition.y;

        return verticalMovement;
    }

    private void Climb(float verticalMovement)
    {
        Vector3 climbDirection = Vector3.up * verticalMovement * climbingSpeed * Time.fixedDeltaTime;
        characterController.Move(climbDirection);
    }
}
