using Microsoft.AspNetCore.Mvc;
using mynotes.Database;
using mynotes.Models;
using System;
using System.Linq;

namespace mynotes.Controllers
{
    public class NoteController : Controller
    {
        private readonly DatabaseContext _databaseContext;

        public NoteController(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public IActionResult NoteAdd(string accountId)
        {
            ViewBag.AccountId = accountId;
            return View();
        }

        [HttpPost]
        public IActionResult NoteAdd(NoteRequestModel model)
        {
           
            var account = _databaseContext.Account.FirstOrDefault(a => a.AccountId == model.AccountId);

            if (account != null)
            {
                if (ModelState.IsValid)
                {
                    var newNote = new Notes
                    {
                        AccountId = account.AccountId, 
                        Description = model.Description,
                        CreateTime = DateTime.Now,
                        LastUpdateTime = DateTime.Now
                    };
                    _databaseContext.Notes.Add(newNote);
                    _databaseContext.SaveChanges();


                    return RedirectToAction("Index", new RouteValueDictionary(
                                  new { controller = "User", action = "Index", accountId = account.AccountId }));
                }
                else
                {
                    
                    ModelState.AddModelError("", "Lütfen gerekli alanları doldurun.");
                    return View(model);
                }
            }
            else
            {
                
                ModelState.AddModelError("", "Hesap bilgisi bulunamadı.");
                return RedirectToAction("Index","User");
            }
        }

        public IActionResult NoteList(int accountId)
        {
         
            var account = _databaseContext.Account.FirstOrDefault(a => a.AccountId == accountId);
            if (account == null)
            {
                ModelState.AddModelError("", "Not bulunamadı.");
                return RedirectToAction("Index", "User");
            }

           
            var notes = _databaseContext.Notes.Where(n => n.AccountId == accountId).ToList();

            
            ViewBag.AccountId = accountId;
            return View(notes);
        }


        public IActionResult NoteDelete(int accountId, int noteId)
        {
            var note = _databaseContext.Notes.FirstOrDefault(n => n.NotesId == noteId && n.AccountId == accountId);
            if (note == null)
            {
                ModelState.AddModelError("", "Not bulunamadı.");
                return RedirectToAction("Index", "User");
            }

            _databaseContext.Notes.Remove(note);
            _databaseContext.SaveChanges();

            return RedirectToAction("NoteList", new { accountId = accountId });
        }

        [HttpGet]
        public IActionResult NoteEdit(int noteId)
        {
            var note = _databaseContext.Notes.FirstOrDefault(n => n.NotesId == noteId);
            if (note == null)
            {
                ModelState.AddModelError("", "Not bulunamadı.");
                return RedirectToAction("Index", "User");
            }

           
            return View(note);
        }

        [HttpPost]
        public IActionResult NoteEdit(NoteUpdateRequestModel updatedNote)
        {
            var note = _databaseContext.Notes.FirstOrDefault(n => n.NotesId == updatedNote.NotesId);
            if (note == null)
            {
                ModelState.AddModelError("", "Not bulunamadı.");
                return RedirectToAction("Index", "User");
            }

            if (ModelState.IsValid)
            {
                try
                {
                  
                    note.Description = updatedNote.Description;
                    note.LastUpdateTime = DateTime.Now;

                    _databaseContext.SaveChanges(); 

                    TempData["SuccessMessage"] = "Not başarıyla güncellendi.";
                    return RedirectToAction("NoteList", new RouteValueDictionary(
                        new { controller = "Note", action = "NoteList", accountId = updatedNote.AccountId }));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, $"Güncelleme sırasında bir hata oluştu: {ex.Message}");
                }
            }

            return View(updatedNote);
        }


    }
}
