using Dominios.Classes;
using Dominios.Interfaces;
using Infra.Data.Contextos;
using Servicos.ViewModels;
using System;
using System.Collections.Generic;
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

        public bool criarSessao(int Recebido)
        {
            throw new NotImplementedException();
        }

        public bool deletarSessao()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public Sessoes obterStatus()
        {
            throw new NotImplementedException();
        }
    }
}
