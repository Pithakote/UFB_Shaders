using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Managerv2 : MonoBehaviour
{
	public float upTime = 0.5f;
	public GameObject floor;
	public GameObject pickedUp;

	//Drag
	[SerializeField]
	float forceAmount = 2000;
	float distance_plane;
	Vector3 goalPosition;
	Vector3 origin;
	float startTime;

	//Rotate
	[SerializeField]
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


	// Start is called before the first frame update
	void Start()
	{
		//distance_plane = floor.transform.position.y;
	}

	// Update is called once per frame
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

			//Move to raycast
			if (Physics.Raycast(ray_, out hitMouse))
			{
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
			float up_dist = origin.y + upDistance;
			Vector3 up = new Vector3(0, 1, 0);
			Vector3 maxUp = new Vector3(entPos.x, up_dist, entPos.z);

			if (entPos.y < maxUp.y)
			{
				float distCovered = (Time.time - startTime);// * speed;
				float fractionOfJourney = distCovered / upTime;
				float upLerp = Mathf.Lerp(origin.y, maxUp.y, fractionOfJourney);

				up = new Vector3(entPos.x, upLerp, entPos.z);
				pickedUp.transform.position = up;
			}
			else
			{
				Vector3 vel = rigidbody.velocity;
				vel = new Vector3(0, 0, 0);

				rigidbody.velocity = vel;
				rigidbody.angularVelocity = vel;
			}
			entPos = entPos + rigidbody.velocity;

			float dist = Vector3.Distance(goalPosition, entPos);

			//Move in mouse dir
			if (dist > 0.2f)
				rigidbody.AddForce((goalPosition - entPos).normalized * forceAmount * Time.deltaTime);

			//Rotate
			Vector3 angles = pickedUp.transform.eulerAngles;
			Vector2 scrollDelta = Input.mouseScrollDelta;

			if (Input.GetKey(KeyCode.R) && scrollDelta.y != 0)
			{
				startAng = angles[axis];
				scrollTime = Time.time;
				deltaSnap = scrollDelta.y;

				
			}

			if (Time.time < scrollTime + snapTime)
			{
				snapAngle = 90.0f;
				float minAngle = startAng;
				float maxAngle = startAng + (snapAngle * deltaSnap);
				

				float frac = (Time.time - scrollTime) / snapTime;

				//if(axis == 1)
				//	pickedUp.transform.RotateAround(pickedUp.transform.position, Vector3.left, speed * deltaSnap * Time.deltaTime );
				//else
				//	pickedUp.transform.RotateAround(pickedUp.transform.position, Vector3.right, speed * deltaSnap * Time.deltaTime );


				float angle = Mathf.LerpAngle(minAngle, maxAngle, frac);



				//float angle;
				//if (minAngle > maxAngle)
				//	angle = Mathf.LerpAngle(maxAngle, minAngle, frac);
				//else
				//	angle = Mathf.LerpAngle(minAngle, maxAngle, frac);

				print(minAngle);
				print(maxAngle);
				

				if (axis == 0)
					pickedUp.transform.rotation = Quaternion.Euler(angles[0] + 30, angles[1], angles[2]); // = new Vector3(angle, angles[1], angles[2]);
				else if (axis == 1)
					pickedUp.transform.rotation = Quaternion.Euler(angles[0], angle, angles[2]);
			}
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

					if (ent != null && ent.tag == "Physics")
					{
						pickedUp = ent;
						origin = ent.transform.position;
						startTime = Time.time;

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
			//print ("Released");

			if (pickedUp == null)//null check
				return;
			Renderer _renderer = pickedUp.GetComponent<Renderer>();
			_renderer.material.shader = Shader.Find("Custom/ToonURPShader"); //finds the shader
			_renderer.material.SetColor("_OutlineColor", Color.black);
			pickedUp = null;

		}



		if (Input.GetMouseButtonDown(1))
		{
			if (pickedUp == null)//null check
				return;

			print("RMB clicked");
			//pickedUp.transform.position = goalPosition;
			Rigidbody rigidbody = pickedUp.GetComponent<Rigidbody>();
			rigidbody.constraints = RigidbodyConstraints.FreezeAll;
		}
	}

}
