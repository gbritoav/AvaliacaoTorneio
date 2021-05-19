using System.Collections.Generic;
using System.Linq;

namespace AvaliacaoTorneio.Models
{
    public abstract class Lutador
    {

        public int Id { get; set; }

        public string Nome { get; set; }

        public int Idade { get; set; }

        public List<string> ArtesMarciais { get; set; }

        public int Lutas { get; set; }

        public int Derrotas { get; set; }

        public int Vitorias { get; set; }

        public abstract IEnumerable<Lutador> VerificarGanhador(IEnumerable<Lutador> lutadores);


    }

    public class TorneioLuta : Lutador
    {

        public IEnumerable<Lutador> Inicio(IEnumerable<Lutador> lutadores)
        {
           
          return lutadores.OrderBy(x => x.Idade).Where(x => x.Vitorias >= 24 && x.Lutas >= 24).Take(16);
         
          
        }


        public override IEnumerable<Lutador> VerificarGanhador(IEnumerable<Lutador> lutadores)
        {

            List<Lutador> resultadoGanhador = new List<Lutador>();
            List<Lutador> resultadoLuta = new List<Lutador>();
            foreach (var item in lutadores)
            {
               
                resultadoLuta.Add(item);

                if (resultadoLuta.Count()==2)
                {
                  
                    resultadoGanhador.Add(resultadoLuta.OrderByDescending(x => x.Lutas).First() );
                    resultadoLuta.Clear();
                    
                }
                
            }

            return resultadoGanhador;
        }



        public Lutador Final(IEnumerable<Lutador> lutadores)
        {


            return lutadores.OrderByDescending(x => x.Lutas).First();
        }


    }
}
