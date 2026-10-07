
using Microsoft.EntityFrameworkCore;

public class RegistroDAO
{
    private MeuDiarioSENACContext conexao = new MeuDiarioSENACContext();
    public void Inserir (Registro registro)
    {
        conexao.Registros.Add(registro);
        conexao.SaveChanges();
    }
    public List<Registro> ListarTodos()
    {
        return conexao.Registros
            .AsNoTracking()
            .Include(r => r.Usuario)
            .ToList();
    }

    public Registro BuscarPorID(int Id)
    {
        return conexao.Registros
            .Include(r => r.Usuario)
            .FirstOrDefault(r => r.Id == Id);
    }

     public void Atualizar(Registro registro)
    {
        conexao.Registros.Update(registro);
        conexao.SaveChanges();
    }
 
    public void Excluir(int id)
    {
        var registro = conexao.Registros.Find(id);
        if (registro != null)
        {
            conexao.Registros.Remove(registro);
            conexao.SaveChanges();
        }
    }    
}
