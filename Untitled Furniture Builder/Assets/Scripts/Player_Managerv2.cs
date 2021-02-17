using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class Player_Managerv2 : MonoBehaviour
{

	[DllImport("user32.dll")]
	public static extern bool SetCursorPos(int X, int Y);

	public float upTime = 0.5f;
	public static GameObject pickedUp;
	public static GameObject screwEnt;
	GameObject levelManager;
	public static GameObject[] snappables;

	//Drag
	[SerializeField]
	float forceAmount = 15000;
	float distance_plane;
	Vector3 goalPosition;
	Vector3 origin;
	float startTime;
	float upLerp = 0;
	float scroll = 0;
	private Vector3 surface_normal = new Vector3(0, 1, 0);
	public LayerMask mask;
	bool invertMask;

	//Rotate
	Vector2 storedMousePos;
	bool rotating = false;
	int axis = 1;
	float startAng;
	float scrollTime;

	[SerializeField]
	float upDistance;
	[SerializeField]
	Color pickupColor, releaseColor;

	[SerializeField]
	float rotSpeed;
	//-----------------

	void Start()
	{
		releaseColor = Color.black;
		//SpawnSnappable(model, pos + offset_pos, offset, 0, 1);
		//SpawnSnappable(model, pos, offset, 1, 2);
		//SpawnSnappable(model, pos - offset_pos, offset2, 3, 4);
		levelManager = GameObject.Find("LevelManager");
		snappables = levelManager.GetComponent<LevelManager>().snappables;
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
				string tag = "";
				if (hitObj != null)
				{
					//print(hitObj.tag);
					tag = hitObj.tag;
				}

				float ang = Vector3.Angle(hitMouse.normal, transform.up);
				//print( ang );
				if (ang == 0)
				{
					float hitY = hitMouse.point.y;
					float dist_hit = Mathf.Abs(hitY - distance_plane);
					float min_dist = 0.1f;

					if (hitY != distance_plane && dist_hit > min_dist)
					{
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
			scroll = Mathf.Clamp(scroll + mscroll, -(upDistance * .5f), upDistance * 2);
			float up_dist = distance_plane + upDistance + scroll;
			Vector3 maxUp = new Vector3(entPos.x, up_dist, entPos.z);

			if (Mathf.Abs(maxUp.y - entPos.y) > 0.2f)
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
			if (dist > 0.12f)
			{
				//print(dist);
				float perc = 0.12f / dist;
				rigidbody.AddForce((goalPosition - entPos).normalized * (forceAmount - (forceAmount * perc)) * Time.deltaTime);
			}
		}

		//ROTATE
		//Store mouse data
		if (Input.GetKeyDown(KeyCode.R))
			storedMousePos = Input.mousePosition;

		if (Input.GetKey(KeyCode.R) && pickedUp != null)
		{
			Cursor.lockState = CursorLockMode.Locked;
			Cursor.visible = false;
			//SetCursorPos( (int)storedMousePos.x, Screen.height - (int)storedMousePos.y);
			//SetCursorPos( Screen.width/2, Screen.height/2);
			//Vals
			Rigidbody rigidbody = pickedUp.GetComponent<Rigidbody>();
			//float mouseDX = Mathf.Clamp(Input.GetAxis("Mouse X"), -1, 1);
			//float mouseDY = Mathf.Clamp(Input.GetAxis("Mouse Y"), -1, 1);
			float mouseDX = Input.GetAxis("Mouse X");
			float mouseDY = Input.GetAxis("Mouse Y");


			rigidbody.velocity = new Vector3(0, 0, 0);
			rigidbody.angularVelocity = new Vector3(0, 0, 0);

			FreezeConstraints(rigidbody, true);
			//pickedUp.transform.Rotate(new Vector3(mouseDX, mouseDY, 0) * Time.deltaTime * rotSpeed);
			pickedUp.transform.RotateAround(pickedUp.transform.position, new Vector3(mouseDX, 0, mouseDY), Time.deltaTime * rotSpeed);
			if (rotating == false)
				rotating = true;

		}
		else if (Input.GetKeyUp(KeyCode.R) && pickedUp != null)
		{
			Rigidbody rigidbody = pickedUp.GetComponent<Rigidbody>();
			FreezeConstraints(rigidbody, false);
			rotating = false;
		}
		//-----------

		//Find pick up entitiy
		if (Input.GetMouseButtonDown(0))
		{

			//Only find if we dont have a picked up
			if (pickedUp == null)
			{
				//pressed
				mousePos = Input.mousePosition;
				Ray ray = Camera.main.ScreenPointToRay(mousePos);
				RaycastHit hit;

				if (Physics.Raycast(ray, out hit))
				{
					//Hit object
					GameObject ent = hit.collider.gameObject;
					Rigidbody rigidbody = ent.GetComponent<Rigidbody>();
					Cursor.visible = true;
					Cursor.lockState = CursorLockMode.None;
					//rigidbody.constraints = RigidbodyConstraints.None;

					if (ent.tag == "screw")
					{
						ent.GetComponent<screw>().onMouseClick();
						screwEnt = ent;
					}

					if (ent != null && rigidbody != null && ent.tag == "Physics" || ent != null && ent.tag == "Snappable" || ent != null && ent.tag == "Preview" && ent.transform.root.gameObject.GetComponent<Rigidbody>() != null)
					{
						if (screwEnt == null)
						{
							if (ent.tag == "Preview")
							{
								ent = ent.transform.root.gameObject;
								rigidbody = ent.GetComponent<Rigidbody>();
							}

							snap comp = ent.GetComponent<snap>();
							if (comp != null)
							{
								GameObject snapped = comp.snapped;
								if (snapped != null)
									ent = snapped;

								//Enable preview
								int to_id = comp.snap_to_id;
								int parent_to_id = ent.GetComponent<snap>().snap_to_id;

								if (parent_to_id > -1 && to_id > -1 && to_id < snappables.Length)
								{
									GameObject connect_to = snappables[to_id];
									snap p_comp = connect_to.GetComponent<snap>();
									Mesh mesh = ent.GetComponent<MeshFilter>().mesh;
									Vector3 newOffset = comp.offset;

									p_comp.InitPreview(mesh, newOffset);
								}
							}

							distance_plane = hit.point.y;
							IgnoreRaycast(ent, true);
							scroll = (upDistance / 2);
							upLerp = 0;
							rotating = false;
							pickedUp = ent;
							origin = ent.transform.position;
							startTime = Time.time;
							if (rigidbody == null)
								return;
							else
							{
								rigidbody.constraints = RigidbodyConstraints.None;
							}


							Renderer _renderer = pickedUp.GetComponent<Renderer>();
							//_renderer.material.shader = Shader.Find("Custom/ToonURPShader"); //finds the shader
							if (_renderer.material.HasProperty("_OutlineColor") == true)
								_renderer.material.SetColor("_OutlineColor", pickupColor);

						}
						else if (ent.tag == "Snappable" || ent.transform.root.tag == "Snappable")
						{
							snap comp = ent.GetComponent<snap>();
							if (comp != null)
							{
								screw comp_screw = screwEnt.GetComponent<screw>();
								int snap_to = comp_screw.snap_to_id;

								if (snap_to == comp.id)
								{
									int closest_id = comp.findClosestScrewable(hit.point);
									bool screwable = comp.isPieceScrewable(closest_id);
									bool screwed = comp.isPieceScrewed(closest_id);

									//print(closest_id);
									if (screwable == true && screwed == false)
										comp_screw.onMouseAttach(ent);
								}
							}
						}
					}
				}
			}

		}
		else if (Input.GetMouseButtonUp(0))
		{
			//released
			if (pickedUp != null)
			{
				//Remove preview
				snap comp = pickedUp.GetComponent<snap>();

				if (comp != null)
				{
					int id = comp.snap_to_id;
					if (id > -1 && id < snappables.Length)
					{
						GameObject snapTo = snappables[id];
						snap p_comp = snapTo.GetComponent<snap>();
						GameObject preview = comp.preview_ent;

						if (preview != null)
							p_comp.DestroyPreview();
					}
				}
				IgnoreRaycast(pickedUp, false);
				FreezeConstraints(pickedUp.GetComponent<Rigidbody>(), false);
				Renderer _renderer = pickedUp.GetComponent<Renderer>();
				//_renderer.material.shader = Shader.Find("Custom/ToonURPShader"); //finds the shader
				if (_renderer.material.HasProperty("_OutlineColor") == true)
					_renderer.material.SetColor("_OutlineColor", releaseColor);
				pickedUp = null;
			}
		}

		//if (Input.GetMouseButtonDown(1))
		//{
		//	if (pickedUp != null)
		//	{
		//		Debug.Log("RMB clicked");
		//		//pickedUp.transform.position = goalPosition;
		//		Rigidbody rigidbody = pickedUp.GetComponent<Rigidbody>();
		//		FreezeConstraints(rigidbody, true);
		//	}
		//}




	}

	public void IgnoreRaycast(GameObject ent, bool b)
	{
		if (b == true)
			ent.layer = layermask_to_layer(mask);
		else
			ent.layer = 0;
	}

	//---
	public int layermask_to_layer(LayerMask layerMask)
	{
		int layerNumber = 0;
		int layer = layerMask.value;
		while (layer > 0)
		{
			layer = layer >> 1;
			layerNumber++;
		}
		return layerNumber - 1;
	}
	//---

	void FreezeConstraints(Rigidbody rigidbody, bool b)
	{
		if (b == true)
		{
			rigidbody.constraints = RigidbodyConstraints.FreezeAll;
			Cursor.visible = false;
			//rigidbody.constraints = RigidbodyConstraints.FreezeAll;
		}
		else
		{
			rigidbody.constraints = RigidbodyConstraints.None;
			Cursor.visible = true;
		}
	}

}


