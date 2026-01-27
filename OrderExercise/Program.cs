using OrderExercise.Controllers;
using OrderExercise.Application;
using OrderExercise.Domain;
using OrderExercise.Repository;
using System.Collections.Generic;
using System.Text.Json;

class Program
{
    public static void Main()
    {
        OrderConsoleController program_start = new OrderConsoleController();
        program_start.Start();
    }
}