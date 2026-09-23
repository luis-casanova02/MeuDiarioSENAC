public class RegistroService
{
    private readonly RegistroDAO _registroDAO;

    public RegistroService(RegistroDAO registroDAO)
    {
        _registroDAO = registroDAO;
    }

    public void InserirRegistro(Registro registro)
    {
        ValidarRegistro(registro);
        _registroDAO.Inserir(registro);
    }

    public List<Registro> ListarTodos()
    {
        return _registroDAO.ListarTodos();
    }

    public Registro? BuscarRegistroPorId(int id)
    {
        return _registroDAO.BuscarPorID(id);
    }

    public void AtualizarRegistro(Registro registro)
    {
        var existente = _registroDAO.BuscarPorID(registro.Id);
        if (existente == null)
            throw new InvalidOperationException("Registro não encontrado.");

        ValidarRegistro(registro);
        _registroDAO.Atualizar(registro);
    }

    public void ExcluirRegistro(int id)
    {
        _registroDAO.Excluir(id);
    }

    private void ValidarRegistro(Registro registro)
    {
        var business = new RegistrosBusiness(registro);
        business.TituloInformado();
        // outras validações de negócio entram aqui
    }
}