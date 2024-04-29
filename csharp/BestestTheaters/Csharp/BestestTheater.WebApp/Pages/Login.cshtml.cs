using System.Data.SQLite;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BestestTheater.WebApp.Pages;

public class Login : PageModel
{
    [BindProperty]
    public string Username { get; set; }

    [BindProperty]
    public string Password { get; set; }
    
    [BindProperty]
    public bool LoginFailed { get; set; }
    
    public void OnGet(bool loginFailed = false)
    {
        LoginFailed = loginFailed;
    }
    
    public IActionResult OnPost()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }


        // TODO 2015: This is in the TechnicalDebt.xls file
        using var connection = new SQLiteConnection("Data Source=./database/BestestTheater.db");
        connection.Open();

        var cmd = connection.CreateCommand();
        cmd.CommandText = "SELECT * FROM AppUsers WHERE LoginName = @username AND Password = @password";
        cmd.Parameters.AddWithValue("@username", Username);
        cmd.Parameters.AddWithValue("@password", Password);

        using var reader = cmd.ExecuteReader();
        if (reader.Read())
        {
            // The username and password match, set the session variable and redirect to the bookings page
            HttpContext.Session.SetString("isLoggedIn", "true");
            return RedirectToPage("/MyBookings");
        }
        else
        {
            // The username and password don't match, return to the login page with an error message
            return RedirectToPage("/Login", new { loginFailed = true });
        }
        
        return RedirectToPage("/MyBookings");
    }
}