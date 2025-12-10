using UnityEngine;

public class VRCamera : MonoBehaviour
{
    [Header("Configurações Gerais")]
    public bool isVRMode = false;   // True = modo headset, False = modo PC
    public bool isDragging = false; // Detecta se o mouse está sendo arrastado
    public float sensitivity = 0.2f; // Velocidade da rotação

    private float startMouseX;
    private float startMouseY;
    private Camera cam;

    void Start()
    {
        cam = GetComponent<Camera>();

         // ADICIONE ESTAS LINHAS:
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        // Detecta automaticamente se está em modo VR (para o futuro)
#if UNITY_XR_MANAGEMENT
        if (UnityEngine.XR.Management.XRGeneralSettings.Instance.Manager.activeLoader != null)
            isVRMode = true;
#endif
    }

    void Update()
    {
        // Somente ativa o controle de mouse se NÃO for VR
        if (!isVRMode)
        {
            if (Input.GetMouseButtonDown(0) && !isDragging)
            {
                isDragging = true;
                startMouseX = Input.mousePosition.x;
                startMouseY = Input.mousePosition.y;
            }
            else if (Input.GetMouseButtonUp(0) && isDragging)
            {
                isDragging = false;
            }
        }
    }

    void LateUpdate()
    {
        if (!isVRMode && isDragging)
        {
            float endMouseX = Input.mousePosition.x;
            float endMouseY = Input.mousePosition.y;

            float diffX = endMouseX - startMouseX;
            float diffY = endMouseY - startMouseY;

            // Ignora cliques leves (sem arrasto real)
            if (Mathf.Abs(diffX) < 0.5f && Mathf.Abs(diffY) < 0.5f)
                return;

            float newCenterX = Screen.width / 2 + diffX * sensitivity;
            float newCenterY = Screen.height / 2 + diffY * sensitivity;

            Vector3 lookHerePoint = cam.ScreenToWorldPoint(
                new Vector3(newCenterX, newCenterY, cam.nearClipPlane)
            );

            transform.LookAt(lookHerePoint);

            startMouseX = endMouseX;
            startMouseY = endMouseY;
        }
    }
}
