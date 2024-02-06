using System;
using System.Collections.Generic;
using System.Linq;
using EShop;

namespace EShop;

public class EShopDatabase
{
    private List<Category> categories;
    private List<Product> products;
    private int productIdCounter = 1; // начальное значение для идентификаторов товаров
    private int categoryIdCounter = 1; // начальное значение для идентификаторов категорий
    

    public EShopDatabase()
    {
        categories = new List<Category>();
        products = new List<Product>();
    }

    // Методы для категорий
    public void AddCategory(Category category)
    {
        category.Id = GenerateCategoryId(); // присваиваем категории уникальный идентификатор
        if (!categories.Any(c => c.CategoryName == category.CategoryName))
        {
            categories.Add(category);
        }
        else
        {
            Console.WriteLine("Категория с таким названием уже существует.");
        }
    }

    public void RemoveCategory(int categoryId)
    {
        Category category = categories.FirstOrDefault(c => c.Id == categoryId);
        if (category != null)
        {
            categories.Remove(category);
        }
        else
        {
            Console.WriteLine("Категория не найдена.");
        }
    }

    public void UpdateCategory(Category category)
    {
        Category existingCategory = categories.FirstOrDefault(c => c.Id == category.Id);
        if (existingCategory != null)
        {
            existingCategory.CategoryName = category.CategoryName;
        }
        else
        {
            Console.WriteLine("Категория не найдена.");
        }
    }

    public void ShowAllCategories()
    {
        if (categories != null)
        {
            foreach (var category in categories)
            {
                Console.WriteLine($"Id: {category.Id}, Name: {category.CategoryName}");
                Console.WriteLine("Products:");
                if (category.Products != null)
                {
                    foreach (var product in category.Products)
                    {
                        Console.WriteLine($"  Id: {product.Id}, Name: {product.ProductName}, Quantity: {product.Quantity}");
                    }
                }
                Console.WriteLine();
            }
        }
        else
        {
            Console.WriteLine("Список категорий пуст.");
        }
    }

    // Методы для товаров
    public void AddProduct(Product product)
    {
        product.Id = GenerateProductId(); // присваиваем товару уникальный идентификатор
        products.Add(product);
    }

    public void RemoveProduct(int productId)
    {
        Product product = products.FirstOrDefault(p => p.Id == productId);
        if (product != null)
        {
            products.Remove(product);
        }
        else
        {
            Console.WriteLine("Товар не найден.");
        }
    }

    public void UpdateProduct(Product product)
    {
        Product existingProduct = products.FirstOrDefault(p => p.Id == product.Id);
        if (existingProduct != null)
        {
            existingProduct.ProductName = product.ProductName;
            existingProduct.Quantity = product.Quantity;
        }
        else
        {
            Console.WriteLine("Товар не найден.");
        }
    }

    public void ShowAllProducts()
    {
        foreach (var product in products)
        {
            Console.WriteLine($"Id: {product.Id}, Name: {product.ProductName}, Quantity: {product.Quantity}, Category: {product.Category.CategoryName}, CategoryId: {product.CategoryId}");
        }
    }
    
    public Category GetCategoryById(int categoryId)
    {
        return categories.FirstOrDefault(c => c.Id == categoryId);
    }
    
    public Product GetProductById(int productId)
    {
        return products.FirstOrDefault(p => p.Id == productId);
    }
    
    private int GenerateProductId()
    {
        return productIdCounter++; // возвращаем текущее значение и увеличиваем счетчик
    }
    
    private int GenerateCategoryId()
    {
        return categoryIdCounter++; // возвращаем текущее значение и увеличиваем счетчик
    }
}