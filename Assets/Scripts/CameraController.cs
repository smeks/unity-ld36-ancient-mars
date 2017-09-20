using UnityEngine;
using System.Collections;
using Spine.Unity;

public class CameraController : MonoBehaviour {

    public float ortho = 20.0f;
    public Vector2 camOffset = new Vector2(0, 0);
    public GameObject player;
    private PlayerController playerController;
    

	// Use this for initialization
	void Start () {
        playerController = player.GetComponent<PlayerController>();
    }
	
	// Update is called once per frame
	void Update () {


    }
    
    void FixedUpdate()
    {
        var newPos = new Vector3(player.transform.position.x + camOffset.x, player.transform.position.y + camOffset.y, -1);
        var orthoSize = ortho;
        if (playerController.nearStructure)
        {
            var offset = playerController.cameraOffsetNearStructure;
            var newOrtho = playerController.cameraOrthoNearStructure;
            newPos.x += offset.x;
            newPos.y += offset.y;
            orthoSize = newOrtho;
        }
        Vector3 vel = Vector3.zero;
        transform.position = Vector3.SmoothDamp(transform.position, newPos,
                                                ref vel, 0.2f);

        float orthoVel = 0;
        GetComponent<Camera>().orthographicSize = Mathf.SmoothDamp(GetComponent<Camera>().orthographicSize, orthoSize, ref orthoVel, 0.4f);
    }
}
