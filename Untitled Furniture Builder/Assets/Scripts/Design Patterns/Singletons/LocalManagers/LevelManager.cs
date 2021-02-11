using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : LocalManager
{
	//Game Manager
	public float triggerMinDistance = 0;
	public int minAngleSnap = 45;
	public Vector3 startpos;
	public Material materialShader;
	public GameObject snappable;
	public Mesh[] snap_meshes;
	public int[] snap_tos;
	public Vector3[] snap_offsets;
	public GameObject[] snappables;
<<<<<<< HEAD:Untitled Furniture Builder/Assets/Scripts/LevelManager.cs
	
    // Start is called before the first frame update
    void Start()
    {
		Vector3 offset = new Vector3(0.5f,0,-0.5f);
		
		int amt_snappables = snap_tos.Length;
		snappables = new GameObject[amt_snappables];
		
		for (int i = 0;  i <= amt_snappables - 1; i++)
=======

	[SerializeField]
	GameObject _pauseMenuObject;

	protected override void SetInitialState()
	{
		//base class abstraction
		_instance.StateManager.PauseMenuObject = _pauseMenuObject;
		_instance.StateManager.InGameState = new InGameState(_instance, _pauseMenuObject);
		_instance.StateManager.CurrentState = _instance.StateManager.InGameState;

		Debug.Log(_instance.StateManager.CurrentState + " is being called");

	}

	void InitializeSnappable()
	{
		Vector3 offset = new Vector3(0.5f, 0, -0.5f);

		int amt_snappables = snap_tos.Length;
		snappables = new GameObject[amt_snappables];

		for (int i = 0; i <= amt_snappables - 1; i++)
>>>>>>> parent of a179e38 (Revert "Merge branch 'UFB-56-Design-Patterns'"):Untitled Furniture Builder/Assets/Scripts/Design Patterns/Singletons/LocalManagers/LevelManager.cs
		{
			float min = Random.Range(-1, 1);
			if (min == 0)
				min = 0.5f;

			snappables[i] = SpawnSnappable(snap_meshes[i], startpos + (offset * i), snap_offsets[i], i, snap_tos[i]);
		}
<<<<<<< HEAD:Untitled Furniture Builder/Assets/Scripts/LevelManager.cs
		
		Player_Managerv2.snappables = snappables;
    } 
=======
>>>>>>> parent of a179e38 (Revert "Merge branch 'UFB-56-Design-Patterns'"):Untitled Furniture Builder/Assets/Scripts/Design Patterns/Singletons/LocalManagers/LevelManager.cs

		Player_Managerv2.snappables = snappables;
	}
  
    protected override void Start()
    {
		//base class abstraction
		base.Start();

		InitializeSnappable();

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


