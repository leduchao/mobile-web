//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.Rendering;
//using Microsoft.EntityFrameworkCore;
//using MobileWeb.Areas.Admin.Models;
//using MobileWeb.Data;

//namespace MobileWeb.Areas.Admin.Controllers
//{
//  [Area("Admin")]
//  public class UsersController : Controller
//  {
//    private readonly MobileWebContext _context;

//    public UsersController(MobileWebContext context)
//    {
//      _context = context;
//    }

//    // GET: Admin/Users
//    public async Task<IActionResult> Index()
//    {
//      return _context.User_1 != null ?
//                  View(await _context.User_1.ToListAsync()) :
//                  Problem("Entity set 'MobileWebContext.User_1'  is null.");
//    }

//    // GET: Admin/Users/Details/5
//    public async Task<IActionResult> Details(int? id)
//    {
//      if (id == null || _context.User_1 == null)
//      {
//        return NotFound();
//      }

//      var user = await _context.User_1
//          .FirstOrDefaultAsync(m => m.Id == id);
//      if (user == null)
//      {
//        return NotFound();
//      }

//      return View(user);
//    }

//    // GET: Admin/Users/Create
//    public IActionResult Create()
//    {
//      return View();
//    }

//    // POST: Admin/Users/Create
//    // To protect from overposting attacks, enable the specific properties you want to bind to.
//    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
//    [HttpPost]
//    [ValidateAntiForgeryToken]
//    public async Task<IActionResult> Create([Bind("Id,Name,Email,Password,Address,AvatarUrl,Role")] User user)
//    {
//      if (ModelState.IsValid)
//      {
//        _context.Add(user);
//        await _context.SaveChangesAsync();
//        return RedirectToAction(nameof(Index));
//      }
//      return View(user);
//    }

//    // GET: Admin/Users/Edit/5
//    public async Task<IActionResult> Edit(int? id)
//    {
//      if (id == null || _context.User_1 == null)
//      {
//        return NotFound();
//      }

//      var user = await _context.User_1.FindAsync(id);
//      if (user == null)
//      {
//        return NotFound();
//      }
//      return View(user);
//    }

//    // POST: Admin/Users/Edit/5
//    // To protect from overposting attacks, enable the specific properties you want to bind to.
//    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
//    [HttpPost]
//    [ValidateAntiForgeryToken]
//    public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Email,Password,Address,AvatarUrl,Role")] User user)
//    {
//      if (id != user.Id)
//      {
//        return NotFound();
//      }

//      if (ModelState.IsValid)
//      {
//        try
//        {
//          _context.Update(user);
//          await _context.SaveChangesAsync();
//        }
//        catch (DbUpdateConcurrencyException)
//        {
//          if (!UserExists(user.Id))
//          {
//            return NotFound();
//          }
//          else
//          {
//            throw;
//          }
//        }
//        return RedirectToAction(nameof(Index));
//      }
//      return View(user);
//    }

//    // GET: Admin/Users/Delete/5
//    public async Task<IActionResult> Delete(int? id)
//    {
//      if (id == null || _context.User_1 == null)
//      {
//        return NotFound();
//      }

//      var user = await _context.User_1
//          .FirstOrDefaultAsync(m => m.Id == id);
//      if (user == null)
//      {
//        return NotFound();
//      }

//      return View(user);
//    }

//    // POST: Admin/Users/Delete/5
//    [HttpPost, ActionName("Delete")]
//    [ValidateAntiForgeryToken]
//    public async Task<IActionResult> DeleteConfirmed(int id)
//    {
//      if (_context.User_1 == null)
//      {
//        return Problem("Entity set 'MobileWebContext.User_1'  is null.");
//      }
//      var user = await _context.User_1.FindAsync(id);
//      if (user != null)
//      {
//        _context.User_1.Remove(user);
//      }

//      await _context.SaveChangesAsync();
//      return RedirectToAction(nameof(Index));
//    }

//    private bool UserExists(int id)
//    {
//      return (_context.User_1?.Any(e => e.Id == id)).GetValueOrDefault();
//    }
//  }
//}
