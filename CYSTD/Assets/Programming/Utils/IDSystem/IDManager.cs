using UnityEngine;

/// <summary>
/// Provides static methods for generating Identifiers for GameObjects, and retrieving GameObjects by their ID.
/// </summary>
public static class IDManager
{
    static long lastId = 0;
    /// <summary>
    /// Assigns the next avaliable ID to the GameObject.
    /// </summary>
    public static void GenerateID(this Identifiable target) { if (target.id != -1) target.id = lastId++; }
    /// <summary>
    /// Finds and returns an instance of a GameObject by its ID in the current scene.
    /// </summary>
    public static GameObject Find(long id)
    {
        if (id < 1 || id > lastId) return null;
        foreach (Identifiable o in Object.FindObjectsOfType<Identifiable>())
            if (o.id == id) return o.gameObject;
        return null;
    }
}