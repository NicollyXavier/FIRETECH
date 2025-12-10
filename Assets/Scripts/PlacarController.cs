using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;
using System;
using UnityEngine.SceneManagement;

public class PlacarController : MonoBehaviour
{
    public string url = "https://api-vr-production.up.railway.app/resultados";
    public Transform content;
    public GameObject linhaPrefab;

    void Start()
    {
        StartCoroutine(BuscarResultados());
    }

    public void VoltarMenuInicial()
    {
        SceneManager.LoadScene("Cena_MenuInicial");
    }

    IEnumerator BuscarResultados()
    {
        using (UnityWebRequest www = UnityWebRequest.Get(url))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log("ERRO API: " + www.error);
                yield break;
            }

            string json = www.downloadHandler.text;

            Resultado[] lista = JsonHelper.FromJson<Resultado>(json);

            foreach (Resultado r in lista)
            {
                GameObject linha = Instantiate(linhaPrefab, content);

                string nomeUsuario = r.usuario != null ? r.usuario.nome : "Sem nome";
                string dataFormatada = "sem data";

                if (!string.IsNullOrEmpty(r.data))
                {
                    try
                    {
                        DateTime data = DateTime.Parse(r.data);
                        dataFormatada = data.ToString("dd/MM/yyyy"); // <-- Aqui decide o formato!
                    }
                    catch
                    {
                        dataFormatada = r.data; // fallback
                    }
                }

                linha.GetComponent<TextMeshProUGUI>().text =
                    $"{nomeUsuario} — {r.pontuacao} pontos — {r.acertos} acertos — {r.erros} erros — {dataFormatada}";
            }
        }
    }
}
