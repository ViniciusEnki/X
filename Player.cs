using UnityEngine;

public class BlockInteraction : MonoBehaviour
{
    public GameObject blockPrefab; // Prefab do bloco a ser colocado
    public float placementDistance = 10f; // Distância máxima para colocar um bloco
    public float interactionDistance = 10f; // Distância máxima para interagir com blocos
    public LayerMask blockLayerMask; // Camada dos blocos

    void Update()
    {
        // Verifica se o botão esquerdo do mouse está pressionado (para quebrar blocos)
        if (Input.GetKey(KeyCode.Mouse0))
        {
            BreakBlock();
        }

        // Verifica se o botão direito do mouse está pressionado (para adicionar blocos)
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            PlaceBlock();
        }
    }

    void BreakBlock()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        // Lança um raio a partir do ponto de clique do mouse
        if (Physics.Raycast(ray, out hit, interactionDistance, blockLayerMask))
        {
            // Verifica se o objeto atingido é um bloco
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

        // Lança um raio a partir do ponto de clique do mouse
        if (Physics.Raycast(ray, out hit, placementDistance, blockLayerMask))
        {
            // Verifica se o objeto atingido não é um bloco (espaço vazio)
            if (!hit.collider.CompareTag("Block"))
            {
                // Obtém a posição de colocação do bloco
                Vector3 placePosition = hit.point + hit.normal / 2f;

                // Arredonda a posição para o grid
                placePosition = new Vector3(
                    Mathf.Round(placePosition.x),
                    Mathf.Round(placePosition.y),
                    Mathf.Round(placePosition.z)
                );

                // Instancia um novo bloco na posição calculada
                Instantiate(blockPrefab, placePosition, Quaternion.identity);
            }
        }
    }
}
