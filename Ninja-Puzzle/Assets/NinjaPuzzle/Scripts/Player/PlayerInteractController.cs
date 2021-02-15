using System.Collections;
using UnityEngine;

namespace NinjaPuzzle.Scripts.Player
{
	public class PlayerInteractController : MonoBehaviour
	{
		[SerializeField] Transform cameraTransform;
		[SerializeField] GameObject weaponPrefab;
		[SerializeField] Transform weaponSpawnPoint;
		[SerializeField] float weaponImpulse = 40f;
		
		void Update()
		{
			if (Input.GetButtonDown("Fire1"))
			{
				StartCoroutine(Fire());
			}
		}

		IEnumerator Fire()
		{
			var weapon = Instantiate(weaponPrefab, weaponSpawnPoint.position, weaponSpawnPoint.rotation);
			Rigidbody weaponRb = weapon.GetComponent<Rigidbody>();
			weaponRb.velocity = cameraTransform.forward * weaponImpulse;
			Vector3 initialScale = weapon.transform.localScale;
			weapon.transform.localScale = Vector3.zero;
			yield return new WaitForSeconds(0.1f);
			weapon.transform.localScale = initialScale;
		}
	}
}