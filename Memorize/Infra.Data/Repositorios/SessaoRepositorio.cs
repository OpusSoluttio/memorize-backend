using Dominios.Classes;
using Dominios.Interfaces;
using Infra.Data.Contextos;
using Servicos.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Infra.Data.Repositorios
{
    public class SessaoRepositorio : ISessao, IDisposable
    {

        private MemoRizeContext _context;

        //Utiliza injeção de dependência.
        public SessaoRepositorio(MemoRizeContext context)
        {
            _context = context;
        }


        public bool criarSessao(Sessoes SessaoRecebida)
        {
            try
            {
                _context.Sessao.Add(SessaoRecebida);
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        
        public ObterStatusViewModel obterStatus()
        {
            try
            {
                var Lista = _context.Sessao.ToList();
                Sessoes sessaoRetornar = new Sessoes();

                if(Lista.Count == 0)
                {
                    return null;
                }

                foreach (var item in Lista)
                {
                    sessaoRetornar = item;
                }

                var sequenciaCorretaSeparada = sessaoRetornar.SequenciaCorreta.Split(";");

                int[] sequenciaCorreta = new int[sequenciaCorretaSeparada.Length];

                for (int i = 0; i < sequenciaCorretaSeparada.Length; i++)
                {
                    sequenciaCorreta[i] = (int.Parse(sequenciaCorretaSeparada[i]));
                }

                var SequenciaRecebidaSeparada = sessaoRetornar.SequenciaRecebida.Split(";");

                int[] sequenciaRecebida = new int[SequenciaRecebidaSeparada.Length];

                if (sessaoRetornar.SequenciaRecebida.Length > 0 )
                {
                    for (int i = 0; i < SequenciaRecebidaSeparada.Length; i++)
                    {
                        sequenciaRecebida[i] = (int.Parse(SequenciaRecebidaSeparada[i]));
                    }
                }

                ObterStatusViewModel sessao = new ObterStatusViewModel()
                {
                    Id = sessaoRetornar.Id,
                    Fase = sessaoRetornar.Fase,
                    SequenciaCorreta = sequenciaCorreta,
                    SequenciaRecebida = sequenciaRecebida,
                    Errou = sessaoRetornar.Errou,
                    PassarDeFase = sessaoRetornar.PassarDeFase
                };

                return sessao;
                    
            }
            catch (Exception)
            {
                return null;
            }
        }

        public bool adicionarNaSequencia(int Recebido)
        {
            var Lista = _context.Sessao.ToList();
            Sessoes sessaoRetornar = new Sessoes();

            if (Lista.Count == 0)
            {
                return false;
            }

            foreach (var item in Lista)
            {
                sessaoRetornar = item;
            }
            try
            {
                if (sessaoRetornar.SequenciaRecebida.Length == 0) sessaoRetornar.SequenciaRecebida += $"{Recebido}";

                else sessaoRetornar.SequenciaRecebida += $";{Recebido}";

                var sequenciaRecebidaSeparada = sessaoRetornar.SequenciaRecebida.Split(";");
                var sequenciaCorretaSeparada = sessaoRetornar.SequenciaCorreta.Split(";");

                for (int i = 0; i < sequenciaRecebidaSeparada.Length; i++)
                {
                    if (sequenciaRecebidaSeparada[i] != sequenciaCorretaSeparada[i]) sessaoRetornar.Errou = true;
                }

                _context.Sessao.Update(sessaoRetornar);
                _context.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
            
        }

        public bool deletarSessao()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

    }
}
