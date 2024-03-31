using CatalogoJogos.Nucleo.InputModel;
using CatalogoJogos.Nucleo.ViewModel;

namespace CatalogoJogos.Nucleo.Interface
{
    public interface IJogoService : IDisposable
    {
        Task<List<JogoResponse>> Obter(int pagina, int quantidade);
        Task<JogoResponse> Obter(Guid id);
        Task<JogoResponse> Inserir(JogoRequest jogo);
        Task Atualizar(Guid id, JogoRequest jogo);
        Task Atualizar(Guid id, double preco);
        Task Remover(Guid id);
    }
}
