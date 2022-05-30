using System.Collections;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.InputSystem;

public class DrawingData : MonoBehaviour
{
    private static SaveDrawing _save;
    public static bool isLoadCoroutineStarted;
    public static int trailCount=0;
    [SerializeField] private GameObject loadDrawPrefab;
    private GameObject theTrail;
    // Start is called before the first frame update
    void Awake()
    {
        _save = new SaveDrawing();
    }

    private void Update()
    {
        if (Keyboard.current.sKey.wasPressedThisFrame)
        {
            SaveDrawing();  
        } else if (Keyboard.current.lKey.wasPressedThisFrame && !isLoadCoroutineStarted)
        {
            isLoadCoroutineStarted = true;
            StartCoroutine(LoadDrawing());
        }
    }

    public static SerializedTrail AddSerializedTrail()
    {
        SerializedTrail freshTrail = new SerializedTrail();
        _save.drawing.Add(freshTrail);
        return freshTrail;
    }
    
    // Save Drawing
    public void SaveDrawing()
    {
        // 2
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/drawing.save");
        bf.Serialize(file, _save);
        file.Close();
        
        Debug.Log("Drawing Saved");
    }
    
    // Load Drawing
    IEnumerator LoadDrawing()
    { 
        
        if (File.Exists(Application.persistentDataPath + "/drawing.save"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/drawing.save", FileMode.Open);
            _save = (SaveDrawing)bf.Deserialize(file);
            file.Close();
            
            StartCoroutine(FetchOldDrawings());
            Debug.Log("Drawing Loaded");
            isLoadCoroutineStarted = false;
            yield return null;
        }
        else
        {
            Debug.Log("No game saved!");
            isLoadCoroutineStarted = false;
            yield return null;
        }
    }

    IEnumerator FetchOldDrawings()
    {
        //Delete all drawPrefabs
        foreach (var drawTrail in GameObject.FindGameObjectsWithTag("Draw"))
        {
            Destroy(drawTrail);
        }
        
        //Add new drawPrefabs - SetPositions/AddPositions, and using the _save nested info
        foreach (var serializedTrail in _save.drawing)
        {
            if (serializedTrail.trail.Count != 0)
            {
                trailCount++;
                theTrail = Instantiate(loadDrawPrefab, serializedTrail.trail[0], Quaternion.identity);
                foreach (var sv in serializedTrail.trail)
                {
                    yield return new WaitForSeconds(0.0001f);
                    theTrail.transform.position = sv;
                }
            }
        }
        Debug.Log(trailCount);
        yield return null;
    }
    
}
