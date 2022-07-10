using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformsRotation : MonoBehaviour
{
    private Vector3 mousePosition;//позиция мыши на экране
    private Vector3 point1;//начальная и конечная позиции мыши
    private Vector3 point2;
    public GameObject player;
    private Quaternion startRotation;
    private Vector3 startPosition;

    private bool swiping = false;//вращаются платформы или нет(зажата ли лкм или нет?)
    private float forwardRotation;//величина вращения платформ за кадр
    private float rightRotation;

    private float maxRotation = 0.25f;
    private float rotationSpeed = 600f;//скорость вращения платформ
    private float rotationAmplitude = 35f;//максимальнное отклонение вращения в градусах
    private float startRotationX;//начальное вращение платформы относительно оси X

    private void Start()
    {
        //начальное вращение платформы относительно оси X, от него будет браться амплитуда
        startRotationX = transform.rotation.eulerAngles.x;
    }
    private void Update()
    {
        mousePosition = Camera.main.ScreenToViewportPoint(Input.mousePosition);
        if (Input.GetMouseButtonDown(0))//когда пользователь начинает зажимать мышь, начинаем вращать платформы
        {
            swiping = true;
            point1 = mousePosition;
        }
        else if (Input.GetMouseButtonUp(0))//когда пользователь отпускает мышь, перестаём вращать платформы
        {
            swiping = false;
        }
        //вращение: считываем позицию мыши (конечную) для текущего кадра, вращаем и считываем позицию (начальную) для следующего кадра
        if (swiping == true)
        {
            point2 = mousePosition;
            RotatePlatforms(point1, point2);
            point1 = mousePosition;
        }
    }
    private void RotatePlatforms(Vector3 point1, Vector3 point2)
    {
        
        //считываем, сколько за кадр пользователь провёл пальцем/мышью
        forwardRotation = -(point2.x - point1.x);
        rightRotation = point2.y - point1.y;
        CheckForMaxRotation(ref forwardRotation, ref rightRotation);


        //вращаем платформы относительно игрока
        startRotation = transform.rotation;
        startPosition = transform.position;
        transform.RotateAround(player.transform.position, Vector3.forward, forwardRotation * rotationSpeed * Time.deltaTime);
        transform.RotateAround(player.transform.position, Vector3.right, rightRotation * rotationSpeed * Time.deltaTime);
        OverRotation();
    }
    //проверка на превышение допустимого вращения платформы, если превышено - возвращаем значение вращения на максимально допустимое
    private void OverRotation()
    {
        if (transform.rotation.eulerAngles.x > startRotationX + rotationAmplitude)
        {
            transform.rotation = startRotation;
            transform.position = startPosition;
            //transform.rotation = Quaternion.Euler(startRotationX + rotationAmplitude, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
        }
        if (transform.rotation.eulerAngles.x < startRotationX - rotationAmplitude)
        {
            transform.rotation = startRotation;
            transform.position = startPosition;
            //transform.rotation = Quaternion.Euler(startRotationX - rotationAmplitude, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
        }
        return;
    }
    private void CheckForMaxRotation(ref float forwardRotation, ref float rightRotation)
    {
        int positiveOrNegative;
        if (Mathf.Abs(forwardRotation) > maxRotation)
        {
            positiveOrNegative = forwardRotation < 0 ? -1 : 1;
            forwardRotation = maxRotation * positiveOrNegative;
        }
        if(rightRotation > maxRotation)
        {
            positiveOrNegative = rightRotation < 0 ? -1 : 1;
            rightRotation = maxRotation * positiveOrNegative;
        }
    }
}
