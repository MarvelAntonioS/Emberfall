using UnityEngine;
using System.Collections;

public class DestroyTreeTop : MonoBehaviour
{
    [SerializeField] private string treeTopName = "TreeTop";
    [SerializeField] private GameObject logs;
    [SerializeField] private Transform vfx;
    [SerializeField] private float destroyDelay = 5f; // Increased delay to allow for falling
    [SerializeField] private float logsDestroyDelay = 10f;

    private void Start()
    {
        StartCoroutine(DestroyTreeTopCoroutine());
    }

    private IEnumerator DestroyTreeTopCoroutine()
    {
        yield return new WaitForSeconds(destroyDelay);

        Transform treeTop = transform.Find(treeTopName);

        if (treeTop != null)
        {
            GameObject logsInstance = Instantiate(logs, treeTop.position, treeTop.rotation);
            Destroy(treeTop.gameObject);
            SpawnVFXAtLogsChildren(logsInstance);
            StartCoroutine(DestroyLogsAfterDelay(logsInstance));
        }
    }

    private void SpawnVFXAtLogsChildren(GameObject logsInstance)
    {
        foreach (Transform child in logsInstance.transform)
        {
            Instantiate(vfx, child.position, child.rotation);
        }
    }

    private IEnumerator DestroyLogsAfterDelay(GameObject logsInstance)
    {
        yield return new WaitForSeconds(logsDestroyDelay);

        if (logsInstance != null)
        {
            Destroy(logsInstance);
        }
    }
}