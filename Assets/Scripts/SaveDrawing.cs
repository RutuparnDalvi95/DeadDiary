using System.Collections.Generic;

[System.Serializable]
public class SaveDrawing
{
    public List<SerializedTrail> drawing = new List<SerializedTrail>();
}

[System.Serializable]
public class SerializedTrail
{
    public List<SerializedVector3> trail = new List<SerializedVector3>();
}
