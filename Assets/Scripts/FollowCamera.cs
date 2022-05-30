using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    
    // Update is called once per frame
    void Update()
    {
        Vector3 temp = Input.mousePosition;
        transform.position = Camera.main.ScreenToWorldPoint(temp);
    }
}
