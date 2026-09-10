public class RegistroService
{
    private readonly RegistrosBusiness _registrosBusiness;

    public RegistroService(RegistrosBusiness registrosBusiness)
    {
        _registrosBusiness = registrosBusiness;
    }

    public void ValidarRegistro(Registro registro)
    {
        _registrosBusiness.TituloInformado();
        // Adicione outras validações conforme necessário
    }
}