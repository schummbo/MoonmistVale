using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace Assets.Scripts.Ambiance
{
    public class RandomLight : MonoBehaviour
    {
        [SerializeField] private Light2D randomLight;

        [SerializeField] private float randomMin = 0f;
        [SerializeField] private float randomMax = 20f;

        private float currentTime;
        private float futureTime;
        private bool isLighting;
        private bool isToggling;

        public bool IsOn => randomLight.enabled;

        void Update()
        {
            if (isToggling)
            {
                currentTime += Time.deltaTime;

                if (currentTime > futureTime)
                {
                    randomLight.enabled = isLighting;
                    isToggling = false;
                }
            }
        }

        public void TurnOn(bool randomize)
        {
            SetFutureTime(randomize);

            isToggling = true;
            isLighting = true;
        }

        public void TurnOff(bool randomize)
        {
            SetFutureTime(randomize);

            isToggling = true;
            isLighting = false;
        }

        private void SetFutureTime(bool randomize)
        {
            if (!randomize)
            {
                futureTime = 0;
            }
            else
            {
                futureTime = Random.Range(randomMin, randomMax);
            }
        }
    }
}
