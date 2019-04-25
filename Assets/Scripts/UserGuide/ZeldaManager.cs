using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZeldaManager : MonoBehaviour
{

    public GameObject panel;
    public GameObject lookat_target;
    public GameObject distance_target;
    public Light point_light;
    public CameraCollision cameraCollision;
    public AudioSource audioClip;

    private float start_range = 3f;
    private float target_range = 13f;
    private float distance = 0f;
    private bool started = false;

    private float distance_start = 0f;
    private float distance_end = -2f;



    // Update is called once per frame
    void FixedUpdate()
    {
        if (started) {
            Vector2 v21 = new Vector2(BarbPlayerController.instance.transform.position.x, BarbPlayerController.instance.transform.position.z);
            Vector2 v22 = new Vector2(distance_target.transform.position.x, distance_target.transform.position.z);
            float current_distance = Vector2.Distance(v21, v22);
            float time_elpsed = (distance - current_distance) / distance;
            point_light.range = 10f * time_elpsed + start_range;

            float current_camera = distance_start - (distance_start - distance_end) * time_elpsed;
            cameraCollision.maxDistance = current_camera;

            RenderSettings.skybox.SetFloat("_Exposure", (20 - 1) * time_elpsed + 1);

            if (current_distance < 0.1) {
                PlayerInput.Instance.setStop();
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.CompareTag("zelda_area")){
            
            collision.collider.gameObject.SetActive(false);
            PlayerInput.Instance.enable = false;
            BarbPlayerController.instance.transform.LookAt(lookat_target.transform, Vector3.up);
            PlayerInput.Instance.setMaxStraight();
            panel.SetActive(false);
            Vector2 v21 = new Vector2(BarbPlayerController.instance.transform.position.x, BarbPlayerController.instance.transform.position.z);
            Vector2 v22 = new Vector2(distance_target.transform.position.x, distance_target.transform.position.z);
            distance = Vector2.Distance(v21, v22);
            started = true;
            distance_start = cameraCollision.maxDistance;
            audioClip.Play();
        }
    }

    private void OnDestroy()
    {
        RenderSettings.skybox.SetFloat("_Exposure", 1);
    }
}
