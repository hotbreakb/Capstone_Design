using UnityEngine;
using System.Collections;

// ----- Low Poly FPS Pack Free Version -----
public class BulletScript : MonoBehaviour {

	[Range(5, 100)]
	[Tooltip("After how long time should the bullet prefab be destroyed?")]
	public float destroyAfter;
	[Tooltip("If enabled the bullet destroys on impact")]
	public bool destroyOnImpact = false;
	[Tooltip("Minimum time after impact that the bullet is destroyed")]
	public float minDestroyTime;
	[Tooltip("Maximum time after impact that the bullet is destroyed")]
	public float maxDestroyTime;

	[Header("Impact Effect Prefabs")]
	public Transform [] metalImpactPrefabs;

	[Header("Shoot Force")]
	[Tooltip("Minimum shoot force")]
	public float minimumShootForce = 2500.0f;
	[Tooltip("Maximum shoot force")]
	public float maximumShootForce = 3000.0f;


	[Header("Throw Force")]
	[Tooltip("Minimum throw force")]
	public float minimumThrowForce = 1500.0f;
	[Tooltip("Maximum throw force")]
	public float maximumThrowForce = 2500.0f;

	private float shootForce;
	private float throwForce;

	private void Awake()
	{
		shootForce = Random.Range(minimumShootForce, maximumShootForce);
		throwForce = Random.Range(minimumThrowForce, maximumThrowForce);
	}
	private void Start () 
	{
		//Start destroy timer
		StartCoroutine (DestroyAfter ());
	}

	private void OnTriggerEnter(Collider other){    // 수혁 일단 지금 필요없음
		//Debug.Log("other.tag : " + other.tag);
		if(other.tag == "Chair"){
			//Toggle "explode" on explosive barrel object
			//collider.transform.gameObject.GetComponent<ExplosiveBarrelScript>().explode = true;
			//Destroy bullet object
			other.attachedRigidbody.AddForce(other.transform.forward * shootForce);
			Debug.Log("Chair collider");
			//GameObject chair = other.gameObject;
			//chair.GetComponent<Rigidbody>().AddForce(other.transform.forward * throwForce);
			//collider.GetComponent<Rigidbody>().AddForce(collider.transform.forward * throwForce);
			//Destroy(gameObject);
		}
		if(other.tag == "Tables") other.attachedRigidbody.AddForce(other.transform.forward * shootForce);

		if (other.tag == "Enemy"){
			Destroy(gameObject);
		}

	}

	//If the bullet collides with anything
	private void OnCollisionEnter (Collision collision) 
	{	
	
		//If destroy on impact is false, start 
		//coroutine with random destroy timer
		if (!destroyOnImpact) 
		{ 
			StartCoroutine (DestroyTimer ());
		}
		//Otherwise, destroy bullet on impact
		else 
		{
			Destroy (gameObject);
		}

		//If bullet collides with "Metal" tag
		if (collision.transform.tag == "Metal") 
		{
			//Instantiate random impact prefab from array
			Instantiate (metalImpactPrefabs [Random.Range 
				(0, metalImpactPrefabs.Length)], transform.position, 
				Quaternion.LookRotation (collision.contacts [0].normal));
			//Destroy bullet object
			Destroy(gameObject);
		}

		//If bullet collides with "Target" tag
		if (collision.transform.tag == "Target") 
		{
			//Toggle "isHit" on target object
			collision.transform.gameObject.GetComponent
				<TargetScript>().isHit = true;
			//Destroy bullet object
			Destroy(gameObject);
		}
			
		//If bullet collides with "ExplosiveBarrel" tag
		if (collision.transform.tag == "ExplosiveBarrel") 
		{
			//Toggle "explode" on explosive barrel object
			collision.transform.gameObject.GetComponent
				<ExplosiveBarrelScript>().explode = true;
			//Destroy bullet object
			Destroy(gameObject);
		}
	}

	private IEnumerator DestroyTimer () 
	{
		//Wait random time based on min and max values
		yield return new WaitForSeconds
			(Random.Range(minDestroyTime, maxDestroyTime));
		//Destroy bullet object
		Destroy(gameObject);
	}

	private IEnumerator DestroyAfter () 
	{
		//Wait for set amount of time
		yield return new WaitForSeconds (destroyAfter);
		//Destroy bullet object
		Destroy (gameObject);
	}
}
// ----- Low Poly FPS Pack Free Version -----