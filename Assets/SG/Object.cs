using UnityEngine;

public class Object : MonoBehaviour
{
    Rigidbody rb;
    bool wasMoving = false; // ���ο� ���� �߰�

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
        float fadeOutTime = 1.0f; // ���̵�ƿ��� �ɸ��� �ð� (��)

        if (Mathf.Abs(rb.velocity.x) >= 0.2f || Mathf.Abs(rb.velocity.z) >= 0.2f)
        {
            if (!audioSrc.isPlaying)
            {
                audioSrc.volume = 1.0f; // �Ҹ��� �ٽ� ������ �� ������ 1�� ����
                audioSrc.Play();
            }

            wasMoving = true; // wasMoving�� true�� ����
        }
        else
        {
            if (wasMoving && audioSrc.isPlaying)
            {
                // ���̵�ƿ� ����
                float fadeOutSpeed = 1.0f / fadeOutTime;
                audioSrc.volume -= fadeOutSpeed * Time.deltaTime;

                if (audioSrc.volume <= 0.0f)
                {
                    audioSrc.Stop();
                    audioSrc.volume = 1.0f; // �ʱ� �������� �缳�� (�� �κ��� �����Ͻʽÿ�)
                    wasMoving = false; // wasMoving�� false�� ����
                }
            }
        }
    }
}
