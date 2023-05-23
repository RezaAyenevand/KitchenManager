using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Gameplay.UI
{
    public class SliderController : MonoBehaviour
    {
        [SerializeField] Image sliderImage;

        public void FillSlider(float amount)
        {
            sliderImage.fillAmount += amount;
        }
    }

}
