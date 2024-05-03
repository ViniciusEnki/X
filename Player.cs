using UnityEngine;

public class BlockInteraction : MonoBehaviour
{
    public GameObject blockPrefab; // Prefab do bloco a ser colocado
    public float placementDistance = 10f; // Dist�ncia m�xima para colocar um bloco
    public float interactionDistance = 10f; // Dist�ncia m�xima para interagir com blocos
    public LayerMask blockLayerMask; // Camada dos blocos

    void Update()
    {
        // Verifica se o bot�o esquerdo do mouse est� pressionado (para quebrar blocos)
        if (Input.GetKey(KeyCode.Mouse0))
        {
            BreakBlock();
        }

        // Verifica se o bot�o direito do mouse est� pressionado (para adicionar blocos)
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            PlaceBlock();
        }
    }

    void BreakBlock()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        // Lan�a um raio a partir do ponto de clique do mouse
        if (Physics.Raycast(ray, out hit, interactionDistance, blockLayerMask))
        {
            // Verifica se o objeto atingido � um bloco
            if (hit.collider.CompareTag("Block"))
            {
                // Destroi o bloco atingido
                Destroy(hit.collider.gameObject);
            }
        }
    }

    void PlaceBlock()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        // Lan�a um raio a partir do ponto de clique do mouse
        if (Physics.Raycast(ray, out hit, placementDistance, blockLayerMask))
        {
            // Verifica se o objeto atingido n�o � um bloco (espa�o vazio)
            if (!hit.collider.CompareTag("Block"))
            {
                // Obt�m a posi��o de coloca��o do bloco
                Vector3 placePosition = hit.point + hit.normal / 2f;

                // Arredonda a posi��o para o grid
                placePosition = new Vector3(
                    Mathf.Round(placePosition.x),
                    Mathf.Round(placePosition.y),
                    Mathf.Round(placePosition.z)
                );

                // Instancia um novo bloco na posi��o calculada
                Instantiate(blockPrefab, placePosition, Quaternion.identity);
            }
        }
    }
}
