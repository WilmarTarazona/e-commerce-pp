using e_commerce_pp.Data;
using e_commerce_pp.Models;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace e_commerce_pp.Controllers;
public class ProductController : Controller
{
    private readonly MongoDbContext _mongoDbContext;

    public ProductController(MongoDbContext mongoDbContext)
    {
        _mongoDbContext = mongoDbContext;
    }

    public async Task<IActionResult> Index()
    {
        var products = await _mongoDbContext.Products.Find(_ => true).ToListAsync();
        return View(products);
    }

    public IActionResult Create()
    {
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Create(Product product)
    {
        if (ModelState.IsValid)
        {
            await _mongoDbContext.Products.InsertOneAsync(product);
            return RedirectToAction(nameof(Index));
        }
        return View(product);
    }

    public IActionResult Edit(string id)
    {
        var product = _mongoDbContext.Products.Find(p => p.Id == id).First();
        return View(product);
    }
    [HttpPost]
    public async Task<IActionResult> Edit(string id, Product product)
    {
        if (ModelState.IsValid)
        {
            await _mongoDbContext.Products.ReplaceOneAsync(p => p.Id == id, product);
            return RedirectToAction(nameof(Index));
        }
        return View(product);
    }

    public IActionResult Delete(string id)
    {
        var product = _mongoDbContext.Products.Find(p => p.Id == id).First();
        return View(product);
    }
    [HttpPost, ActionName("Delete")]
    public async Task<IActionResult> DeleteConfirmed(string id)
    {
        await _mongoDbContext.Products.DeleteOneAsync(p => p.Id == id);
        return RedirectToAction(nameof(Index));
    }
}