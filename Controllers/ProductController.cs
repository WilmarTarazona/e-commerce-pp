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
    // GET: Products
    public async Task<IActionResult> Index()
    {
        var products = await _mongoDbContext.Products.Find(_ => true).ToListAsync();
        return View(products);
    }
    public IActionResult Create()
    {
        return View();
    }
    // POST: Products/Create
    [HttpPost]
    public async Task<IActionResult> Create(Product product)
    {
        try
        {
            if (ModelState.IsValid)
            {
                await _mongoDbContext.Products.InsertOneAsync(product);
                return RedirectToAction(nameof(Index));
            }
        }
        catch (MongoServerException)
        {
            ModelState.AddModelError("", "Unable to save changes. " +
                                    "Try again, and if the problem persists " +
                                    "see your system administrator.");
        }
        return View(product);
    }
    // GET: Products/Edit/id
    public IActionResult Edit(string? id)
    {
        if (id == null)
        {
            return NotFound();
        }
        var product = _mongoDbContext.Products.Find(p => p.Id == id).FirstOrDefault();
        if (product == null)
        {
            return NotFound();
        }
        return View(product);
    }
    // PUT: Products/Edit/id
    [HttpPost, ActionName("Edit")]
    public async Task<IActionResult> Edit(string id, Product product)
    {
        if (id == null)
        {
            return NotFound();
        }
        try
        {
            if (ModelState.IsValid)
            {
                await _mongoDbContext.Products.ReplaceOneAsync(p => p.Id == id, product);
                return RedirectToAction(nameof(Index));
            }
        }
        catch (MongoServerException)
        {
            ModelState.AddModelError("", "Unable to save changes. " +
                                    "Try again, and if the problem persists " +
                                    "see your system administrator.");
        }
        return View(product);
    }
    // GET: Products/Delete/id    
    public IActionResult Delete(string id)
    {
        if (id == null)
        {
            return NotFound();
        }
        var product = _mongoDbContext.Products.Find(p => p.Id == id).FirstOrDefault();
        if (product == null)
        {
            return NotFound();
        }
        return View(product);
    }
    [HttpPost, ActionName("Delete")]
    public async Task<IActionResult> DeleteConfirmed(string id)
    {
        if (id == null)
        {
            return NotFound();
        }
        await _mongoDbContext.Products.DeleteOneAsync(p => p.Id == id);
        return RedirectToAction(nameof(Index));
    }
}