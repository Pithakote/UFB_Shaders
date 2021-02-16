using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class snap : MonoBehaviour
{
	//Values
	public int snap_to_id;
	public int id;
	public Vector3 offset;
	public GameObject preview;
	
	//Screwing
	public int max_screws;
	public int[] screwable;
	public int[] screwed;
	public Vector3[] screw_offsets;
	
	public int min_angle = 45;
	public float triggerMinDistance = 0;
	public GameObject snapped;
	public GameObject preview_ent;
	
	//Drop
	float stay_time = 3.0f;
	
	//Lerp
	float anim_time = 1.0f;
	GameObject lerped_ent;
	Vector3 lerp_startpos;
	Vector3 lerp_pos;
	Vector3 lerp_startang;
	Vector3 lerp_ang;
	float lerp_start = -100;
	
	//Components
	Rigidbody rigid;
	MeshFilter meshFilter;
	MeshCollider col;

	public static int numSnapped;

	// Start is called before the first frame update
	void Start()
    {
		loadVars();
		//if (snap_to_id != -1)
		//	InitPreview();
		numSnapped = 0;
	}
	
	void loadVars()
	{
        meshFilter = GetComponent<MeshFilter>();
		rigid = GetComponent<Rigidbody>();
		col = GetComponent<MeshCollider>();
	}
	
	//SCREWING
	public void AddPieceIDScrewable( int id, Vector3 offset )
	{
		if (screwable == null || screwable.Length < 1){
			screwable = new int[max_screws];
			screw_offsets = new Vector3[max_screws];
			
			for (int i = 0; i < max_screws; i++)
				screwable[i] = -1;
		}
		
		for (int i = 0; i < screwable.Length; i++){
			if (screwable[i] == -1){
				screwable[i] = id;
				screw_offsets[i] = offset;
				break;
			}
		}
	}
	
	public void AddPieceIDScrewed( int id )
	{
		if (screwed == null || screwed.Length < 1){
			screwed = new int[max_screws];
			
			for (int i = 0; i < max_screws; i++)
				screwed[i] = -1;
		}
		
		for (int i = 0; i < screwed.Length; i++){
			if (screwed[i] == -1){
				screwed[i] = id;
				break;
			}
		}
	}	
	
	public int findClosestScrewable( Vector3 pos )
	{
		if (screw_offsets == null || screw_offsets.Length < 1)
			return -1;
		//--
		float closest = 10000;
		int selected = 0;
		Vector3 empty = new Vector3(0,0,0);
		for (int i = 0; i < screw_offsets.Length; i++)
		{
			Vector3 off = screw_offsets[i];
			if (off == empty)
				continue;
			//--
			Vector3 worldPos = transform.root.TransformPoint( off );
			float dist = Vector3.Distance( pos, worldPos );
			
			//print(off);
			//print(dist);
			
			if (dist < closest){
				closest = dist;
				selected = screwable[i];
			}
		}
		//--
		return selected;
	}
	
	public bool isPieceScrewable( int id )
	{
		for (int i = 0; i < screwable.Length; i++){
			if (screwable[i] == null)
				continue;
			//--
			if (screwable[i] == id)
				return true;
		}
		return false;
	}
	
	public bool isPieceScrewed( int id )
	{
		for (int i = 0; i < screwed.Length; i++){
			if (screwed[i] == null)
				continue;
			//--
			if (screwed[i] == id)
				return true;
		}
		return false;
	}	
	//---------------
	
	void SetSnapToPos()
	{
		if (snapped == null)
			return;
		//--
		Vector3 angles = snapped.transform.eulerAngles;
		snapped.transform.eulerAngles = new Vector3(0,0,0);
		transform.position = lerp_pos;
		transform.eulerAngles = lerp_ang;
		snapped.transform.eulerAngles = angles;
	}
	
	void DropPiece()
	{
		transform.SetParent( null );
		snapped = null;
		Rigidbody rg = gameObject.AddComponent<Rigidbody>();
		rigid = rg;
	}
	
	void Update()
	{
		UpdateLerp();
		
		if (preview_ent == null)
			return;
		
		if (Player_Managerv2.pickedUp == transform.gameObject )
		{
			if (preview_ent.activeSelf == true)
				preview_ent.SetActive(false);
		}else {
			if (preview_ent.activeSelf == false)
				preview_ent.SetActive(true);
		}
	}
	
	void UpdateLerp()
	{
		if (lerp_start == null || lerp_start + anim_time <= Time.time){
			if (col.enabled == false){
				col.enabled = true;
				
				snapped.transform.root.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
			}
			
			if (snapped != null){
				snap parent_comp = snapped.GetComponent<snap>();
				if (parent_comp.isPieceScrewable(id) && parent_comp.isPieceScrewed(id) != true)
					DropPiece();
			}
			
			//if ( screwable == true && lerp_start + anim_time + stay_time <= Time.time){
			//for (int i = 0; i < screwable.Length; i++){
				//Drop
			// }
			//--
			return;
		}
		//--
		float distCovered = (Time.time - lerp_start);
		float fractionOfJourney = distCovered / anim_time;
		//transform.position = Vector3.Lerp(lerp_startpos, lerp_pos, fractionOfJourney);
		transform.eulerAngles = Vector3.Lerp(lerp_startang, lerp_ang, fractionOfJourney);
	}
	
	void OnTriggerStay(Collider other)
    {
		if (rigid == null) return;
		//--
        GameObject hit = other.gameObject;
		
		//print(dist);
		//if ( dist > 2.5 )
		//	return;
		if (triggerMinDistance > 0){
			float dist = Vector3.Distance(transform.position, hit.transform.position);
			if (dist > triggerMinDistance)
				return;
		}
		
		string tag = hit.tag;
		if (tag != "Preview" || snapped != null)
			return;
		
		//Angle Check
		Vector3 myAngles = transform.eulerAngles;
		Vector3 otherAngles = hit.transform.eulerAngles;
		float difference = Mathf.DeltaAngle(otherAngles.z, myAngles.z);
		difference = Mathf.Abs( difference );
		
		//print(transform.name);
		//print(difference);
		if ( difference > min_angle )
			return;
		
		GameObject parent = other.transform.parent.gameObject;
		if ( snap_to_id == parent.GetComponent<snap>().id ){
			Player_Managerv2.pickedUp = null;
			//--
			Vector3 oldPos = transform.position;
			Vector3 oldAng = transform.eulerAngles;
			
			transform.position =  parent.transform.position;
			transform.SetParent( parent.transform );
			
			//Physics.IgnoreCollision(col, other);
			snapped = parent;
			parent.transform.root.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition;
			//drawPreview = false;
			//snapped.GetComponent<snap>().drawPreview = false;
			Destroy( rigid );
			
			//Snap to position (connect)
			Vector3 angles = snapped.transform.eulerAngles;
			numSnapped++;
			//Vector3 offset = new Vector3(0.5f,0,-0.5f);

			snapped.transform.eulerAngles = new Vector3(0,0,0);
			SetGameObjectMoveTo( oldPos, oldAng, snapped.transform.position + offset, angles );
			transform.position = snapped.transform.position + offset;
			snapped.transform.eulerAngles = angles;
			//transform.eulerAngles = angles;
			
			//previews
			Destroy(preview_ent);
			Destroy(parent.GetComponent<snap>().preview_ent);
		}
    }

	void SetGameObjectMoveTo(  Vector3 startPos, Vector3 startAng, Vector3 pos, Vector3 toAng )
	{
		col.enabled = false;
		lerp_startpos = startPos;
		lerp_pos = pos;
		lerp_ang = toAng;
		lerp_startang = startAng;
		lerp_start = Time.time;
	}
	
	public void InitPreview(Mesh mesh, Vector3 newOffset)
	{
		if (preview_ent != null)
			Destroy( preview_ent );
		
		GameObject spawned = Instantiate(preview);
		spawned.layer = 8;
		
		spawned.transform.SetParent( transform );
		spawned.transform.localScale = transform.localScale;
		MeshFilter filter = spawned.GetComponent<MeshFilter>();
		filter.sharedMesh = mesh;
		filter.mesh = mesh;
			
		//Snap to position (connect)
		//Vector3 offset = new Vector3(0.5f,0,-0.5f);
		
		Vector3 ang = transform.eulerAngles;
		transform.eulerAngles = new Vector3(0,0,0);
		
		spawned.transform.position = transform.position + newOffset;
		spawned.transform.eulerAngles = transform.eulerAngles;
		
		transform.eulerAngles = ang;	
		preview_ent = spawned;
	}
	
	public void SetModel( string dir )
	{
		if (meshFilter == null)
			loadVars();
		
		Mesh loaded = (Mesh)Resources.Load(dir,typeof(Mesh));
		meshFilter.mesh = loaded;
		meshFilter.sharedMesh = loaded;
	}
    
}


