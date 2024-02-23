using UnityEngine

public class Door : MonoBehaviour {
// i am eriting thus code on my ipad and cant test this. changes will most likely havw to be made

public GameObject doorObject;
public bool isOpened;

public float openHeightAmount;
private Vector3 closePos;
private Vector3 openPos;

private void Start() {
    closePos = doorObject.transform.position;

    openPos = new Vector3(
        closePos.x,
        closePos.y + openHeightAmount,
        closePos.z
    );
}

public void OpenDoor() {
isOpened = true;
doorObject.transform.position = Vector3.Lerp(doorObject.transform.position, openPos);
}

public void CloseDoor() {
isOpened = false;
doorObject.transform.position = Vector3.Lerp(doorObject.transform.position, closePos);
}

}