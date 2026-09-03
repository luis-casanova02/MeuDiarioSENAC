public class RegistrosBusiness
{
    private readonly Registro _registro;

    public RegistrosBusiness(Registro registro)
    {
        _registro = registro;
    }

    public bool TituloInformado()
    {
        if (string.IsNullOrEmpty(_registro.Titulo))
        {
            throw new ArgumentException("O título do registro deve ser preenchido.");
        }
        return true;
    }
}