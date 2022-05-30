using UnityEngine.InputSystem;
using UnityEngine;

public class FollowCameraPen : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        Vector3 temp = Pen.current.position.ReadValue();
        transform.position = Camera.main.ScreenToWorldPoint(temp);
    }
}
