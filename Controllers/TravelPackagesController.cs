using Microsoft.AspNetCore.Mvc;
using webapp_travel_agency.Models;
using webapp_travel_agency.Repositories;

namespace webapp_travel_agency.Controllers;

public class TravelPackagesController : Controller
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IWebHostEnvironment _hostEnvironment;

    public TravelPackagesController(
        IUnitOfWork unitOfWork, 
        IWebHostEnvironment hostEnvironment)
    {
        _unitOfWork = unitOfWork;
        _hostEnvironment = hostEnvironment;
    }

    public async Task<IActionResult> Index()
    {
        var travelPackages = await _unitOfWork.TravelPackage.GetAllAsync();
        return View(travelPackages);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(TravelPackage travelPackage, IFormFile? file)
    {
        if (!ModelState.IsValid)
        {
            return View(travelPackage);
        }
        
        var wwwRootPath = _hostEnvironment.WebRootPath;

        if (file is not null)
        {
            var imageUrl = await CreateImage(file, wwwRootPath);
            travelPackage.ImageUrl = imageUrl;
        }

        await _unitOfWork.TravelPackage.AddAsync(travelPackage);
        await _unitOfWork.SaveAsync();

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int? id)
    {
        if (id is null or 0)
        {
            return BadRequest();
        }
        
        var travelPackage = 
            await _unitOfWork.TravelPackage.GetFirstOrDefaultAsync(x => x.Id == id);

        if (travelPackage is null)
        {
            return NotFound();
        }
        
        return View(travelPackage);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(TravelPackage travelPackage, IFormFile? file)
    {
        if (!ModelState.IsValid)
        {
            return View(travelPackage);
        }

        if (file is not null)
        {
            var wwwRootPath = _hostEnvironment.WebRootPath;
            
            DeleteImage(travelPackage, wwwRootPath);
            var imageUrl = await CreateImage(file, wwwRootPath);
            
            travelPackage.ImageUrl = imageUrl;
        }
        
        _unitOfWork.TravelPackage.Update(travelPackage);
        await _unitOfWork.SaveAsync();

        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int? id)
    {
        if (id is null or 0)
        {
            return BadRequest();
        }
        
        var travelPackage = 
            await _unitOfWork.TravelPackage.GetFirstOrDefaultAsync(x => x.Id == id);

        if (travelPackage is null)
        {
            return NotFound();
        }
        
        DeleteImage(travelPackage, _hostEnvironment.WebRootPath);
        
        _unitOfWork.TravelPackage.Remove(travelPackage);
        await _unitOfWork.SaveAsync();

        return RedirectToAction(nameof(Index));
    }
    
    private static async Task<string> CreateImage(IFormFile file, string wwwRootPath)
    {
        const string staticPath = @"images\travel-packs";

        var fileName = Guid.NewGuid();
        var uploadPath = Path.Combine(wwwRootPath, staticPath);
        var fileExtension = Path.GetExtension(file.FileName);

        await using var fileStream =
            new FileStream(
                Path.Combine(uploadPath, fileName + fileExtension),
                FileMode.Create);

        await file.CopyToAsync(fileStream);

        return @$"\{staticPath}\" + fileName + fileExtension;
    }
    
    private static void DeleteImage(TravelPackage travelPackage, string wwwRootPath)
    {
        if (travelPackage.ImageUrl is null) return;
        
        var oldImagePath = Path.Combine(wwwRootPath, travelPackage.ImageUrl.TrimStart('\\'));

        if (System.IO.File.Exists(oldImagePath))
        {
            System.IO.File.Delete(oldImagePath);
        }
    }
}