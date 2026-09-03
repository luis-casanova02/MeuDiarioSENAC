public class Registro
{
    public int Id {get; set; }
    public string Titulo {get; set;}
    public DateTime Data {get; set;}
    public string Conteudo {get; set;}
    public Usuario Usuario {get; set;}
    public int UsuarioId{get;set;}
}