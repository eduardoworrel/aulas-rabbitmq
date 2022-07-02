namespace Models;

public class PublicacaoRefinada{
    public int Id {get; set;}
    public string? CidadeDaPublicacao {get; set;}
    public string Texto {get; set;}
    public DateTime? DataHoraCriacao {get; set;}

    public string? IP {get; set;}
    
}