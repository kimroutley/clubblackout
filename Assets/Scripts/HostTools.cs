using UnityEngine;
using System.Collections;

namespace ClubBlackout {
    public class HostTools : MonoBehaviour {
        public float NightTimeoutSeconds = 20f;
        public float DayTimeoutSeconds = 60f;

        private Coroutine runningTimer = null;
        public GameManager GameManager;

        public void StartNightTimer() {
            StopTimer();
            runningTimer = StartCoroutine(RunTimer(NightTimeoutSeconds, () => {
                // Timer finished: auto-advance to day
                GameManager?.HostAdvanceToDay();
            }));
        }

        public void StartDayTimer() {
            StopTimer();
            runningTimer = StartCoroutine(RunTimer(DayTimeoutSeconds, () => {
                // Timer finished: auto-advance to night
                GameManager?.HostAdvanceToNight();
            }));
        }

        public void HostAdvanceToNight() {
            StopTimer();
            GameManager?.HostAdvanceToNight();
        }
        public void HostAdvanceToDay() {
            StopTimer();
            GameManager?.HostAdvanceToDay();
        }

        void StopTimer() {
            if (runningTimer != null) { StopCoroutine(runningTimer); runningTimer = null; }
        }

        IEnumerator RunTimer(float seconds, System.Action onComplete) {
            float t = 0f;
            while (t < seconds) {
                t += Time.deltaTime;
                yield return null;
            }
            onComplete?.Invoke();
        }
    }
}