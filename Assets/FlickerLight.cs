using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlickerLight : MonoBehaviour
{
 public float minFlickerIntensity;
 public float maxFlickerIntensity;
 public float flickerSpeed;
 private Light light;
 
 private int randomizer = 1;
 private bool increase = true;
  
public void Start(){
	light = GetComponent<Light>();
}

  public void Update()
  {
     // if(increase == true) {
     //   light.intensity = light.intensity + 1;
     //   }else {
     //   	light.intensity = light.intensity - 1;
     //   }

     //   if(light.intensity == maxFlickerIntensity) {
     //   	increase = false;
     //   } else if(light.intensity == minFlickerIntensity){
     //   	increase = true;
     //   }

  	if (randomizer == 2) {
       light.intensity = (Random.Range(minFlickerIntensity, maxFlickerIntensity));
       randomizer = Random.Range(0, 8);
 
     }
     else {
 
     randomizer = Random.Range(0, 8);
   }
  }
}
