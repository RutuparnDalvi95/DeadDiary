using UnityEngine;
using UnityEngine.InputSystem;

public class DrawManagerPen : MonoBehaviour
{
    [SerializeField] private GameObject drawPrefab;
    private GameObject theTrail;
    private Plane planeObj;

    // Start is called before the first frame update
    void Start()
    {
        planeObj = new Plane(Camera.main.transform.forward * -1, transform.position);
        Debug.Log(planeObj.distance+" ,"+planeObj.normal);
    }

    // Update is called once per frame
    void Update()
    {
        if (Pen.current.press.wasPressedThisFrame && !DrawingData.isLoadCoroutineStarted)
        {
            theTrail = Instantiate(drawPrefab, transform.position, Quaternion.identity);
            theTrail.GetComponent<TrailManager>().enabled = true;

        }else if (Pen.current.press.isPressed && !DrawingData.isLoadCoroutineStarted)
        {
            Ray penRay = Camera.main.ScreenPointToRay(Pen.current.position.ReadValue());
            float _dis;
            if (planeObj.Raycast(penRay,out _dis))
            {
                theTrail.transform.position = penRay.GetPoint(_dis);
            }
        }
    }
}
