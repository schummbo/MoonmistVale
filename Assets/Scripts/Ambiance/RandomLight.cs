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

        public bool IsToggling { get; private set; }
        public bool IsOn => randomLight.enabled;

        void Update()
        {
            if (IsToggling)
            {
                currentTime += Time.deltaTime;

                if (currentTime > futureTime)
                {
                    randomLight.enabled = isLighting;
                    IsToggling = false;
                }
            }
        }

        public void TurnOn(bool randomize)
        {
            SetFutureTime(randomize);

            IsToggling = true;
            isLighting = true;
        }

        public void TurnOff(bool randomize)
        {
            SetFutureTime(randomize);

            IsToggling = true;
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
