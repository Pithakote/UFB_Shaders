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
	public int[] snap_tos;
	public Vector3[] snap_offsets;
	public int[] screw_snap_id;
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
		screws = new GameObject[screw_snap_id.Length];
		
		for (int i = 0; i <= screw_snap_id.Length - 1; i++)
		{
			GameObject newScrew = Instantiate(screw);
			newScrew.transform.position = tray_pos;
			newScrew.GetComponent<screw>().snap_to_id = i;
			//--
			int id = screw_snap_id[i];
			GameObject piece = snappables[id];
			piece.GetComponent<snap>().screwable = true;
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
		//ent.transform.localScale = new Vector3(0.25f,0.25f,0.25f);
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


