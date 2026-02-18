using System.ComponentModel.DataAnnotations;

namespace FTS.App.Components.Pages.Login.Models;

public class RegisterModel
{
    [Required(ErrorMessage = "Nazwa jest wymagana")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "Email jest wymagany")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Has³o jest wymagane")]
    public string Password { get; set; } = string.Empty;

    [Required(ErrorMessage = "Potwierdzenie has³a jest wymagane")]
    [Compare(nameof(Password), ErrorMessage = "Has³a musz¹ siê zgadzaæ")]
    public string ConfirmPassword { get; set; } = string.Empty;
}
