using UnityEngine;
public class RovController : MonoBehaviour
{
    [Header("Тяга двигателей")]
    public float thrustForward = 5f; // сила вперед/назад
    public float thrustSide = 5f; // сила влево/вправо
    public float thrustVertical = 4f; // сила вверх/вниз
    public float torqueYaw = 2f; // сила поворота

    private Rigidbody _rb;

    // переменная для хранения ввода между Update и FixedUpdate
    private Vector3 _moveInput;
    private float _rotateInput;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();

        _rb.useGravity = false; // отключаем гравитацию, чтобы объект плавал
        _rb.linearDamping = 2f; // сопротивление движению (вода тормозит)
        _rb.angularDamping = 3f; // сопротивление вращению
    }

    private void Update()
{
    // Левый стик (WASD) — движение
    float forward = 0f;
    float side    = 0f;

    if (Input.GetKey(KeyCode.W)) forward =  1f;  // вперёд
    if (Input.GetKey(KeyCode.S)) forward = -1f;  // назад
    if (Input.GetKey(KeyCode.A)) side = -1f;  // стрейф влево
    if (Input.GetKey(KeyCode.D)) side =  1f;  // стрейф вправо

   // Правый стик - Стрелки
    float up = 0f;
    float rotate = 0f;

    if (Input.GetKey(KeyCode.UpArrow)) up = 1f;  // вверх
    if (Input.GetKey(KeyCode.DownArrow)) up = -1f;  // вниз
    if (Input.GetKey(KeyCode.LeftArrow)  || Input.GetKey(KeyCode.PageUp))   rotate = -1f; //поворот налево
    if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.PageDown)) rotate =  1f; // поворот направо

        // Определяем какой KeyCode у твоих стрелок
    if (Input.anyKeyDown)
    {
        foreach (KeyCode kc in System.Enum.GetValues(typeof(KeyCode)))
        {
            if (Input.GetKeyDown(kc))
                Debug.Log("Нажата клавиша: " + kc);
        }
    }

    _moveInput = new Vector3(side, up, forward);
    _rotateInput = rotate;
}

    private void FixedUpdate()
    {
        Debug.Log("moveInput: " + _moveInput + " | rotate: " + _rotateInput);
        _rb.AddRelativeForce(new Vector3(
        _moveInput.x * thrustSide,      // A/D — вправо/влево
        _moveInput.y * thrustVertical,  // стрелки — вверх/вниз
        _moveInput.z * thrustForward    // W/S — вперёд/назад
    ), ForceMode.Force);
        
        _rb.AddRelativeTorque(
    Vector3.up * _rotateInput * torqueYaw,
    ForceMode.Force
    );
    }
}