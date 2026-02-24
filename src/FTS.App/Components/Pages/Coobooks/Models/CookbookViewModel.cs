using System.ComponentModel.DataAnnotations;

namespace FTS.App.Components.Pages.Coobooks.Models;

public class CookbookViewModel
{
    public Guid Id { get; set; }
    [Required(ErrorMessage = "Nazwa jest wymagana")]
    public string Name { get; set; }
}
