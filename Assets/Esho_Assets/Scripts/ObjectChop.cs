using System.Collections;
using UnityEngine;

public class ObjectChopped : MonoBehaviour, Hitable
{
    [SerializeField] private GameObject ChoppedObject;
    [SerializeField] private Transform vfx;
    [SerializeField] private float delay = 0.45f;
    [SerializeField] public float respawnTime = 10f;
    [SerializeField] private float initialForce = 5f; // Increased initial force
    [SerializeField] private Vector3 fallDirection = new Vector3(1f, -0.5f, 0f); // Adjust as needed

    private bool isChopped = false;
    private Collider objectCollider;

    private void Start()
    {
        objectCollider = GetComponent<Collider>();
    }

    public void Execute()
    {
        if (!isChopped)
        {
            StartCoroutine(ChoppingSequence());
        }
    }

    private IEnumerator ChoppingSequence()
    {
        isChopped = true;
        yield return new WaitForSeconds(delay);

        GameObject choppedInstance = Instantiate(ChoppedObject, transform.position, transform.rotation);
        Instantiate(vfx, transform.position, transform.rotation);

        ApplyFallPhysics(choppedInstance);

        objectCollider.enabled = false;
        gameObject.SetActive(false);
        ObjectRespawner.Instance.RespawnObject(gameObject, respawnTime, choppedInstance);
    }

    private void ApplyFallPhysics(GameObject choppedInstance)
    {
        Rigidbody treeTopRb = choppedInstance.GetComponentInChildren<Rigidbody>();
        if (treeTopRb != null)
        {
            // Adjust rigidbody properties
            treeTopRb.mass = 100f; // Increase mass
            treeTopRb.drag = 0.1f; // Lower drag
            treeTopRb.angularDrag = 0.05f; // Lower angular drag

            // Apply initial force
            treeTopRb.AddForce(fallDirection.normalized * initialForce, ForceMode.Impulse);

            // Add some torque for rotation
            treeTopRb.AddTorque(Random.insideUnitSphere * initialForce, ForceMode.Impulse);
        }
    }

    public void ResetObject()
    {
        isChopped = false;
        if (objectCollider != null)
        {
            objectCollider.enabled = true;
        }
    }
}