using UnityEngine;
using System.Collections;

public class PositionLerp : MonoBehaviour {

	// 2 public transforms, with one private vector3 each. One for on-state position and one for off-state position.
    public Transform positionOn;
	private Vector3 vectorOn;
	public Transform positionOff;
	private Vector3 vectorOff;

	// Variable for target position (where the object should be).
	private Vector3 targetPosition;

	// Bool for if the object should start as on or off.
    public bool beginInOnState;
	// Speed variable - how fast the object should move.
    public float lerpSpeed = 1;

    void Awake()
    {
		// Saves positons in a new variable, since if not the object keeps moving until end of time.
		vectorOn = positionOn.position;
		vectorOff = positionOff.position;

		if(beginInOnState == true)
		{
			// Changes what position the object should be in.
			targetPosition = vectorOn;
			// Changes where the object is sitting to target positon.
			transform.position = targetPosition;
        }
        else
        {
			// Changes what position the object should be in.
			targetPosition = vectorOff;
			// Changes where the object is sitting to target positon.
			transform.position = targetPosition;
        }

    }

	// What should happen 60 times per second?
	void FixedUpdate () 
    {
		// Changes the position of where the object is to where it should be.
		transform.position = Vector3.Lerp(transform.position, targetPosition, lerpSpeed * Time.deltaTime);
	}

	// Public function for changing if object should be on screen or not.
    public void showObject(bool state)
    {
		print (gameObject + " has state = " + state);
		// If state-parameter equals true we change objects position to on, if not we change it to off.
		if (state == true) {
			targetPosition = vectorOn;
		} else {
			targetPosition = vectorOff;
		}
    }

	// Public function for toggling what state the object is in.
    public void toggleState()
    {
		print ("Toggles state");
		// If target position is the same as the position for when object is on we change it to off-state, if not we change it to on-state.
		if (targetPosition == vectorOn) {
			targetPosition = vectorOff;
		} else {
			targetPosition = vectorOn;
		}
    }

}
