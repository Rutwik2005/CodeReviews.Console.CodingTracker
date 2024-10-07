using Spectre.Console;

namespace CodingTracker.Rutwik2005
{
   public class Display
    {   public static void  DisplayMenu()
        {
            AnsiConsole.MarkupLine("[bold yellow]The Following operations can be performed:[/]");
            AnsiConsole.MarkupLine("[green]Press [[u]] for update[/]");
            AnsiConsole.MarkupLine("[green]Press [[d]] to delete[/]");
            AnsiConsole.MarkupLine("[green]Press [[i]] to insert[/]");
            AnsiConsole.MarkupLine("[green]Press [[v]] to view[/]");
            AnsiConsole.MarkupLine("[green]Press [[e]] to exit[/]");

        }
    }
}
