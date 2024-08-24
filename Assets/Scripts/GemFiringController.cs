using UnityEngine;
using UnityEngine.Events;

public class GemFiringController : MonoBehaviour
{
    public UnityEvent EvtGemFired;
    [SerializeField] private float fireForce = 10f;
    private GameObject currentGem; 

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && currentGem != null)
        {
            currentGem.transform.parent = null; 
            currentGem.GetComponent<Rigidbody2D>().AddForce(transform.up * fireForce, ForceMode2D.Impulse);
            EvtGemFired?.Invoke();
        }
    }

    public void SetCurrentGem(GameObject gem)
    {
        currentGem = gem;
    }
}
