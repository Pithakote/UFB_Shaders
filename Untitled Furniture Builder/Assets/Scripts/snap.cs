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
	
	public GameObject snapped;
	public GameObject preview_ent;
	
	//Components
	Rigidbody rigid;
	MeshFilter meshFilter;
	//MeshCollider col;
	
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
	
	void OnTriggerStay(Collider other)
    {
		if (rigid == null) return;
		//--
        GameObject hit = other.gameObject;
		float dist = Vector3.Distance(transform.position, hit.transform.position);
		
		//print(dist);
		//if ( dist > 2.5 )
		//	return;
		
		string tag = hit.tag;
		if (tag != "Preview" || snapped != null)
			return;
		
		
		//Angle Check
		Vector3 myAngles = transform.eulerAngles;
		Vector3 otherAngles = hit.transform.eulerAngles;
		float difference = Mathf.DeltaAngle(otherAngles.z, myAngles.z);
		difference = Mathf.Abs( difference );
		
		float min_angle = 45;
		print(difference);
		if ( difference > min_angle )
			return;
		
		GameObject parent = other.transform.parent.gameObject;
		if ( snap_to_id == parent.GetComponent<snap>().id ){
			Player_Managerv2.pickedUp = null;
			//--
			transform.position =  parent.transform.position;
			transform.SetParent( parent.transform );
			
			//Physics.IgnoreCollision(col, other);
			snapped = parent;
			//drawPreview = false;
			//snapped.GetComponent<snap>().drawPreview = false;
			Destroy( rigid );
			
			//Snap to position (connect)
			Vector3 angles = snapped.transform.eulerAngles;
			//Vector3 offset = new Vector3(0.5f,0,-0.5f);
			
			snapped.transform.eulerAngles = new Vector3(0,0,0);
			transform.position = snapped.transform.position + offset;
			snapped.transform.eulerAngles = angles;
			transform.eulerAngles = angles;
			
			//previews
			Destroy(preview_ent);
			Destroy(parent.GetComponent<snap>().preview_ent);
		}
    }

	public void InitPreview(Mesh mesh, Vector3 newOffset)
	{
		if (preview_ent != null)
			Destroy( preview_ent );
		
		GameObject spawned = Instantiate(preview);
		spawned.layer = 8;
		spawned.transform.localScale = transform.localScale;
		spawned.transform.SetParent( transform );
			
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


