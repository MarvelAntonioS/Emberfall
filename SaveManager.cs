using Firebase;
using Firebase.Database;
using Firebase.Extensions;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    private DatabaseReference databaseReference;

    void Start()
    {
        // Initialize Firebase
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsCompleted)
            {
                if (task.Result == DependencyStatus.Available)
                {
                    // Firebase is ready
                    databaseReference = FirebaseDatabase.DefaultInstance.RootReference;
                    Debug.Log("Firebase initialized successfully.");
                }
                else
                {
                    Debug.LogError($"Could not resolve all Firebase dependencies: {task.Result}");
                }
            }
            else
            {
                Debug.LogError("Firebase initialization failed: " + task.Exception);
            }
        });
    }

    public DatabaseReference GetDatabaseReference()
    {
        if (databaseReference == null)
        {
            Debug.LogError("Firebase Database Reference is null. Make sure Firebase is initialized.");
        }
        return databaseReference;
    }
}


