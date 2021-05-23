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

        public abstract Lutador VerificarDesempate(IEnumerable<Lutador> lutadores);

        public abstract int CalcularGanhador(Lutador lutadores);

    }

    public class TorneioLuta : Lutador
    {
        public override IEnumerable<Lutador> VerificarGanhador(IEnumerable<Lutador> lutadores)
        {

            List<Lutador> resultadoGanhador = new List<Lutador>();
            List<Lutador> resultadoLuta = new List<Lutador>();
            foreach (var item in lutadores)
            {

                resultadoLuta.Add(item);

                if (resultadoLuta.Count() == 2)
                {
                    
                    int lutador1 = CalcularGanhador(resultadoLuta[0]);

                    var lutador2 = CalcularGanhador(resultadoLuta[1]);


                    if (lutador1 > lutador2)
                    {
                        resultadoGanhador.Add(resultadoLuta[0]);
                    }
                    else if(lutador2 > lutador1)
                    {
                        resultadoGanhador.Add(resultadoLuta[1]);
                    }
                    if(lutador1 == lutador2)
                    {
                        resultadoGanhador.Add(VerificarDesempate(resultadoLuta));
                    }                    
                    
                    resultadoLuta.Clear();

                }

            }

            return resultadoGanhador;
        }


        public override Lutador VerificarDesempate(IEnumerable<Lutador> lutadores)
        {
       
            if(lutadores.ToList()[0].ArtesMarciais.Count() > lutadores.ToList()[1].ArtesMarciais.Count())
            {
                return lutadores.ToList()[0];
            }
            else if(lutadores.ToList()[1].ArtesMarciais.Count() > lutadores.ToList()[0].ArtesMarciais.Count())
            {
                return lutadores.ToList()[1];
            }
            else if (lutadores.ToList()[0].Lutas > lutadores.ToList()[1].Lutas)
            {
                return lutadores.ToList()[0];
            }
            else if(lutadores.ToList()[1].Lutas > lutadores.ToList()[0].Lutas )
            {
                return lutadores.ToList()[1];
            }

            return null;
        
        }

        public override int CalcularGanhador(Lutador lutadores)
        {
            return (int)((lutadores.Lutas * 100) / lutadores.Vitorias);
        }
    }
}
