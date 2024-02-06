using System;
using EShop;

namespace Program;

class Program
{
    static void Main(string[] args)
    {
        EShopDatabase database = new EShopDatabase();

        // Дополнительно: создание 3 категорий и добавление товаров
        Category category1 = new Category { Id = 1, CategoryName = "Electronics" };
        Category category2 = new Category { Id = 2, CategoryName = "Clothing" };
        Category category3 = new Category { Id = 3, CategoryName = "Books" };

        database.AddCategory(category1);
        database.AddCategory(category2);
        database.AddCategory(category3);

        for (int i = 0; i < 5; i++)
        {
            database.AddProduct(new Product { Id = i + 1, ProductName = $"Product {i + 1}", Quantity = 10, CategoryId = 1, Category = category1 });
        }

        for (int i = 5; i < 10; i++)
        {
            database.AddProduct(new Product { Id = i + 1, ProductName = $"Product {i + 1}", Quantity = 10, CategoryId = 2, Category = category2 });
        }

        for (int i = 10; i < 15; i++)
        {
            database.AddProduct(new Product { Id = i + 1, ProductName = $"Product {i + 1}", Quantity = 10, CategoryId = 3, Category = category3 });
        }

        bool exit = false;

        while (!exit)
        {
            Console.WriteLine("Выберите действие:");
            Console.WriteLine("1. Посмотреть все категории и товары");
            Console.WriteLine("2. Посмотреть количество товаров в категории и список товаров по категории");
            Console.WriteLine("3. Создать категорию");
            Console.WriteLine("4. Создать товар");
            Console.WriteLine("5. Удалить категорию");
            Console.WriteLine("6. Удалить товар");
            Console.WriteLine("7. Обновить информацию о категории");
            Console.WriteLine("8. Обновить информацию о товаре");
            Console.WriteLine("9. Выйти");

            int choice = int.Parse(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    Console.WriteLine("Список всех категорий и товаров:");
                    database.ShowAllCategories();
                    break;
                case 2:
                    Console.WriteLine("Введите Id категории:");
                    int categoryId = int.Parse(Console.ReadLine());
                    Category selectedCategory = database.GetCategoryById(categoryId);
                    if (selectedCategory != null)
                    {
                        Console.WriteLine($"Количество товаров в категории {selectedCategory.CategoryName}: {selectedCategory.Products?.Count ?? 0}");
                        Console.WriteLine("Список товаров:");
                        if (selectedCategory.Products != null)
                        {
                            foreach (var product in selectedCategory.Products)
                            {
                                Console.WriteLine($"Id: {product.Id}, Name: {product.ProductName}, Quantity: {product.Quantity}");
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("Категория не найдена.");
                    }
                    break;
                case 3:
                    Console.WriteLine("Введите название категории:");
                    string categoryName = Console.ReadLine();
                    database.AddCategory(new Category { CategoryName = categoryName });
                    Console.WriteLine("Категория успешно создана.");
                    break;
                case 4:
                    Console.WriteLine("Введите название товара:");
                    string productName = Console.ReadLine();
                    Console.WriteLine("Введите количество товара:");
                    int quantity = int.Parse(Console.ReadLine());
                    Console.WriteLine("Введите Id категории:");
                    int productCategoryId = int.Parse(Console.ReadLine());
                    Category productCategory = database.GetCategoryById(productCategoryId);
                    if (productCategory != null)
                    {
                        // Проверяем инициализацию списка Products для категории
                        if (productCategory.Products == null)
                        {
                            productCategory.Products = new List<Product>();
                        }
        
                        Product newProduct = new Product { ProductName = productName, Quantity = quantity, CategoryId = productCategoryId, Category = productCategory };
                        database.AddProduct(newProduct);
        
                        // Добавляем новый товар в список продуктов категории
                        productCategory.Products.Add(newProduct);
        
                        Console.WriteLine("Товар успешно создан.");
                    }
                    else
                    {
                        Console.WriteLine("Категория не найдена.");
                    }
                    break;
                case 5:
                    Console.WriteLine("Введите Id категории для удаления:");
                    int categoryIdToRemove = int.Parse(Console.ReadLine());
                    database.RemoveCategory(categoryIdToRemove);
                    Console.WriteLine("Категория успешно удалена.");
                    break;
                case 6:
                    Console.WriteLine("Введите Id товара для удаления:");
                    int productIdToRemove = int.Parse(Console.ReadLine());
                    Product productToRemove = database.GetProductById(productIdToRemove);
                    if (productToRemove != null)
                    {
                        database.RemoveProduct(productIdToRemove);
        
                        // Удаляем товар из списка продуктов категории
                        Category categoryOfProductToRemove = database.GetCategoryById(productToRemove.CategoryId);
                        if (categoryOfProductToRemove != null && categoryOfProductToRemove.Products != null)
                        {
                            categoryOfProductToRemove.Products.Remove(productToRemove);
                        }
        
                        Console.WriteLine("Товар успешно удален.");
                    }
                    else
                    {
                        Console.WriteLine("Товар не найден.");
                    }
                    break;
                case 7:
                    Console.WriteLine("Введите Id категории для обновления:");
                    int categoryIdToUpdate = int.Parse(Console.ReadLine());
                    Console.WriteLine("Введите новое название категории:");
                    string newCategoryName = Console.ReadLine();
                    database.UpdateCategory(new Category { Id = categoryIdToUpdate, CategoryName = newCategoryName });
                    Console.WriteLine("Информация о категории успешно обновлена.");
                    break;
                case 8:
                    Console.WriteLine("Введите Id товара для обновления:");
                    int productIdToUpdate = int.Parse(Console.ReadLine());
                    Console.WriteLine("Введите новое название товара:");
                    string newProductName = Console.ReadLine();
                    Console.WriteLine("Введите новое количество товара:");
                    int newQuantity = int.Parse(Console.ReadLine());
                    database.UpdateProduct(new Product { Id = productIdToUpdate, ProductName = newProductName, Quantity = newQuantity });
                    Console.WriteLine("Информация о товаре успешно обновлена.");
                    break;
                case 9:
                    exit = true;
                    break;
                default:
                    Console.WriteLine("Некорректный выбор.");
                    break;
            }
        }
    }
}