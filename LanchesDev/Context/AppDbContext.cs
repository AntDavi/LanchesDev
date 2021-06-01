using LanchesDev.Models;
using Microsoft.EntityFrameworkCore;

namespace LanchesDev.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext( DbContextOptions<AppDbContext> options ) : base(options)
        {}

        public DbSet<Lanche> Lanches { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<CarrinhoCompraItem> CarrinhoCompraItens { get; set; }
        public DbSet<Pedido> Pedido { get; set; }
        public DbSet<PedidoDetalhe> PedidoDetalhe { get; set; }
<<<<<<< HEAD
        public object Pedidos { get; internal set; }
=======
>>>>>>> e08d1b70471f4002fd55b72227e1399e8136fa8a
    }
}
