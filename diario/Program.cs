var diario = new RegistroDAO();
var contexto = new MeuDiarioSENACContext();
RegistrosBusiness registrosBusiness = new RegistrosBusiness(new Registro());

Usuario usuarioPadrao = contexto.Usuarios.FirstOrDefault(u => u.Nome == "Usuário Padrão");
if (usuarioPadrao == null)
{
    usuarioPadrao = new Usuario { Nome = "Usuário Padrão" };
    contexto.Usuarios.Add(usuarioPadrao);
    contexto.SaveChanges();
}

void CriarRegistro(RegistroDAO diario, int usuarioId)
{
    Console.WriteLine("Titulo: ");
    string titulo = Console.ReadLine();

    DateTime data;
    try
    {
        Console.WriteLine("Data (yyyy-MM-dd): ");
        data = DateTime.Parse(Console.ReadLine());
    }
    catch (FormatException)
    {
        Console.WriteLine("\nFormato de data inválido. Por favor, use o formato yyyy-MM-dd, com mascara.\n");
        return;
    }

    try
    {
        registrosBusiness.TituloInformado();
        Console.WriteLine("Conteudo: ");
        string conteudo = Console.ReadLine();
        diario.Inserir(new Registro
        {
            Titulo = titulo,
            Data = data,
            Conteudo = conteudo,
            UsuarioId = usuarioId
        });
        Console.WriteLine("\nRegistro inserido com sucesso!\n");
    }
    catch (ArgumentException ex)
    {
        Console.WriteLine($"\nErro: {ex.Message}\n");
    }
}

void ListarRegistros(RegistroDAO diario)
{
    var registros = diario.ListarTodos();

    if (registros.Count == 0)
    {
        Console.WriteLine("\nNenhum registro encontrado.\n");
        return;
    }

    foreach (var r in registros)
    {
        Console.WriteLine($"ID: {r.Id}, Titulo: {r.Titulo}, Data: {r.Data}, Conteudo: {r.Conteudo}, Usuario: {r.Usuario?.Nome}");
    }
}

void BuscarRegistro(RegistroDAO diario)
{
    Console.WriteLine("Digite o ID do registro: ");
    int id = int.Parse(Console.ReadLine());

    var registro = diario.BuscarPorID(id);

    if (registro == null)
    {
        Console.WriteLine("\nRegistro não encontrado.\n");
        return;
    }

    Console.WriteLine($"ID: {registro.Id}, Titulo: {registro.Titulo}, Data: {registro.Data}, Conteudo: {registro.Conteudo}, Usuario: {registro.Usuario?.Nome}");
}

void AtualizarRegistro(RegistroDAO diario)
{
    Console.WriteLine("Digite o ID do registro a ser atualizado: ");
    int id = int.Parse(Console.ReadLine());

    var registro = diario.BuscarPorID(id);
    if (registro == null)
    {
        Console.WriteLine("\nRegistro não encontrado.\n");
        return;
    }

    Console.WriteLine("Novo titulo: ");
    registro.Titulo = Console.ReadLine();

    Console.WriteLine("Novo conteudo: ");
    registro.Conteudo = Console.ReadLine();

    diario.Atualizar(registro);
    Console.WriteLine("\nRegistro atualizado com sucesso!\n");
}

void ExcluirRegistro(RegistroDAO diario)
{
    Console.WriteLine("Digite o ID do registro a ser excluído: ");
    int id = int.Parse(Console.ReadLine());

    diario.Excluir(id);
    Console.WriteLine("\nRegistro excluído com sucesso!\n");
}


while (true)
{
    Console.WriteLine("Escolha uma opção:");
    Console.WriteLine("1 - Inserir registro");
    Console.WriteLine("2 - Listar registros");
    Console.WriteLine("3 - Buscar registro por ID");
    Console.WriteLine("4 - Atualizar registro");
    Console.WriteLine("5 - Excluir registro");
    Console.WriteLine("6 - Sair");

    var opcao = Console.ReadLine();

    switch (opcao)
    {
        case "1":
            CriarRegistro(diario, usuarioPadrao.Id);
            Continue();
            break;
        case "2":
            ListarRegistros(diario);
            Continue();
            break;
        case "3":
            BuscarRegistro(diario);
            Continue();
            break;
        case "4":
            AtualizarRegistro(diario);
            Continue();
            break;
        case "5":
            ExcluirRegistro(diario);
            Continue();
            break;
        case "6":
            return;
        default:
            Console.WriteLine("\nOpção inválida.\n");
            break;
    }
}

void Continue()
{
    Console.WriteLine("\nPressione enter para continuar...");
    Console.ReadLine();
    Console.Clear();
}