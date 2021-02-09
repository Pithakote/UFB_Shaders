using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
	//Game Manager
	public Vector3 startpos;
	public Material materialShader;
	public GameObject snappable;
	public Mesh[] snap_meshes;
	public int[] snap_tos;
	public Vector3[] snap_offsets;
	public GameObject[] snappables;
	
    // Start is called before the first frame update
    void Start()
    {
		Vector3 offset = new Vector3(0.5f,0,-0.5f);
		
		int amt_snappables = snap_tos.Length;
		snappables = new GameObject[amt_snappables];
		
		for (int i = 0;  i <= amt_snappables - 1; i++)
		{
			float min = Random.Range(-1,1);
			if (min == 0)
				min = 0.5f;
			
			snappables[i] = SpawnSnappable(snap_meshes[i], startpos + (offset * i), snap_offsets[i], i, snap_tos[i]);
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
		//s.SetModel( model );
		//--
		return ent;
	}
	
}


