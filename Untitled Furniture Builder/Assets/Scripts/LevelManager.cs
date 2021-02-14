using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
	//Game Manager
	public float triggerMinDistance = 0;
	public int minAngleSnap = 45;
	public Vector3 startpos;
	public Material materialShader;
	public GameObject snappable;
	public GameObject tray;
	public GameObject screw;
	public Mesh[] snap_meshes;
	public Vector3[] piece_scales;
	public int[] snap_tos;
	public Vector3[] snap_offsets;
	public int[] screw_snapto;
	public int[] screw_pieceid;
	public Vector3[] screw_offsets;
	public GameObject[] snappables;
	public GameObject[] screws;
	
    // Start is called before the first frame update
    void Start()
    {
		Vector3 offset = new Vector3(0.5f,0,-0.5f);
		
		int amt_snappables = snap_tos.Length;
		snappables = new GameObject[amt_snappables];
		
		//Spawn pieces
		for (int i = 0;  i <= amt_snappables - 1; i++)
		{
			float min = Random.Range(-1,1);
			if (min == 0)
				min = 0.5f;
			
			snappables[i] = SpawnSnappable(snap_meshes[i], startpos + (offset * i), snap_offsets[i], i, snap_tos[i]);
		}
		
		//Spawn Screws
		Vector3 tray_pos = tray.transform.position + new Vector3(0,3.5f,0);
		int max_screws = screw_snapto.Length;
		screws = new GameObject[max_screws];
		
		for (int i = 0; i <= max_screws - 1; i++)
		{
			GameObject newScrew = Instantiate(screw);
			newScrew.transform.position = tray_pos;
			//
			int id = screw_snapto[i];
			newScrew.GetComponent<screw>().piece_id = screw_pieceid[i];
			newScrew.GetComponent<screw>().snap_to_id = id;
			newScrew.GetComponent<screw>().offset = screw_offsets[i];
			//--
			GameObject piece = snappables[id];
			snap comp = piece.GetComponent<snap>();
			
			comp.max_screws = max_screws;
			comp.AddPieceIDScrewable( screw_pieceid[i], screw_offsets[i] );
			
			//piece.GetComponent<snap>().screwable = new bool[screw_snap_id.Length];
			//piece.GetComponent<snap>().screwable[i] = true;
		}
		
		Player_Managerv2.snappables = snappables;
    } 

    // Update is called once per frame
    void Update()
    {
        
    }
	
	GameObject SpawnSnappable(Mesh model, Vector3 origin, Vector3 offset, int id, int snap_to)
	{
		GameObject ent = Instantiate(snappable);
		ent.transform.position = origin;
		//--
		//Scale
		Vector3 default_scale = new Vector3(1,1,1);
		Vector3 empty_scale = new Vector3(0,0,0);
		Vector3 custom_scale = piece_scales[id];
		
		if ( custom_scale != null && custom_scale != empty_scale )
			ent.transform.localScale = custom_scale;
		else
			ent.transform.localScale = default_scale;
		//
		snap s = ent.GetComponent<snap>();
		s.id = id;
		s.snap_to_id = snap_to;
		s.offset = offset;
		ent.GetComponent<MeshFilter>().sharedMesh = model;
		
		MeshCollider[] colliders = ent.GetComponentsInChildren<MeshCollider>();
		foreach ( MeshCollider col in colliders )
		{
			col.sharedMesh = null;
			col.sharedMesh = model;
		}
		
		ent.GetComponent<MeshCollider>().sharedMesh = model;
		ent.GetComponent<MeshRenderer>().material = materialShader;
		
		if (triggerMinDistance > 0)
			s.triggerMinDistance = triggerMinDistance;
		
		s.min_angle = minAngleSnap;
		//--
		return ent;
	}
	
}


