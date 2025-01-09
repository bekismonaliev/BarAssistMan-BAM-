using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;



class Program
{
    static async Task Main(string[] args)
    {
        // Veritabanı bağlamını başlat
        var context = new ApplicationDbContext();

        // Stok ve tarif yöneticilerini oluştur
        var stockManager = new StockManager(context);
        var recipeManager = new RecipeManager(context);

        // Konsol menüsünü çalıştır
        var menu = new ConsoleMenu(stockManager, recipeManager);
        await menu.ShowMenu();
    }
}


public class ApplicationDbContext : DbContext
{
    public DbSet<Ingredient> Ingredients { get; set; }
    public DbSet<Recipe> Recipes { get; set; }
    public DbSet<RecipeIngredient> RecipeIngredients { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderDetail> OrderDetails { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=BSM;Trusted_Connection=True;Encrypt=False;");


    }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<RecipeIngredient>()
            .HasOne(ri => ri.Recipe)
            .WithMany(r => r.RecipeIngredients)
            .HasForeignKey(ri => ri.RecipeID);

        modelBuilder.Entity<RecipeIngredient>()
            .HasOne(ri => ri.Ingredient)
            .WithMany()
            .HasForeignKey(ri => ri.IngredientID);

        modelBuilder.Entity<OrderDetail>()
            .HasOne(od => od.Order)
            .WithMany(o => o.OrderDetails)
            .HasForeignKey(od => od.OrderID);

        modelBuilder.Entity<OrderDetail>()
            .HasOne(od => od.Recipe)
            .WithMany()
            .HasForeignKey(od => od.RecipeID);
    }
}

public class Ingredient
{
    public int ID { get; set; }
    public string Name { get; set; }
    public decimal Quantity { get; set; }
    public string Unit { get; set; }
    public decimal CriticalLevel { get; set; }
}

public class Recipe
{
    public int ID { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public ICollection<RecipeIngredient> RecipeIngredients { get; set; }
}

public class RecipeIngredient
{
    public int ID { get; set; }
    public int RecipeID { get; set; }
    public Recipe Recipe { get; set; }
    public int IngredientID { get; set; }
    public Ingredient Ingredient { get; set; }
    public decimal Quantity { get; set; }
}

public class Order
{
    public int ID { get; set; }
    public DateTime OrderDate { get; set; }
    public ICollection<OrderDetail> OrderDetails { get; set; }
}

public class OrderDetail
{
    public int ID { get; set; }
    public int OrderID { get; set; }
    public Order Order { get; set; }
    public int RecipeID { get; set; }
    public Recipe Recipe { get; set; }
    public int Quantity { get; set; }
}

public class StockManager
{
    private readonly ApplicationDbContext _context;

    public StockManager(ApplicationDbContext context)
    {
        _context = context;
    }

    public void CheckStockLevels()
    {
        var lowStockItems = _context.Ingredients
            .Where(i => i.Quantity <= i.CriticalLevel)
            .ToList();

        if (lowStockItems.Any())
        {
            Console.WriteLine("Dikkat! Asagidaki malzemelerin stogu kritik seviyenin altinda:");
            foreach (var item in lowStockItems)
            {
                Console.WriteLine($"- {item.Name}: {item.Quantity} {item.Unit}");
            }
        }
        else
        {
            Console.WriteLine("Tum stoklar yeterli seviyede.");
        }
    }

    public void ReduceStock(int recipeId, int quantity)
    {
        var recipe = _context.Recipes
            .Include(r => r.RecipeIngredients)
            .ThenInclude(ri => ri.Ingredient)
            .FirstOrDefault(r => r.ID == recipeId);

        if (recipe == null)
        {
            Console.WriteLine("Tarif bulunamadi.");
            return;
        }

        foreach (var ingredient in recipe.RecipeIngredients)
        {
            var stockItem = ingredient.Ingredient;
            stockItem.Quantity -= ingredient.Quantity * quantity;

            if (stockItem.Quantity < 0)
            {
                Console.WriteLine($"Dikkat! {stockItem.Name} icin yetersiz stok.");
            }
        }

        _context.SaveChanges();
        Console.WriteLine("Stok seviyeleri guncellendi.");
    }

    public void AddStock(string name, decimal quantity, string unit, decimal criticalLevel)
    {
        var ingredient = _context.Ingredients.FirstOrDefault(i => i.Name == name);

        if (ingredient == null)
        {
            _context.Ingredients.Add(new Ingredient
            {
                Name = name,
                Quantity = quantity,
                Unit = unit,
                CriticalLevel = criticalLevel
            });
        }
        else
        {
            ingredient.Quantity += quantity;
        }

        _context.SaveChanges();
        Console.WriteLine($"{quantity} {unit} miktarinda {name} stoga eklendi.");
    }

    public void ListAllStocks()
    {
        var ingredients = _context.Ingredients.ToList();
        Console.WriteLine("Guncel Stok Seviyeleri:");
        foreach (var ingredient in ingredients)
        {
            Console.WriteLine($"- {ingredient.Name}: {ingredient.Quantity} {ingredient.Unit} (Kritik Seviye: {ingredient.CriticalLevel})");
        }
    }
}

public class RecipeManager
{
    private readonly ApplicationDbContext _context;
    private readonly string _openAIKey = "Your_OpenAI_API_Key";

    public RecipeManager(ApplicationDbContext context)
    {
        _context = context;
    }

    public void AddRecipeFromInput()
    {
        Console.WriteLine("Tarif adini girin:");
        string name = Console.ReadLine();

        Console.WriteLine("Tarif aciklamasini girin:");
        string description = Console.ReadLine();

        var ingredients = new Dictionary<string, decimal>();
        while (true)
        {
            Console.WriteLine("Malzeme adini girin (veya bitirmek icin 'done' yazin):");
            string ingredientName = Console.ReadLine();
            if (ingredientName.ToLower() == "done")
                break;

            Console.WriteLine($"{ingredientName} icin miktar girin:");
            if (!decimal.TryParse(Console.ReadLine(), out decimal quantity))
            {
                Console.WriteLine("Gecersiz miktar. Tekrar deneyin.");
                continue;
            }

            ingredients[ingredientName] = quantity;
        }

        AddRecipe(name, description, ingredients);
    }

    public void ListRecipes()
    {
        var recipes = _context.Recipes
            .Include(r => r.RecipeIngredients)
            .ThenInclude(ri => ri.Ingredient)
            .ToList();

        foreach (var recipe in recipes)
        {
            Console.WriteLine($"- {recipe.Name}: {recipe.Description}");
            foreach (var ri in recipe.RecipeIngredients)
            {
                Console.WriteLine($"  * {ri.Ingredient.Name}: {ri.Quantity} {ri.Ingredient.Unit}");
            }
        }
    }

    public async Task AddRecipeFromAPI(string query)
    {
        var apiResponse = await CallChatGPTAPI(query);

        if (apiResponse == null || apiResponse.Choices.Count == 0)
        {
            Console.WriteLine("Tarif bulunamadi.");
            return;
        }

        var recipeText = apiResponse.Choices[0].Message.Content;

        Console.WriteLine($"Alinan Tarif: {recipeText}");

        Console.WriteLine("Yukaridaki tarif icin bir ad girin:");
        string recipeName = Console.ReadLine();

        Console.WriteLine("Bir aciklama girin:");
        string recipeDescription = Console.ReadLine();

        var ingredients = new Dictionary<string, decimal>
        {
            { "Nane Yapraklari", 10 },
            { "Rom", 0.5m },
            { "Seker Surubu", 0.2m }
        };

        AddRecipe(recipeName, recipeDescription, ingredients);
    }

    private async Task<OpenAIResponse> CallChatGPTAPI(string query)
    {
        using var client = new HttpClient();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _openAIKey);

        var requestContent = new
        {
            model = "gpt-4",
            messages = new[]
            {
                new { role = "user", content = query }
            }
        };

        var content = new StringContent(JsonConvert.SerializeObject(requestContent), System.Text.Encoding.UTF8, "application/json");

        var response = await client.PostAsync("https://api.openai.com/v1/chat/completions", content);

        if (!response.IsSuccessStatusCode)
        {
            Console.WriteLine("API cagrisinda hata olustu.");
            return null;
        }

        var responseContent = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<OpenAIResponse>(responseContent);
    }

    private void AddRecipe(string name, string description, Dictionary<string, decimal> ingredients)
    {
        var recipe = new Recipe
        {
            Name = name,
            Description = description,
            RecipeIngredients = new List<RecipeIngredient>()
        };

        foreach (var ingredient in ingredients)
        {
            var ingredientEntity = _context.Ingredients.FirstOrDefault(i => i.Name == ingredient.Key);
            if (ingredientEntity == null)
            {
                Console.WriteLine($"Malzeme {ingredient.Key} bulunamadi. Atlanacak...");
                continue;
            }

            recipe.RecipeIngredients.Add(new RecipeIngredient
            {
                IngredientID = ingredientEntity.ID,
                Quantity = ingredient.Value
            });
        }

        _context.Recipes.Add(recipe);
        _context.SaveChanges();
        Console.WriteLine($"Tarif {name} basariyla eklendi.");
    }
}

public class OpenAIResponse
{
    [JsonProperty("choices")]
    public List<Choice> Choices { get; set; }
}

public class Choice
{
    [JsonProperty("message")]
    public Message Message { get; set; }
}

public class Message
{
    [JsonProperty("content")]
    public string Content { get; set; }
}

public class ConsoleMenu
{
    private readonly StockManager _stockManager;
    private readonly RecipeManager _recipeManager;

    public ConsoleMenu(StockManager stockManager, RecipeManager recipeManager)
    {
        _stockManager = stockManager;
        _recipeManager = recipeManager;
    }

    public async Task ShowMenu()
    {
        while (true)
        {
            Console.WriteLine("\n--- Ana Menu ---");
            Console.WriteLine("1. Yeni Malzeme Ekle");
            Console.WriteLine("2. Stoklari Listele");
            Console.WriteLine("3. Stok Durumunu Kontrol Et");
            Console.WriteLine("4. Tarif Ekle (Manuel)");
            Console.WriteLine("5. Tarif Ekle (API ile)");
            Console.WriteLine("6. Tarifleri Listele");
            Console.WriteLine("7. Siparis Isle");
            Console.WriteLine("8. Cikis");
            Console.Write("Seciminizi yapin: ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    AddIngredient();
                    break;
                case "2":
                    _stockManager.ListAllStocks();
                    break;
                case "3":
                    _stockManager.CheckStockLevels();
                    break;
                case "4":
                    _recipeManager.AddRecipeFromInput();
                    break;
                case "5":
                    Console.Write("Tarif sorgusunu girin: ");
                    string query = Console.ReadLine();
                    await _recipeManager.AddRecipeFromAPI(query);
                    break;
                case "6":
                    _recipeManager.ListRecipes();
                    break;
                case "7":
                    ProcessOrder();
                    break;
                case "8":
                    Console.WriteLine("Cikis yapiliyor...");
                    return;
                default:
                    Console.WriteLine("Gecersiz secim, lutfen tekrar deneyin.");
                    break;
            }
        }
    }

    private void AddIngredient()
    {
        Console.Write("Malzeme adini girin: ");
        string name = Console.ReadLine();

        Console.Write("Miktarini girin: ");
        if (!decimal.TryParse(Console.ReadLine(), out decimal quantity))
        {
            Console.WriteLine("Gecersiz miktar.");
            return;
        }

        Console.Write("Birimini girin: ");
        string unit = Console.ReadLine();

        Console.Write("Kritik seviyeyi girin: ");
        if (!decimal.TryParse(Console.ReadLine(), out decimal criticalLevel))
        {
            Console.WriteLine("Gecersiz kritik seviye.");
            return;
        }

        _stockManager.AddStock(name, quantity, unit, criticalLevel);
    }

    private void ProcessOrder()
    {
        Console.Write("Tarif ID'sini girin: ");
        if (!int.TryParse(Console.ReadLine(), out int recipeId))
        {
            Console.WriteLine("Gecersiz tarif ID.");
            return;
        }

        Console.Write("Kac adet siparis edilecegini girin: ");
        if (!int.TryParse(Console.ReadLine(), out int quantity))
        {
            Console.WriteLine("Gecersiz miktar.");
            return;
        }

        _stockManager.ReduceStock(recipeId, quantity);
    }
}
public class DataSeeder
{
    private readonly ApplicationDbContext _context;

    public DataSeeder(ApplicationDbContext context)
    {
        _context = context;
    }

    public void SeedData()
    {
        // Stoklar (Ingredients) Ekleniyor
        if (!_context.Ingredients.Any())
        {
            var ingredients = new List<Ingredient>
            {
                new Ingredient { Name = "Nane Yapraklari", Quantity = 500, Unit = "Gram", CriticalLevel = 50 },
                new Ingredient { Name = "Rom", Quantity = 5, Unit = "Litre", CriticalLevel = 1 },
                new Ingredient { Name = "Seker Surubu", Quantity = 2, Unit = "Litre", CriticalLevel = 0.5m },
                new Ingredient { Name = "Limon Suyu", Quantity = 3, Unit = "Litre", CriticalLevel = 1 },
                new Ingredient { Name = "Soda", Quantity = 10, Unit = "Litre", CriticalLevel = 2 }
            };

            _context.Ingredients.AddRange(ingredients);
            _context.SaveChanges();
        }

        // Tarifler (Recipes) Ekleniyor
        if (!_context.Recipes.Any())
        {
            var recipes = new List<Recipe>
            {
                new Recipe
                {
                    Name = "Mojito",
                    Description = "Ferahlatıcı bir içecek",
                    RecipeIngredients = new List<RecipeIngredient>
                    {
                        new RecipeIngredient { IngredientID = _context.Ingredients.First(i => i.Name == "Nane Yapraklari").ID, Quantity = 5 },
                        new RecipeIngredient { IngredientID = _context.Ingredients.First(i => i.Name == "Rom").ID, Quantity = 50 },
                        new RecipeIngredient { IngredientID = _context.Ingredients.First(i => i.Name == "Seker Surubu").ID, Quantity = 20 },
                        new RecipeIngredient { IngredientID = _context.Ingredients.First(i => i.Name == "Limon Suyu").ID, Quantity = 30 },
                        new RecipeIngredient { IngredientID = _context.Ingredients.First(i => i.Name == "Soda").ID, Quantity = 100 }
                    }
                },
                new Recipe
                {
                    Name = "Daiquiri",
                    Description = "Klasik bir kokteyl",
                    RecipeIngredients = new List<RecipeIngredient>
                    {
                        new RecipeIngredient { IngredientID = _context.Ingredients.First(i => i.Name == "Rom").ID, Quantity = 60 },
                        new RecipeIngredient { IngredientID = _context.Ingredients.First(i => i.Name == "Seker Surubu").ID, Quantity = 15 },
                        new RecipeIngredient { IngredientID = _context.Ingredients.First(i => i.Name == "Limon Suyu").ID, Quantity = 20 }
                    }
                }
            };

            _context.Recipes.AddRange(recipes);
            _context.SaveChanges();
        }

        Console.WriteLine("Veritabanı başarıyla dolduruldu!");
    }
}
