using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public bool hasPowerup = false;
    public float speed = 5f;
    public float powerupStrength = 10f;
    private Rigidbody rb;
    private GameObject focalPoint;
    public GameObject powerupIndicator;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("Focal Point");
    }

    // Update is called once per frame
    void Update()
    {
        powerupIndicator.transform.position = transform.position;
        float verticalInput = Input.GetAxis("Vertical");
        rb.AddForce(focalPoint.transform.forward * speed * verticalInput * Time.deltaTime );
    }
    IEnumerator PowerupCountdown()
    {
        yield return new WaitForSeconds(5f);
        hasPowerup = false;
        powerupIndicator.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag=="Powerup")
        {
            hasPowerup = true;
            powerupIndicator.SetActive(true);
            Destroy(other.gameObject);
            StartCoroutine(PowerupCountdown());
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag=="Enemy" && hasPowerup)
        {
            Rigidbody enemyRb = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromPlayer = collision.gameObject.transform.position - transform.position;
            enemyRb.AddForce(awayFromPlayer * powerupStrength, ForceMode.Impulse);
            Debug.Log("Collided with : " + collision.gameObject.name + " with powerup set to " + hasPowerup);
        }
    }
}
