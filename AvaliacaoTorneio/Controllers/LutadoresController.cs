using AvaliacaoTorneio.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace AvaliacaoTorneio.Controllers
{
    public class LutadoresController : Controller
    {

        string caminho = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("ApiTorneio")["Caminho"];
        string key = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("ApiTorneio")["key"];
        IEnumerable<Lutador> lutadores = null;


        public IActionResult Index()
        {
            lutadores = GetLutadores();

            if (lutadores != null)
            {
                return View(lutadores);
            }
            else
            {
                return NotFound("Erro no servidor. Contate o Administrador.");
            }

        }

        [HttpGet]
        public async Task<ActionResult> IniciarLutas(int[] Id)
        {
            if (Id.Count() == 16)
            {
                TorneioLuta lutador = new TorneioLuta();


                var lutadores = GetLutadores().Where(x => Id.Contains(x.Id)).ToList();              

                if (lutadores.Count() > 0)
                {
                    var inicio = lutadores.OrderBy(x => x.Idade);

                    var oitavas = lutador.VerificarGanhador(inicio);
                    var quartas = lutador.VerificarGanhador(oitavas);
                    var semifinal = lutador.VerificarGanhador(quartas);
                    var vencedor = lutador.VerificarGanhador(semifinal).First();

                    return View(vencedor);
                }
                else
                {
                    return NotFound("Não há lutadores para iniciar a partida.");
                }
            }
            else
            {

                return NotFound("É necessário escolher 16 lutadores.");
            }
        }
        public IEnumerable<Lutador> GetLutadores()
        {


            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(caminho);

                client.DefaultRequestHeaders.Add("x-api-key", key);


                var responseTask = client.GetAsync("api/competidores");


                responseTask.Wait();
                var result = responseTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<TorneioLuta>>();
                    readTask.Wait();
                    lutadores = readTask.Result;
                }

            }

            return lutadores;
        }




    }
}
