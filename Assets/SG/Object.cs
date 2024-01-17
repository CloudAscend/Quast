using UnityEngine;

public class Object : MonoBehaviour
{
    Rigidbody rb;
    bool wasMoving = false; // 새로운 변수 추가

    public AudioSource audioSrc;

    private void Awake()
    {
        audioSrc = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        MoveSfx();
    }

    void MoveSfx()
    {
        float fadeOutTime = 1.0f; // 페이드아웃에 걸리는 시간 (초)

        if (Mathf.Abs(rb.velocity.x) >= 0.2f || Mathf.Abs(rb.velocity.z) >= 0.2f)
        {
            if (!audioSrc.isPlaying)
            {
                audioSrc.volume = 1.0f; // 소리를 다시 시작할 때 볼륨을 1로 설정
                audioSrc.Play();
            }

            wasMoving = true; // wasMoving을 true로 설정
        }
        else
        {
            if (wasMoving && audioSrc.isPlaying)
            {
                // 페이드아웃 로직
                float fadeOutSpeed = 1.0f / fadeOutTime;
                audioSrc.volume -= fadeOutSpeed * Time.deltaTime;

                if (audioSrc.volume <= 0.0f)
                {
                    audioSrc.Stop();
                    audioSrc.volume = 1.0f; // 초기 볼륨으로 재설정 (이 부분을 조절하십시오)
                    wasMoving = false; // wasMoving을 false로 설정
                }
            }
        }
    }
}
