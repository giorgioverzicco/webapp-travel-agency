using Microsoft.AspNetCore.Mvc;
using webapp_travel_agency.Models;
using webapp_travel_agency.Repositories;

namespace webapp_travel_agency.Controllers;

public class TravelPackagesController : Controller
{
    private readonly IUnitOfWork _unitOfWork;

    public TravelPackagesController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
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
    public async Task<IActionResult> Create(TravelPackage travelPackage)
    {
        if (!ModelState.IsValid)
        {
            return View(travelPackage);
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
    public async Task<IActionResult> Edit(TravelPackage travelPackage)
    {
        if (!ModelState.IsValid)
        {
            return View(travelPackage);
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
        
        _unitOfWork.TravelPackage.Remove(travelPackage);
        await _unitOfWork.SaveAsync();

        return RedirectToAction(nameof(Index));
    }
}