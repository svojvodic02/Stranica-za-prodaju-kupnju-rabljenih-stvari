using Vjezba.DAL;
using Vjezba.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using vojvodic_sara_1191250193.Controllers;



namespace vojvodic_sara_1191250193.Controllers
{
    public class SecondHandController(SecondHandManagerDbContext _dbContext) : Controller
    {
        public IActionResult Index()
        {
            var listings = _dbContext.Listings
            .Include(l => l.City)
            .Include(l => l.Attachments) 
            .ToList();

            return View(listings);
        }

        public IActionResult Details(int? id = null)
        {
            var client = _dbContext.Listings
                .Include(p => p.City).Include(p => p.ListingType).Include(p => p.Attachments)
                .Where(p => p.ID == id)
                .FirstOrDefault();

            return View(client);
        }

        public IActionResult Create()
        {
            this.FillDropdownValuesCities();
            this.FillDropdownValuesListingTypes();
            return View();
        }

        [HttpPost]
        public IActionResult Create(Listing model)
        {
            model.CreatedAt = model.UpdatedAt;

            int? id = HttpContext.Session.GetInt32("UserId");

            model.UserID = (int)id;

            if (ModelState.IsValid)
            {

                
                _dbContext.Listings.Add(model);
                _dbContext.SaveChanges();

                return RedirectToAction(nameof(Index));

            }
            else
            {

                foreach (var state in ModelState)
                {
                    foreach (var error in state.Value.Errors)
                    {
                        Console.WriteLine($"Field: {state.Key}, Error: {error.ErrorMessage}");
                    }
                }

                this.FillDropdownValuesCities();
                this.FillDropdownValuesListingTypes();
                return View();
            }
        }

        [ActionName(nameof(Edit))]
        public IActionResult Edit(int id)
        {
            int? currentUserId = HttpContext.Session.GetInt32("UserId");
            if (currentUserId == null)
                return RedirectToAction("Login", "User");

            var currentUser = _dbContext.Users.FirstOrDefault(u => u.Id == currentUserId);
            if (currentUser == null)
                return RedirectToAction("Login", "User");

            var model = _dbContext.Listings
                .Include(l => l.Attachments)
                .FirstOrDefault(c => c.ID == id && (c.UserID == currentUserId || currentUser.IsAdmin));

            if (model == null)
                return NotFound(); 

            this.FillDropdownValuesCities();
            this.FillDropdownValuesListingTypes();
            return View(model);
        }

        [HttpPost]
        [ActionName(nameof(Edit))]
        public async Task<IActionResult> EditPost(int id)
        {
            int? currentUserId = HttpContext.Session.GetInt32("UserId");
            if (currentUserId == null)
                return RedirectToAction("Login", "User");

            var currentUser = _dbContext.Users.FirstOrDefault(u => u.Id == currentUserId);
            if (currentUser == null)
                return RedirectToAction("Login", "User");

            var client = _dbContext.Listings
                .Include(l => l.Attachments)
                .FirstOrDefault(l => l.ID == id && (l.UserID == currentUserId || currentUser.IsAdmin));


            if (client == null)
                return RedirectToAction("Index", "Home"); 

            var ok = await this.TryUpdateModelAsync(client);

            if (ok && this.ModelState.IsValid)
            {
                _dbContext.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            this.FillDropdownValuesCities();
            this.FillDropdownValuesListingTypes();
            return View(client);
        }


        private void FillDropdownValuesCities()
        {
            var selectItems = new List<SelectListItem>();

            var listItem = new SelectListItem();
            listItem.Text = "- odaberite -";
            listItem.Value = "";
            selectItems.Add(listItem);

            foreach (var category in _dbContext.Cities)
            {
                listItem = new SelectListItem(category.Name, category.ID.ToString());
                selectItems.Add(listItem);
            }

            ViewBag.PossibleCities = selectItems;
        }

        private void FillDropdownValuesListingTypes()
        {
            var selectItems = new List<SelectListItem>();
            var listItem = new SelectListItem();
            listItem.Text = "- odaberite -";
            listItem.Value = "";
            selectItems.Add(listItem);
            foreach (var category in _dbContext.ListingTypes)
            {
                listItem = new SelectListItem(category.Name, category.ID.ToString());
                selectItems.Add(listItem);
            }
            ViewBag.PossibleListingTypes = selectItems;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UploadAttachment(List<IFormFile> file, int listingId)
        {
            if (file == null || file.Count == 0)
            {
                return BadRequest("No files received.");
            }

            var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", listingId.ToString());

            if(!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            foreach(var formFile in file)
            {
                if (formFile.Length > 0)
                {
                    var fileName= Path.GetFileName(formFile.FileName);
                    var filePath = Path.Combine(uploadsFolder, fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await formFile.CopyToAsync(stream);
                    }

                    var attachment = new Attachment
                    {
                        ListingID = listingId,
                        AttachmentPath = $"/Uploads/{listingId}/{fileName}"
                    };
                    _dbContext.Attachments.Add(attachment);
                }
            }
            await _dbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpGet]
        public IActionResult GetAttachments(int listingId)
        {
            var attachments = _dbContext.Attachments
                .Where(a => a.ListingID == listingId)
                .ToList();
            return PartialView("_AttachmentList", attachments);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteAttachment(int id)
        {
            var attachment = _dbContext.Attachments.FirstOrDefault(a => a.Id == id);
            if (attachment != null)
            {
                _dbContext.Attachments.Remove(attachment);
                _dbContext.SaveChanges();
            }

            return Ok();
        }

    }
}
