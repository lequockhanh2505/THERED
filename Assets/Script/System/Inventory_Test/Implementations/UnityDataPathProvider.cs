using System.IO;
using UnityEngine;

public class UnityDataPathProvider : IDataPathProvider
{
    public string GetPath(string fileName)
    {
        return Path.Combine(Application.streamingAssetsPath, fileName);
    }
}
