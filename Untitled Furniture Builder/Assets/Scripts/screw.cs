using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class screw : MonoBehaviour
{
	//Values
	public int snap_to_id;
	public int piece_id;	
	public Vector3 offset;
	public GameObject snapped;
	public GameObject iconPreview;
	
	//Lerp
	float move_back_time = -100;
	bool moving_to_screw = false;
	bool moving = false;
	bool corrected = false;
	float anim_time = 1.0f;
	float air_time = 1.0f;
	GameObject icon;
	Vector3 lerp_startpos;
	Vector3 lerp_pos;
	Vector3 lerp_startang;
	Vector3 lerp_ang;
	float lerp_start = -100;
	
	//Mouse Minigame
	float screw_time = .65f;
	GameObject attempt_ent;
	float req_rotations = 5;
	float rotations = 0;
	float cur_perc = 0;
	float lastAngle = 0;

	//Components
	bool attached = false;
	Rigidbody rigid;
	MeshFilter meshFilter;
	
    // Start is called before the first frame update
    void Start()
    {
		loadVars();
		//if (snap_to_id != -1)
		//	InitPreview();
    }
	
	void loadVars()
	{
        meshFilter = GetComponent<MeshFilter>();
		rigid = GetComponent<Rigidbody>();
	}
	
	void SetSnapToPos()
	{
		//if (snapped == null)
		//	return;
		////--
		//Vector3 angles = snapped.transform.eulerAngles;
		//snapped.transform.eulerAngles = new Vector3(0,0,0);
		//transform.position = lerp_pos;
		//transform.eulerAngles = lerp_ang;
		//snapped.transform.eulerAngles = angles;
		if (snapped == null)
			return;
		//--
		corrected = true;
		Vector3 angles = snapped.transform.eulerAngles;
		Quaternion rotation = snapped.transform.rotation;
		Quaternion inverse = Quaternion.Inverse(rotation);

		//transform.position = snapped.transform.position + offset;
		snapped.transform.eulerAngles = new Vector3(0, 0, 0);
		transform.position = snapped.transform.position + offset;
		transform.eulerAngles = lerp_ang;
		snapped.transform.eulerAngles = angles;
	}
	
	void Drop()
	{
		moving = false;
				
		if (rigid != null)
			rigid.constraints = RigidbodyConstraints.None;
				
		if (Player_Managerv2.screwEnt == this.gameObject)
			Player_Managerv2.screwEnt = null;
	}

	void Update()
	{
		//Rigidbody
		if ( rigid != null ){
			rigid.angularVelocity = new Vector3(0,0,0);
		}

		//Lerp
		if (corrected == false)
			UpdateLerp();
		
		if (attached == true || moving_to_screw == false || lerp_start + anim_time + air_time > Time.time)
			return;
		
		//Minigame
		Vector2 center = new Vector2(Screen.width/2, Screen.height/2);
		Vector2 mpos = Input.mousePosition;
        center = mpos - center;
        float angle = Mathf.Atan2(center.y, center.x) * Mathf.Rad2Deg;
		
		if (lastAngle != angle){
			move_back_time = Time.time + screw_time;
			float difference = angle - lastAngle;
			float perc_change = difference / 360;
			
			if (perc_change > 0)
				cur_perc += perc_change;
			
			//print(perc_change);
			
			if (cur_perc >= 1){
				cur_perc = 0;
				rotations += 1;
				
				if (rotations >= req_rotations){
					moving_to_screw = false;
					Destroy( icon );
					ScrewAttach( attempt_ent );
				}
			}
		}
		
		lastAngle = angle;
		//print("ANGLE: " +angle);
	}
	
	void UpdateLerp()
	{
		if (lerp_start == null || lerp_start + anim_time <= Time.time){
			//Lerp Finished
			if (attached == true && corrected == false){				
				SetSnapToPos();
			}
			
			//Drops from air
			if (attached == false && moving == true && lerp_start + anim_time + air_time <= Time.time ){
				if ( moving_to_screw == true ){
					//If not icon spawned, spawn it
					if (icon == null){
						icon = Instantiate( iconPreview );
						icon.transform.position = transform.position;
						Vector3 localAng = transform.eulerAngles;
						icon.transform.eulerAngles = transform.eulerAngles + new Vector3(90,0,90);
						
						//icon.transform.SetParent( transform );
						//icon.transform.position = new Vector3(0,0,0);
					}
					
					//Move back screw to drop after elapsed time
					if ( move_back_time <= Time.time ){
						if (icon != null)
							Destroy( icon );
						
						moving_to_screw = false;
						attempt_ent = null;
						SetGameObjectMoveTo(  lerp_pos, lerp_ang, lerp_startpos, lerp_startang );
					}
				}
				else if (attached == false)
					Drop();
			}
			//--
			return;
		}
		//--
		float distCovered = (Time.time - lerp_start);
		float fractionOfJourney = distCovered / anim_time;
		transform.position = Vector3.Lerp(lerp_startpos, lerp_pos, fractionOfJourney);
		transform.eulerAngles = Vector3.Lerp(lerp_startang, lerp_ang, fractionOfJourney);
	}
	
	void SetAttached( GameObject parent )
    {
		if ( snapped != null )
			return;
		//--
		if (Player_Managerv2.pickedUp == this.gameObject)
			Player_Managerv2.pickedUp = null;
		//--
		Vector3 oldPos = transform.position;
		Vector3 oldAng = transform.eulerAngles;

		snapped = parent;
		attached = true;
		transform.SetParent(parent.transform);
		//transform.position =  parent.transform.position;
		Player_Managerv2.screwEnt = null;

		//Physics.IgnoreCollision(col, parent.GetComponent<MeshCollider>());
		
		Destroy( rigid );
		
		CapsuleCollider[] colliders = GetComponentsInChildren<CapsuleCollider>();
		foreach ( CapsuleCollider col in colliders )
		{
			Destroy( col );
		}

		//Snap to position (connect)
		Vector3 angles = snapped.transform.eulerAngles;
		Quaternion rotation = snapped.transform.rotation;
		Quaternion inverse = Quaternion.Inverse(rotation);
		//transform.position =  new Vector3(0,0,0);

		//snapped.transform.eulerAngles = new Vector3(0,0,0);
		SetGameObjectMoveTo(oldPos, oldAng, parent.transform.position + parent.transform.TransformDirection(offset), angles);
		//snapped.transform.eulerAngles = angles;
	}

	void SetGameObjectMoveTo(  Vector3 startPos, Vector3 startAng, Vector3 pos, Vector3 toAng )
	{
		moving = true;
		rigid.constraints = RigidbodyConstraints.FreezePosition;
		//--
		lerp_startpos = startPos;
		lerp_pos = pos;
		lerp_ang = toAng;
		lerp_startang = startAng;
		lerp_start = Time.time;
	}
	
	void ScrewAttach( GameObject ent )
	{
		SetAttached( ent );
		
		snap s = ent.GetComponent<snap>();
		s.AddPieceIDScrewed( piece_id );
		snap.numSnapped++;
	}
	
	//---------------------
	//EVENTS
	public void onMouseClick()
	{
		if (attached == true || moving == true || snapped != null)
			return;
		//--
		Vector3 startPos = transform.position;
		Vector3 startAng = transform.eulerAngles;
		Vector3 target = startPos + new Vector3(0,1,0);
		Vector3 toAng = new Vector3(0,180,0);
		
		SetGameObjectMoveTo(  startPos, startAng, target, toAng );
	}
	
	public void onMouseAttach( GameObject ent )
	{
		if (attached == true || moving_to_screw == true)
			return;

		attempt_ent = ent;
		moving_to_screw = true;
		move_back_time = Time.time + air_time + anim_time + screw_time;
		//--
		Quaternion rotation = ent.transform.rotation;
		Quaternion inverse = Quaternion.Inverse(rotation);

		Vector3 startPos = transform.position;
		Vector3 startAng = transform.eulerAngles;
		Vector3 up = (transform.up * .25f);

		Vector3 pos = ent.transform.position + ent.transform.TransformDirection(offset);
		Vector3 toAng = new Vector3(0, -180, 0);

		SetGameObjectMoveTo(startPos, startAng, pos, toAng);
	}
	//---------
	
	public void SetModel( Mesh loaded )
	{
		if (meshFilter == null)
			loadVars();
		
		meshFilter.mesh = loaded;
		meshFilter.sharedMesh = loaded;
	}
    
}


