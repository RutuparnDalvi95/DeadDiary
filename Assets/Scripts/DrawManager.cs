using UnityEngine;

public class DrawManager : MonoBehaviour
{
    [SerializeField] private GameObject drawPrefab;
    private GameObject theTrail;
    private Plane planeObj;
 
    // Start is called before the first frame update
    void Start()
    {
         
        planeObj = new Plane(Camera.main.transform.forward * -1, transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            theTrail = Instantiate(drawPrefab, transform.position, Quaternion.identity);
            
        }else if (Input.GetMouseButton(0))
        {
            Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            float _dis;
            if (planeObj.Raycast(mouseRay,out _dis))
            {
                theTrail.transform.position = mouseRay.GetPoint(_dis);
            }
        }
    }
}
