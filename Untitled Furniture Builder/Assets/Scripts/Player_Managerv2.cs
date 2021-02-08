using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Managerv2 : MonoBehaviour
{
	public float upTime = 0.5f;
	public GameObject snappable;
	public static GameObject pickedUp;

	//Drag
	[SerializeField]
	float forceAmount = 15000;
	float distance_plane;
	Vector3 goalPosition;
	Vector3 origin;
	float startTime;
	float upLerp = 0;
	float scroll = 0;
	private Vector3 surface_normal = new Vector3(0,1,0);
	public LayerMask mask;
	bool invertMask;

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
		Vector3 offset_pos = new Vector3(0.0f, 0, 3.0f);
		Vector3 offset = new Vector3(1,0,0.5f);
		Vector3 offset2 = new Vector3(2,0,1.5f);
		string model = "Assets/3DModels/Bar Chair/Objects/WoodenChair.fbx";
		
		//SpawnSnappable(model, pos + offset_pos, offset, 0, 1);
		//SpawnSnappable(model, pos, offset, 1, 2);
		//SpawnSnappable(model, pos - offset_pos, offset2, 3, 4);
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
			
			if (pickedUp.tag == "Snappable" && rigidbody == null || pickedUp.tag == "Preview")
				return;
			
			//Move to raycast
			//print("Move");
			LayerMask newMask = ~(invertMask ? ~mask.value : mask.value);
			if (rotating == false && Physics.Raycast(ray_, out hitMouse, Mathf.Infinity, newMask))
			{
				GameObject hitObj = hitMouse.collider.gameObject;
				if (hitObj != null){
					print(hitObj.tag);
					//print(hitObj.layer);
					//print( layermask_to_layer(mask.value) );
				}
				
				float ang = Vector3.Angle(hitMouse.normal, transform.up);
				print( ang );
				if ( ang == 0 ){
					float hitY = hitMouse.point.y;
					float dist_hit = Mathf.Abs(hitY - distance_plane);
					float min_dist = 0.025f;
					
					if ( hitY != distance_plane && dist_hit > min_dist ){
						distance_plane = hitY;
						origin = entPos;
						//origin = hitMouse.point;
						startTime = Time.time;
					}
				}
				
				goalPosition = new Vector3(hitMouse.point.x, entPos.y, hitMouse.point.z);
			}
			/*
			else
			{
				Vector2 mPos = new Vector2();
				mPos.x = mousePos.x;
				mPos.y = mousePos.y;

				Vector3 worldPos = cam.ScreenToWorldPoint(new Vector3(mPos.x, mPos.y, camPos.y));
				goalPosition = new Vector3(worldPos.x, entPos.y, worldPos.z);
			}*/

			//Move upwards
			float mscroll = Input.GetAxis("Mouse ScrollWheel");
			scroll = Mathf.Clamp(scroll + mscroll, -(upDistance*.5f), upDistance*2);
			float up_dist = distance_plane + upDistance + scroll;
			Vector3 maxUp = new Vector3(entPos.x, up_dist, entPos.z);

			if ( Mathf.Abs(maxUp.y - entPos.y) > 0.2f )
			{
				float distCovered = (Time.time - startTime);// * speed;
				float fractionOfJourney = distCovered / upTime;
				upLerp = Mathf.Lerp(origin.y, maxUp.y, fractionOfJourney);
			}

			Vector3 vel = rigidbody.velocity;
			vel = new Vector3(0, 0, 0);
			rigidbody.velocity = vel;
			rigidbody.angularVelocity = vel;

			Vector3 up = new Vector3(entPos.x, upLerp, entPos.z);
			pickedUp.transform.position = up;

			entPos = entPos + rigidbody.velocity;
			float dist = Vector3.Distance(goalPosition, entPos);

			//Move in mouse dir
			if (dist > 0.12f){
				//print(dist);
				float perc = 0.12f / dist;
				//print(perc);
				rigidbody.AddForce((goalPosition - entPos).normalized * (forceAmount - (forceAmount * perc)) * Time.deltaTime);
			}
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
					Rigidbody rigidbody = ent.GetComponent<Rigidbody>();

					if (ent != null && rigidbody != null && ent.tag == "Physics" || ent != null && rigidbody != null && ent.tag == "Snappable")
					{
						snap comp = ent.GetComponent<snap>();
						if (ent.tag == "Snappable" && comp != null){
							GameObject snapped = comp.snapped;
							if ( snapped != null )
								ent = snapped;
						}
						
						distance_plane = hit.point.y;
						IgnoreRaycast( ent, true );
						scroll = (upDistance/2);
						upLerp = 0;
						rotating = false;
						pickedUp = ent;
						origin = ent.transform.position;
						startTime = Time.time;
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
				IgnoreRaycast( pickedUp, false );
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

	void SpawnSnappable(string model, Vector3 origin, Vector3 offset, int id, int snap_to)
	{
		GameObject ent = Instantiate(snappable);
		ent.transform.position = origin;
		//
		//snap s = ent.GetComponent<snap>();
		//s.id = id;
		//s.snap_to_id = snap_to;
		//s.offset = offset;
		//s.model = model;
	}
	
	void IgnoreRaycast( GameObject ent, bool b )
	{
		if (b == true)
			ent.layer = layermask_to_layer( mask );
		else
			ent.layer = 0;
	}
	
	//---
	public static int layermask_to_layer(LayerMask layerMask) {
         int layerNumber = 0;
         int layer = layerMask.value;
         while(layer > 0) {
             layer = layer >> 1;
             layerNumber++;
         }
         return layerNumber - 1;
    }
	//---
	
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


