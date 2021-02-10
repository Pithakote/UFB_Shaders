using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class screw : MonoBehaviour
{
	//Values
	public int snap_to_id;
	public Vector3 offset;
	public GameObject snapped;
	
	//Lerp
	bool moving = false;
	float anim_time = 1.0f;
	float air_time = 1.0f;
	GameObject lerped_ent;
	Vector3 lerp_startpos;
	Vector3 lerp_pos;
	Vector3 lerp_startang;
	Vector3 lerp_ang;
	float lerp_start = -100;
	
	//Components
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

	void Update()
	{
		UpdateLerp();
	}
	
	void UpdateLerp()
	{
		if (lerp_start == null || lerp_start + anim_time <= Time.time){
			if (moving == true && lerp_start + anim_time + air_time <= Time.time ){
				moving = false;
				
				if (rigid != null)
					rigid.constraints = RigidbodyConstraints.None;
				
				if (Player_Managerv2.screwEnt == this.gameObject){
					Player_Managerv2.screwEnt = null;
				}
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
			
		transform.position =  parent.transform.position;
		transform.SetParent( parent.transform );
			
		//Physics.IgnoreCollision(col, parent.GetComponent<MeshCollider>());
		snapped = parent;
		Destroy( rigid );
		
		CapsuleCollider[] colliders = GetComponentsInChildren<CapsuleCollider>();
		foreach ( CapsuleCollider col in colliders )
		{
			Destroy( col );
		}
			
		//Snap to position (connect)
		Vector3 angles = snapped.transform.eulerAngles;


		snap.numSnapped++;


		snapped.transform.eulerAngles = new Vector3(0,0,0);
		SetGameObjectMoveTo( oldPos, oldAng, snapped.transform.position + offset, angles );
		transform.position = snapped.transform.position + offset;
		snapped.transform.eulerAngles = angles;
    }

	void SetGameObjectMoveTo(  Vector3 startPos, Vector3 startAng, Vector3 pos, Vector3 toAng )
	{
		moving = true;
		rigid.constraints = RigidbodyConstraints.FreezePosition;
		//--
		//col.enabled = false;
		lerp_startpos = startPos;
		lerp_pos = pos;
		lerp_ang = toAng;
		lerp_startang = startAng;
		lerp_start = Time.time;
	}
	
	public void onMouseClick()
	{
		if (moving == true || snapped != null)
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
		 SetAttached( ent );
	}
	
	public void SetModel( Mesh loaded )
	{
		if (meshFilter == null)
			loadVars();
		
		meshFilter.mesh = loaded;
		meshFilter.sharedMesh = loaded;
	}
    
}


