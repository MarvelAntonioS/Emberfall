using System.Collections;
using UnityEngine;

public class ObjectRespawner : MonoBehaviour
{
    public static ObjectRespawner Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void RespawnObject(GameObject obj, float respawnTime, GameObject choppedInstance)
    {
        StartCoroutine(RespawnObjectCoroutine(obj, respawnTime, choppedInstance));
    }

    private IEnumerator RespawnObjectCoroutine(GameObject obj, float respawnTime, GameObject choppedInstance)
    {
        yield return new WaitForSeconds(respawnTime);

        Destroy(choppedInstance);

        obj.SetActive(true);
        obj.GetComponent<ObjectChopped>()?.ResetObject(); // Reset the object's state
    }
}