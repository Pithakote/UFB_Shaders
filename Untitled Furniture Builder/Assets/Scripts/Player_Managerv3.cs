using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Managerv3 : MonoBehaviour
{
	public float upTime = 0.5f;
	public GameObject snappable;
	public GameObject pickedUp;

	//Drag
	[SerializeField]
	float forceAmount = 5000;
	float distance_plane;
	Vector3 goalPosition;
	Vector3 origin;
	float startTime;
	float upLerp = 0;

	//Rotate
	[SerializeField]
	bool rotating = false;
	int axis = 1;
	float startAng;
	[SerializeField]
	float snapTime = 0.5f;
	[SerializeField]
	float snapAngle;
	
	float scrollTime;

	[SerializeField]
	float upDistance;
	[SerializeField]
	float deltaSnap;
	[SerializeField]
	Color pickupColor;
	[SerializeField]
	float speed;

	[SerializeField]
	float rotSpeed;
	//-----------------
	
	void Start()
	{
		Vector3 pos = new Vector3(3.5f, 3.5f, 35f);
		
		SpawnSnappable(pos, 0, 1);
		SpawnSnappable(pos, 1, 2);
	}
	
	void Update()
	{
		Vector2 mousePos = Input.mousePosition;

		//Move picked up entity
		if (pickedUp != null)
		{
			Camera cam = Camera.main;
			Vector3 entPos = pickedUp.transform.position;
			Vector3 camPos = cam.transform.position;
			Ray ray_ = Camera.main.ScreenPointToRay(mousePos);
			RaycastHit hitMouse;
			Rigidbody rigidbody = pickedUp.GetComponent<Rigidbody>();
			
			if (pickedUp.tag == "Snappable" && rigidbody == null)
				return;
			
			//Move to raycast
			if (rotating == false && Physics.Raycast(ray_, out hitMouse))
			{
				if (hitMouse.collider.gameObject != pickedUp) {
					float hitY = hitMouse.point.y;
					float dist_hit = Mathf.Abs(hitY - distance_plane);
					float min_dist = 0.025f;
					
					if ( hitY != distance_plane ){
						distance_plane = hitY;
						origin = entPos;
						//origin = hitMouse.point;
						startTime = Time.time;
					}
				}
				
				goalPosition = new Vector3(hitMouse.point.x, entPos.y, hitMouse.point.z);
			}
			else
			{
				Vector2 mPos = new Vector2();
				mPos.x = mousePos.x;
				mPos.y = mousePos.y;

				Vector3 worldPos = cam.ScreenToWorldPoint(new Vector3(mPos.x, mPos.y, camPos.y));
				goalPosition = new Vector3(worldPos.x, entPos.y, worldPos.z);
			}

			//Move upwards
			float up_dist = distance_plane + upDistance;
			Vector3 maxUp = new Vector3(entPos.x, up_dist, entPos.z);

			if ( Mathf.Abs(maxUp.y - entPos.y) > 0.1f )
			{
				float distCovered = (Time.time - startTime);// * speed;
				float fractionOfJourney = distCovered / upTime;
				upLerp = Mathf.Lerp(origin.y, maxUp.y, fractionOfJourney);
			}
			else
			{
				Vector3 vel = rigidbody.velocity;
				vel = new Vector3(0, 0, 0);

				rigidbody.velocity = vel;
				rigidbody.angularVelocity = vel;
			}
			Vector3 up = new Vector3(entPos.x, upLerp, entPos.z);
			pickedUp.transform.position = up;

			entPos = entPos + rigidbody.velocity;
			float dist = Vector3.Distance(goalPosition, entPos);

			//Move in mouse dir
			if (dist > 0.2f)
				rigidbody.AddForce((goalPosition - entPos).normalized * forceAmount * Time.deltaTime);
		}

		if (Input.GetKey(KeyCode.R) && pickedUp != null)
		{
			//Vals
			Rigidbody rigidbody = pickedUp.GetComponent<Rigidbody>();
			float mouseDX = Mathf.Clamp(Input.GetAxis("Mouse X"), -1, 1);
			float mouseDY = Mathf.Clamp(Input.GetAxis("Mouse Y"), -1, 1);
			
			FreezeConstraints( rigidbody, true );
			pickedUp.transform.Rotate(new Vector3(mouseDX, mouseDY, 0) * Time.deltaTime * rotSpeed);
			
			if (rotating == false)
				rotating = true;
		}
		else if(Input.GetKeyUp(KeyCode.R) && pickedUp != null)
        {
			Rigidbody rigidbody = pickedUp.GetComponent<Rigidbody>();
			FreezeConstraints( rigidbody, false );
			rotating = false;
		}


		//Find pick up entitiy
		if (Input.GetMouseButtonDown(0))
		{
			//Only find if we dont have a picked up
			if (pickedUp == null)
			{
				//pressed
				Ray ray = Camera.main.ScreenPointToRay(mousePos);
				RaycastHit hit;

				if (Physics.Raycast(ray, out hit))
				{
					//Hit object
					GameObject ent = hit.collider.gameObject;

					if (ent != null && ent.tag == "Physics" || ent != null && ent.tag == "Snappable" || ent != null && ent.tag == "parent")
					{
						snap comp = ent.GetComponent<snap>();
						if (ent.tag == "Snappable" && comp != null){
							GameObject snapped = comp.snapped;
							if ( snapped != null )
								ent = snapped;
						}
							
						upLerp = 0;
						rotating = false;
						pickedUp = ent;
						origin = ent.transform.position;
						startTime = Time.time;
						Rigidbody rigidbody = pickedUp.GetComponent<Rigidbody>();
						rigidbody.constraints = RigidbodyConstraints.None;

						Renderer _renderer = pickedUp.GetComponent<Renderer>();
						_renderer.material.shader = Shader.Find("Custom/ToonURPShader"); //finds the shader
						_renderer.material.SetColor("_OutlineColor", pickupColor);
						
					}

				}
			}
			
		}
		else if (Input.GetMouseButtonUp(0))
		{
			//released
			if (pickedUp != null) {
				FreezeConstraints( pickedUp.GetComponent<Rigidbody>(), false );
				Renderer _renderer = pickedUp.GetComponent<Renderer>();
				_renderer.material.shader = Shader.Find("Custom/ToonURPShader"); //finds the shader
				_renderer.material.SetColor("_OutlineColor", Color.black);
				pickedUp = null;
			}
		}



		if (Input.GetMouseButtonDown(1))
		{
			if (pickedUp != null) {
				//pickedUp.transform.position = goalPosition;
				Rigidbody rigidbody = pickedUp.GetComponent<Rigidbody>();
				FreezeConstraints( rigidbody, true );
			}
		}
	}

	void SpawnSnappable(Vector3 origin, int id, int snap_to)
	{
		GameObject ent = Instantiate(snappable);
		ent.transform.position = origin;
		//
		snap s = ent.GetComponent<snap>();
		s.id = id;
		s.snap_to_id = snap_to;
	}
	
	void FreezeConstraints( Rigidbody rigidbody, bool b )
	{
		if (b == true){
			rigidbody.constraints = RigidbodyConstraints.FreezeAll;
			Cursor.visible = false;
			//rigidbody.constraints = RigidbodyConstraints.FreezeAll;
		} else {
			rigidbody.constraints = RigidbodyConstraints.None;
			Cursor.visible = true;
		}
	}
	
}


