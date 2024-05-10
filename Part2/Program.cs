using System;
using System.Collections.Generic;
using System.Linq;

class Ingredient
{
    public string Name { get; set; }
    public double Quantity { get; set; }
    public string Unit { get; set; }
    public int Calories { get; set; }
    public string FoodGroup { get; set; }
}

class Recipe
{
    public string Name { get; set; }
    private List<Ingredient> ingredients;
    private List<string> steps;

    public Recipe(string name)
    {
        Name = name;
        ingredients = new List<Ingredient>();
        steps = new List<string>();
    }

    public void AddIngredient(string name, double quantity, string unit, int calories, string foodGroup)
    {
        ingredients.Add(new Ingredient
        {
            Name = name,
            Quantity = quantity,
            Unit = unit,
            Calories = calories,
            FoodGroup = foodGroup
        });
    }

    public void AddStep(string step)
    {
        steps.Add(step);
    }

    public double CalculateTotalCalories()
    {
        return ingredients.Sum(i => i.Calories * i.Quantity);
    }

    public void DisplayRecipe()
    {
        Console.WriteLine($"Recipe: {Name}");
        Console.WriteLine("Ingredients:");
        foreach (var ingredient in ingredients)
        {
            Console.WriteLine($"{ingredient.Quantity} {ingredient.Unit} of {ingredient.Name}");
        }
        Console.WriteLine("\nSteps:");
        for (int i = 0; i < steps.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {steps[i]}");
        }
        Console.WriteLine($"Total Calories: {CalculateTotalCalories()}");
    }
}

class RecipeManager
{
    private List<Recipe> recipes;

    public RecipeManager()
    {
        recipes = new List<Recipe>();
    }

    public void AddRecipe(Recipe recipe)
    {
        recipes.Add(recipe);
    }

    public void DisplayRecipes()
    {
        if (recipes.Count == 0)
        {
            Console.WriteLine("No recipes available.");
            return;
        }
        Console.WriteLine("Recipes:");
        foreach (var recipe in recipes.OrderBy(r => r.Name))
        {
            Console.WriteLine(recipe.Name);
        }
    }

    public Recipe GetRecipe(string name)
    {
        return recipes.FirstOrDefault(r => r.Name == name);
    }

    public void NotifyExceedingCalories(Recipe recipe)
    {
        if (recipe.CalculateTotalCalories() > 300)
        {
            Console.WriteLine($"Warning: The total calories of recipe '{recipe.Name}' exceed 300.");
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        RecipeManager recipeManager = new RecipeManager();

        while (true)
        {
            Console.WriteLine("\nOptions:");
            Console.WriteLine("1. Add Recipe");
            Console.WriteLine("2. Display Recipes");
            Console.WriteLine("3. View Recipe Details");
            Console.WriteLine("4. Exit");
            Console.Write("Enter your choice: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Console.Write("\nEnter recipe name: ");
                    string recipeName = Console.ReadLine();
                    Recipe recipe = new Recipe(recipeName);

                    Console.Write("\nEnter the number of ingredients: ");
                    int ingredientCount = int.Parse(Console.ReadLine());

                    for (int i = 0; i < ingredientCount; i++)
                    {
                        Console.WriteLine($"\nIngredient {i + 1}:");
                        Console.Write("Name: ");
                        string name = Console.ReadLine();
                        Console.Write("Quantity: ");
                        double quantity = double.Parse(Console.ReadLine());
                        Console.Write("Unit: ");
                        string unit = Console.ReadLine();
                        Console.Write("Calories: ");
                        int calories = int.Parse(Console.ReadLine());
                        Console.Write("Food Group: ");
                        string foodGroup = Console.ReadLine();
                        recipe.AddIngredient(name, quantity, unit, calories, foodGroup);
                    }

                    Console.Write("\nEnter the number of steps: ");
                    int stepCount = int.Parse(Console.ReadLine());

                    for (int i = 0; i < stepCount; i++)
                    {
                        Console.WriteLine($"\nStep {i + 1}:");
                        Console.Write("Description: ");
                        string description = Console.ReadLine();
                        recipe.AddStep(description);
                    }

                    recipeManager.AddRecipe(recipe);
                    recipeManager.NotifyExceedingCalories(recipe);
                    break;

                case "2":
                    recipeManager.DisplayRecipes();
                    break;

                case "3":
                    Console.Write("\nEnter recipe name to view details: ");
                    string recipeToView = Console.ReadLine();
                    Recipe selectedRecipe = recipeManager.GetRecipe(recipeToView);
                    if (selectedRecipe != null)
                    {
                        selectedRecipe.DisplayRecipe();
                    }
                    else
                    {
                        Console.WriteLine("Recipe not found.");
                    }
                    break;

                case "4":
                    Console.WriteLine("\nExiting the program...");
                    return;

                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }
    }
}
