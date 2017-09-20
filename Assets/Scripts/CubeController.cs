using UnityEngine;
using System.Collections;

public class CubeController : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        transform.Rotate(10*Time.deltaTime, 20 * Time.deltaTime, 30 * Time.deltaTime);
	}
}
