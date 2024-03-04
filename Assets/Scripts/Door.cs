using UnityEngine;

public class Door : MonoBehaviour {
    public GameObject doorObject;
    public bool isOpened;

    public float openHeightAmount;
    private Vector3 closePos;
    private Vector3 openPos;

    public float t;

    private void Start() {
        closePos = doorObject.transform.position;

        openPos = new Vector3(
            closePos.x,
            closePos.y + openHeightAmount,
            closePos.z
        );
    }


    private void Update()
    {
        /*
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (isOpened) CloseDoor();
            else OpenDoor();
        }
        */

        if (isOpened)
        {
            doorObject.transform.position = Vector3.Lerp(doorObject.transform.position, openPos, t * Time.deltaTime);
        } 
        
        else
        {
            doorObject.transform.position = Vector3.Lerp(doorObject.transform.position, closePos, t * Time.deltaTime);
        }
    }

    public void OpenDoor() {
        isOpened = true;
    }

    public void CloseDoor() {
        isOpened = false;
    }

}