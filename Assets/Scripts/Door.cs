using UnityEngine

public class Door : MonoBehaviour {
// i am eriting thus code on my ipad and cant test this. changes will most likely havw to be made

public GameObject doorObject;
public bool isOpened;

public void OpenDoor() {
isOpened = true;
}

public void CloseDoor() {
isOpened = false;
}

}