using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_manager : MonoBehaviour
{
	[SerializeField]
	float upTime = 0.5f;
	[SerializeField]
	float pitckupHeight = 0.5f;
	[SerializeField]
	GameObject floor;
	[SerializeField]
	GameObject pickedUp;

	[SerializeField]
	float forceAmount = 2000;
	float distance_plane;
	Vector3 goalPosition;
	Vector3 origin;
	float startTime;
	[SerializeField]
	Color pickupColor;
    // Start is called before the first frame update
    void Start()
    {
        distance_plane = floor.transform.position.y;
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
			float up_dist = distance_plane + pitckupHeight;
			Vector3 up = new Vector3(0,1,0);
			Vector3 maxUp = new Vector3(entPos.x, up_dist, entPos.z);
			
			if ( entPos.y < maxUp.y ){
				float distCovered = (Time.time - startTime);// * speed;
				float fractionOfJourney = distCovered / upTime;
				float upLerp =  Mathf.Lerp(origin.y, maxUp.y, fractionOfJourney);
				print(upLerp);
				
				up = new Vector3(entPos.x, upLerp, entPos.z);
				pickedUp.transform.position = up;
				//rigidbody.AddForce( up );
			}
			else{
				Vector3 vel = rigidbody.velocity;
				vel.y = 0;
				
				rigidbody.velocity = vel;
			}
			entPos = entPos + rigidbody.velocity;
			
			float dist = Vector3.Distance(goalPosition, entPos);
			
			if (dist > 0.2f )
				rigidbody.AddForce((goalPosition-entPos).normalized*forceAmount*Time.deltaTime);
			
		}
		
		//Find pick up entitiy
        if (Input.GetMouseButtonDown(0))
		{	
			//Only find if we dont have a picked up
			if (pickedUp == null){
				//pressed
				Ray ray = Camera.main.ScreenPointToRay(mousePos);
				RaycastHit hit;
			
				if (Physics.Raycast(ray, out hit))
				{
					//Hit object
					GameObject ent = hit.collider.gameObject;
				
					if (ent != null && ent.tag == "Physics"){
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
			print ("Released");

			if (pickedUp == null)//null check
				return;
			Renderer _renderer = pickedUp.GetComponent<Renderer>();
			_renderer.material.shader = Shader.Find("Custom/ToonURPShader"); //finds the shader
			_renderer.material.SetColor("_OutlineColor", Color.black);

			pickedUp = null;
;
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
