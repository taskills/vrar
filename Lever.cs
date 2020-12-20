using UnityEngine;

public class Lever : MonoBehaviour
{
    public Transform lift, lever, player;
    public static float force;
    float startRot, playerPosInLift;
    int direction;
    bool isInLift;

    void Update()
    {
        if (lever.localRotation.z < -0.75f)                     // если рычаг опущен вниз
            direction = -1;
        else if (lever.localRotation.z > -0.65f)                // если рычаг поднят вверх
            direction = 1;
        else
            direction = 0;                                      // нейтральный режим

        if (direction != 0 && !isInLift)
        {
            playerPosInLift = player.position.y - lift.position.y;
            isInLift = true;
        }

        force = Mathf.Pow(Mathf.Abs(Mathf.Abs(startRot) + lever.localRotation.z), 3);
        lift.position += new Vector3(0, force * Time.deltaTime * 800f * direction, 0);
        lift.position = new Vector3(lift.position.x, Mathf.Clamp(lift.position.y, -700f, 3f), lift.position.z);

        if (isInLift)
            player.position = new Vector3(player.position.x, lift.position.y + playerPosInLift, player.position.z);
    }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        startRot = lever.localRotation.z;
    }
}