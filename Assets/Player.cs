using UnityEngine;

public class Player : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            // shoot portal right
        } 
        
        else if (Input.GetKeyDown(KeyCode.Q)) {
            // shoot portal left
        }
    }
}