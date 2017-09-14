using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class playerMove : NetworkBehaviour {
    public GameObject bulletPrefab;
    public Transform bulletSpawn;
    public Transform cameraPath;
    public Vector3 setit;
    public JoystickController joystick;
	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame

	void FixedUpdate ()
    {
        if (!isLocalPlayer)
        {
            return;
        }

        //float x = Input.GetAxis("Horizontal") * Time.deltaTime * 150.0f;
        // float z = Input.GetAxis("Vertical") * Time.deltaTime * 3.0f;
        float x = joystick.Horizontal()* Time.deltaTime * 150.0f;
        float z = joystick.Vertical()* Time.deltaTime * 3.0f;
        Debug.Log(x);
        Debug.Log(z);

        transform.Rotate(0,x,0);
        transform.Translate(0, 0, z);
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Cmdfire();
        }

    

}
    private void LateUpdate()
    {
        if (!isLocalPlayer)
        {
            return;
        }
        Camera.main.transform.position = this.transform.position-this.transform.forward*(0f)+ this.transform.up*(.3f);
        //Camera.main.transform.LookAt(this.transform.position);
        Camera.main.transform.parent = this.transform;
    }
    public override void OnStartLocalPlayer()
    {
        base.OnStartLocalPlayer();
        GetComponent<MeshRenderer>().material.color = Color.red;
    }
    [Command]
    void Cmdfire()
    {
        Vector3 correction = new Vector3(0,0,0);
        GameObject bullet = (GameObject)Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);
        NetworkServer.Spawn(bullet);//spawn the bullet in server
        Destroy(bullet, 0.1f);
    }
}
