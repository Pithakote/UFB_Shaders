using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class snap : MonoBehaviour
{
	//Values
	public int snap_to_id;
	public int id;
	public GameObject snapped;
	public Material public_mat;
	public bool drawPreview = true;
	Vector3 pos;
	
	//Components
	Rigidbody rigid;
	MeshFilter meshFilter;
	//MeshCollider col;
	
    // Start is called before the first frame update
    void Start()
    {
        meshFilter = GetComponent<MeshFilter>();
		//col = GetComponent<MeshCollider>();
		rigid = GetComponent<Rigidbody>();
    }

	void Update()
	{
		if (drawPreview == false)
			return;
		//--
		Mesh mesh = meshFilter.mesh;
		Vector3 offset = new Vector3(0.5f,0,-0.5f);
		Vector3 pos = transform.position + offset;
		Material material = public_mat;
		
		Graphics.DrawMesh(mesh, pos, transform.rotation, material, 0);
	}
	
	void OnTriggerEnter(Collider other)
    {
		if (rigid == null) return;
		//--
        GameObject hit = other.gameObject;
		string tag = hit.tag;
		
		if (tag != "Snappable" || snapped != null)
			return;
		
		if ( snap_to_id == hit.GetComponent<snap>().id ){
			transform.position =  hit.transform.position;
			transform.SetParent( hit.transform );
			
			//Physics.IgnoreCollision(col, other);
			snapped = hit;
			drawPreview = false;
			snapped.GetComponent<snap>().drawPreview = false;
			Destroy( rigid );
			
			//Snap to position (connect)
			Vector3 offset = new Vector3(0.5f,0,-0.5f);
			transform.position = snapped.transform.position + offset;
			transform.eulerAngles = snapped.transform.eulerAngles;
		}
    }

	
	public void SetModel( string dir )
	{
		meshFilter.sharedMesh = Resources.Load<Mesh>(dir);
	}
    
}


